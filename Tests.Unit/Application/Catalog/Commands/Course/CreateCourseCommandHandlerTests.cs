using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Features.Catalog.Commands.Course.CreateCourse;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Entities.Catalog;
using FluentAssertions;
using Moq;

namespace Tests.Unit.Application.Catalog.Commands.Courses;

public class CreateCourseCommandHandlerTests
{
    private readonly Mock<ICourseRepository> _courseRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock;
    private readonly CreateCourseCommandHandler _handler;

    public CreateCourseCommandHandlerTests()
    {
        _courseRepositoryMock = new Mock<ICourseRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _currentUserServiceMock = new Mock<ICurrentUserService>();
        _handler = new CreateCourseCommandHandler(
            _courseRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _currentUserServiceMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateCourseAndReturnSuccess()
    {
        // Arrange
        var command = new CreateCourseCommand("Title", "Description", 100);
        var userId = Guid.NewGuid();
        _currentUserServiceMock.Setup(x => x.UserId).Returns(userId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _courseRepositoryMock.Verify(x => x.Create(It.Is<Course>(c => 
            c.Title == command.Title && 
            c.Description == command.Description && 
            c.Price.Value == command.Price &&
            c.InstructorId == userId)), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenDomainValidationFails_ShouldReturnFailure()
    {
        // Arrange
        var command = new CreateCourseCommand("", "Description", 100); // Title is empty
        _currentUserServiceMock.Setup(x => x.UserId).Returns(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Title cannot be null");
        _courseRepositoryMock.Verify(x => x.Create(It.IsAny<Course>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Never);
    }
}

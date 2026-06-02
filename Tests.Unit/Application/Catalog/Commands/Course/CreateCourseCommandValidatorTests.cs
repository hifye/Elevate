using Application.Features.Catalog.Commands.Course.CreateCourse;
using FluentAssertions;

namespace Tests.Unit.Application.Catalog.Commands.Course;

public class CreateCourseCommandValidatorTests
{
    private readonly CreateCourseCommandValidator _validator = new();

    [Fact]
    public void Validate_WithValidCommand_ShouldPass()
    {
        // Arrange
        var command = new CreateCourseCommand("Title", "Description", 100);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Validate_WithEmptyTitle_ShouldFail()
    {
        // Arrange
        var command = new CreateCourseCommand("", "Description", 100);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x =>
            x.PropertyName == nameof(CreateCourseCommand.Title) &&
            x.ErrorMessage == "Title is required");
    }

    [Fact]
    public void Validate_WithTitleLongerThan100Characters_ShouldFail()
    {
        // Arrange
        var command = new CreateCourseCommand(new string('a', 101), "Description", 100);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x =>
            x.PropertyName == nameof(CreateCourseCommand.Title) &&
            x.ErrorMessage == "Title cannot be longer than 100 characters.");
    }

    [Fact]
    public void Validate_WithEmptyDescription_ShouldFail()
    {
        // Arrange
        var command = new CreateCourseCommand("Title", "", 100);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x =>
            x.PropertyName == nameof(CreateCourseCommand.Description) &&
            x.ErrorMessage == "Description is required");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WithInvalidPrice_ShouldFail(decimal price)
    {
        // Arrange
        var command = new CreateCourseCommand("Title", "Description", price);

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(x =>
            x.PropertyName == nameof(CreateCourseCommand.Price) &&
            x.ErrorMessage == "Price must be greater than 0");
    }
}

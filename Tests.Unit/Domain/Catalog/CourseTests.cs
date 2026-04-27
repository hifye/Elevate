using Domain.Entities.Catalog;
using FluentAssertions;

namespace Tests.Unit.Domain.Catalog;

public class CourseTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnSuccess()
    {
        // Arrange
        var title = "C# Moderno";
        var description = "Aprenda C# 12 e .NET 8";
        var price = 99.90m;
        var instructorId = Guid.NewGuid();

        // Act
        var result = Course.Create(title, description, price, instructorId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Title.Should().Be(title);
        result.Value.Description.Should().Be(description);
        result.Value.Price.Value.Should().Be(price);
        result.Value.InstructorId.Should().Be(instructorId);
    }

    [Theory]
    [InlineData("", "Description", 10)]
    [InlineData(null, "Description", 10)]
    [InlineData("Title", "", 10)]
    [InlineData("Title", null, 10)]
    [InlineData("Title", "Description", 0)]
    [InlineData("Title", "Description", -5)]
    public void Create_WithInvalidData_ShouldReturnFailure(string title, string description, decimal price)
    {
        // Act
        var result = Course.Create(title, description, price, Guid.NewGuid());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Create_WithTitleTooLong_ShouldReturnFailure()
    {
        // Arrange
        var title = new string('a', 101);
        
        // Act
        var result = Course.Create(title, "Description", 10, Guid.NewGuid());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be("Title cannot be longer than 100 characters.");
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateProperties()
    {
        // Arrange
        var course = Course.Create("Old Title", "Old Description", 10, Guid.NewGuid()).Value!;
        var newTitle = "New Title";
        var newDescription = "New Description";
        var newPrice = 20m;

        // Act
        var result = course.Update(course.Id, newTitle, newDescription, newPrice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.Title.Should().Be(newTitle);
        course.Description.Should().Be(newDescription);
        course.Price.Value.Should().Be(newPrice);
    }

    [Fact]
    public void UpdateTitle_WithValidData_ShouldUpdateTitle()
    {
        // Arrange
        var course = Course.Create("Old Title", "Description", 10, Guid.NewGuid()).Value!;
        var newTitle = "New Title";

        // Act
        var result = course.UpdateTitle(newTitle);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.Title.Should().Be(newTitle);
    }

    [Fact]
    public void UpdateDescription_WithValidData_ShouldUpdateDescription()
    {
        // Arrange
        var course = Course.Create("Title", "Old Description", 10, Guid.NewGuid()).Value!;
        var newDescription = "New Description";

        // Act
        var result = course.UpdateDescription(newDescription);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.Description.Should().Be(newDescription);
    }

    [Fact]
    public void UpdatePrice_WithValidData_ShouldUpdatePrice()
    {
        // Arrange
        var course = Course.Create("Title", "Description", 10, Guid.NewGuid()).Value!;
        var newPrice = 50m;

        // Act
        var result = course.UpdatePrice(newPrice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.Price.Value.Should().Be(newPrice);
    }

    [Fact]
    public void ApplyPatch_WithSomeData_ShouldUpdateOnlyProvidedProperties()
    {
        // Arrange
        var course = Course.Create("Old Title", "Old Description", 10, Guid.NewGuid()).Value!;
        var newTitle = "New Title";

        // Act
        var result = course.ApplyPatch(newTitle, null, null);

        // Assert
        result.IsSuccess.Should().BeTrue();
        course.Title.Should().Be(newTitle);
        course.Description.Should().Be("Old Description");
        course.Price.Value.Should().Be(10);
    }
}

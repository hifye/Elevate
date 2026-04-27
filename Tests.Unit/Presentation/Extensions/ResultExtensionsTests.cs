using Domain.Commom;
using ElevateApi.Commom.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Unit.Presentation.Extensions;

public class ResultExtensionsTests
{
    [Fact]
    public void ToActionResult_SuccessResult_ShouldReturnOk()
    {
        // Arrange
        var result = Result.Success();

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        actionResult.Should().BeOfType<ObjectResult>();
        var objectResult = actionResult as ObjectResult;
        objectResult!.Value.Should().Be(result);
    }

    [Fact]
    public void ToActionResult_GenericSuccessResult_ShouldReturnValue()
    {
        // Arrange
        var value = "Success Data";
        var result = Result<string>.Success(value);

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        actionResult.Should().BeOfType<ObjectResult>();
        var objectResult = actionResult as ObjectResult;
        objectResult!.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("Not Found", typeof(NotFoundObjectResult))]
    [InlineData("Unauthorized", typeof(UnauthorizedObjectResult))]
    [InlineData("Conflict", typeof(ConflictObjectResult))]
    [InlineData("Unknown Error", typeof(BadRequestObjectResult))]
    public void ToActionResult_FailureResult_ShouldReturnCorrectStatusCode(string errorCode, Type expectedType)
    {
        // Arrange
        var errorMessage = "An error occurred";
        var result = Result.Failure(errorMessage, errorCode);

        // Act
        var actionResult = result.ToActionResult();

        // Assert
        actionResult.Should().BeOfType(expectedType);
        var objectResult = actionResult as ObjectResult;
        objectResult!.Value.Should().Be(errorMessage);
    }
}

using Domain.Commom;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Commom.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(this Result<T> result)
    {
        if(result.IsSuccess)
            return new ObjectResult(result.Value);

        return result.ErrorCode switch
        {
            "Not Found" => new NotFoundObjectResult(result.Error),
            "Unauthorized" => new UnauthorizedObjectResult(result.Error),
            "Conflict" => new ConflictObjectResult(result.Error),
            _ => new BadRequestObjectResult(result.Error)
        };
    }

    public static ActionResult ToActionResult(this Result result)
    {
        if(result.IsSuccess)
            return new ObjectResult(result);

        return result.ErrorCode switch
        {
            "Not Found" => new NotFoundObjectResult(result.Error),
            "Unauthorized" => new UnauthorizedObjectResult(result.Error),
            "Conflict" => new ConflictObjectResult(result.Error),
            _ => new BadRequestObjectResult(result.Error)
        };
    }
}
using ConfigurationBag.Core.Common.Consts;
using ConfigurationBag.EndPoint.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sprache;

namespace ConfigurationBag.EndPoint.Api.Attributes;

public class ApiResultFilterAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is OkObjectResult okObjectResult)
        {
            var apiResult = new ApiResult<object>
            {
                Succeeded = true,
                Data = okObjectResult.Value,
                Message = Messages.Success
            };
            context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
        }
        else if (context.Result is OkResult okResult)
        {
            var apiResult = new ApiResult<string>
            {
                Succeeded = true,
                Message = Messages.Success
            };
            context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
        }
        else if (context.Result is ObjectResult { StatusCode: 400 } badRequestObjectResult)
        {
            var messages = new List<string>();
            switch (badRequestObjectResult.Value)
            {
                case ValidationProblemDetails validationProblemDetails:
                    var errorMessages = validationProblemDetails.Errors.SelectMany(p => p.Value).Distinct()
                        .ToList();
                    messages.AddRange(errorMessages);
                    break;

                case SerializableError errors:
                    var errorMessages2 = errors.SelectMany(p => (string[])p.Value).Distinct();
                    messages.AddRange(errorMessages2);
                    break;

                case var value when value != null && value is not ProblemDetails:
                    messages.Add(badRequestObjectResult.Value.ToString());
                    break;
            }

            var apiResult = new ApiResult<string>
            {
                Succeeded = false,
                Message = Messages.BadRequest,
                Errors = messages
            };

            context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
        }
        else if (context.Result is ObjectResult { StatusCode: 404 } notFoundObjectResult)
        {
            string message = null;
            if (notFoundObjectResult.Value != null && notFoundObjectResult.Value is not ProblemDetails)
                message = notFoundObjectResult.Value.ToString();

            var apiResult = new ApiResult<string>
            {
                Succeeded = false,
                Message = Messages.NotFound,
            };

            context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
        }
        else if (context.Result is ContentResult contentResult)
        {
            var apiResult = new ApiResult<object>
            {
                Succeeded = true,
                Data = contentResult.Content,
                Message = Messages.Success
            };
            context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
        }
        else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                                                             && objectResult.Value is not ApiResult<object>)
        {
            if (objectResult.Value == null)
            {
                var apiResult = new ApiResult<string>
                {
                    Succeeded = true,
                    Message = Messages.Success
                };
                context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
            }
            else
            {
                var apiResult = new ApiResult<object>
                {
                    Succeeded = true,
                    Message = Messages.Success,
                    Data = objectResult.Value
                };
                context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
            }
        }

        base.OnResultExecuting(context);
    }
}
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationBag.EndPoint.Api.Models;

public class ApiResult<TData> : ApiResult
{
    [Description("نتیجه ی درخواست ارسال شده")]
    public TData Data { get; set; }

    public ApiResult()
    {
    }

    public ApiResult(TData data, string message) : base(message)
    {
        Data = data;
    }

    #region Implicit Operators

    public static implicit operator ApiResult<TData>(TData data)
    {
        return new ApiResult<TData>
        {
            Succeeded = true,
            Data = data,
            Message = Core.Common.Consts.Messages.CreatedSuccess
        };
    }

    public static implicit operator ApiResult<TData>(OkResult result)
    {
        return new ApiResult<TData>
        {
            Succeeded = true,
            Message = Core.Common.Consts.Messages.Success
        };
    }

    public static implicit operator ApiResult<TData>(OkObjectResult result)
    {
        return new ApiResult<TData>
        {
            Succeeded = true,
            Data = (TData)result.Value,
            Message = Core.Common.Consts.Messages.Success,
        };
    }

    public static implicit operator ApiResult<TData>(BadRequestResult result)
    {
        return new ApiResult<TData>
        {
            Succeeded = false,
            Message = Core.Common.Consts.Messages.UnknownError
        };
    }

    public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
    {
        var errorMessages = new List<string>();
        if (result.Value is SerializableError errors)
            errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
        return new ApiResult<TData>
        {
            Succeeded = false,
            Errors = errorMessages
        };
    }

    public static implicit operator ApiResult<TData>(UnauthorizedResult result)
    {
        return new ApiResult<TData>()
        {
            Succeeded = false,
            Message = Core.Common.Consts.Messages.Unauthorized,
        };
    }

    public static implicit operator ApiResult<TData>(ContentResult result)
    {
        return new ApiResult<TData>
        {
            Succeeded = true,
            Message = result.Content
        };
    }

    public static implicit operator ApiResult<TData>(NotFoundResult result)
    {
        return new ApiResult<TData>()
        {
            Succeeded = true,
            Message = Core.Common.Consts.Messages.NotFound,
        };
    }

    public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
    {
        return new ApiResult<TData>
        {
            Succeeded = true,
            Data = (TData)result.Value,
            Message = Core.Common.Consts.Messages.NotFound
        };
    }

    #endregion Implicit Operators
}

public class ApiResult
{
    public bool Succeeded { get; set; }

    public string Message { get; set; }

    public List<string> Errors;

    protected ApiResult()
    {
    }

    protected ApiResult(string message)
    {
        Message = message;
    }

    #region Implicit Operators

    public static implicit operator ApiResult(OkResult result)
    {
        return new ApiResult();
    }

    public static implicit operator ApiResult(BadRequestResult result)
    {
        return new ApiResult();
    }

    public static implicit operator ApiResult(BadRequestObjectResult result)
    {
        var errorMessages = new List<string>();
        if (result.Value is SerializableError errors)
            errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
        return new ApiResult { Errors = errorMessages };
    }

    public static implicit operator ApiResult(UnauthorizedResult result)
    {
        return new ApiResult();
    }

    public static implicit operator ApiResult(UnauthorizedObjectResult result)
    {
        return new ApiResult(result.Value?.ToString());
    }

    public static implicit operator ApiResult(ContentResult result)
    {
        return new ApiResult
        {
            Message = result.Content
        };
    }

    public static implicit operator ApiResult(NotFoundResult result)
    {
        return new ApiResult();
    }

    #endregion Implicit Operators
}
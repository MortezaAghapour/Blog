using System.Collections.Generic;
using System.Linq;
using Blog.Shared.Enums.Exceptions;
using Blog.Shared.Extensions.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Application.Dtos.Commons
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public CustomStatusCodes StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public ResultDto(bool isSuccess, CustomStatusCodes statusCode, List<string> errors = null, string message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Errors = errors;
            Message = message ?? statusCode.GetDisplayName();
        }

        #region Implicit Operators
        public static implicit operator ResultDto(OkResult result)
        {
            return new ResultDto(true, CustomStatusCodes.Success);
        }

        public static implicit operator ResultDto(BadRequestResult result)
        {
            return new ResultDto(false, CustomStatusCodes.BadRequest);
        }

        public static implicit operator ResultDto(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ResultDto(false, CustomStatusCodes.BadRequest, message: message);
        }

        public static implicit operator ResultDto(ContentResult result)
        {
            return new ResultDto(true, CustomStatusCodes.Success, message: result.Content);
        }

        public static implicit operator ResultDto(NotFoundResult result)
        {
            return new ResultDto(false, CustomStatusCodes.NotFound);
        }
        #endregion
    }


    public class ResultDto<T> : ResultDto where T : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public ResultDto(bool isSuccess, CustomStatusCodes statusCode, T data, List<string> errors = null, string message = null)
            : base(isSuccess, statusCode, errors, message)
        {
            Data = data;
        }

        #region Implicit Operators
        public static implicit operator ResultDto<T>(T data)
        {
            return new ResultDto<T>(true, CustomStatusCodes.Success, data);
        }

        public static implicit operator ResultDto<T>(OkResult result)
        {
            return new ResultDto<T>(true, CustomStatusCodes.Success, null);
        }

        public static implicit operator ResultDto<T>(OkObjectResult result)
        {
            return new ResultDto<T>(true, CustomStatusCodes.Success, (T)result.Value);
        }

        public static implicit operator ResultDto<T>(BadRequestResult result)
        {
            return new ResultDto<T>(false, CustomStatusCodes.BadRequest, null);
        }

        public static implicit operator ResultDto<T>(BadRequestObjectResult result)
        {
            var message = result.Value?.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ResultDto<T>(false, CustomStatusCodes.BadRequest, null, message: message);
        }

        public static implicit operator ResultDto<T>(ContentResult result)
        {
            return new ResultDto<T>(true, CustomStatusCodes.Success, null, message: result.Content);
        }

        public static implicit operator ResultDto<T>(NotFoundResult result)
        {
            return new ResultDto<T>(false, CustomStatusCodes.NotFound, null);
        }

        public static implicit operator ResultDto<T>(NotFoundObjectResult result)
        {
            return new ResultDto<T>(false, CustomStatusCodes.NotFound, (T)result.Value);
        }
        #endregion

    }
}
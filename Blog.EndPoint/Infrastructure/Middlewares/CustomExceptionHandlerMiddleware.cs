using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Application.Dtos.Commons;
using Blog.Shared.Enums.Exceptions;
using Blog.Shared.Exceptions;
using Blog.Shared.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NullReferenceException = Blog.Shared.Exceptions.NullReferenceException;

namespace Blog.EndPoint.Infrastructure.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cancellationToken = context.RequestAborted;
            try
            {
                await _next(context);
            }
            catch (NotFoundException e)
            {
                var errors = new List<string>
                {
                    ErrorResources.ObjectNotFound
                };
                var result = new ResultDto<EmptyDto>(isSuccess: false, statusCode: CustomStatusCodes.NotFound,
                    errors: errors, data: default);
               
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                _logger.LogError(e,e.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }  
            catch (NullArgumentException e)
            {
                var errors = new List<string>
                {
                    ErrorResources.NullArgumentError
                };
                var result = new ResultDto<EmptyDto>(isSuccess: false, statusCode: CustomStatusCodes.NotFound,
                    errors: errors, data: default);
               
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }  
            catch (NullReferenceException e)
            {
                var errors = new List<string>
                {
                    ErrorResources.NullReference
                };
                var result = new ResultDto<EmptyDto>(isSuccess: false, statusCode: CustomStatusCodes.NotFound,
                    errors: errors, data: default);
               
                var json = JsonConvert.SerializeObject(result);
                //log error in database
                 _logger.LogError(e,e.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }
            catch (BadRequestException e)
            {
                var errors = new List<string>
                {
                   ErrorResources.BadRequest
                };
                var result = new ResultDto<EmptyDto>(isSuccess: false, statusCode: CustomStatusCodes.BadRequest,
                    errors: errors, data: default);
                var json = JsonConvert.SerializeObject(result);
                //log error in database 
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);

            }
            catch (AppException e)
            {

                var errors = new List<string>
                {
                   ErrorResources.InternalError
                };
                var result = new ResultDto<EmptyDto>(isSuccess: false, statusCode: CustomStatusCodes.ServerError,
                    errors: errors, data: default);
                
                var json = JsonConvert.SerializeObject(result);
                //log error in database
                 _logger.LogError(e,e.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                var errors = new List<string>
                {
                    ErrorResources.UnknownError
                };
                var result = new ResultDto<EmptyDto>(isSuccess: false, statusCode: CustomStatusCodes.UnKnown,
                    errors: errors, data: default);
               
                var json = JsonConvert.SerializeObject(result);
                //log error in database
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json, cancellationToken: cancellationToken);
            }

        }
    }
}

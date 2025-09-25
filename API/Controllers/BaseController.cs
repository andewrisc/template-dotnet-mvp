using System.Net;
using System.Runtime.CompilerServices;
using API.Interfaces;
using API.Models.Base;
using API.Models.DTOs.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {

        }
        protected async Task<TOutput> DoService<TInput, TOutput>(TInput parameters, Func<TInput, Task<TOutput>> func, [CallerMemberName] string methodName = "")
        where TInput : BaseParameters
        where TOutput : BaseResponse, new()
        {
            return await DoService_LogAsync(parameters, async (parameters) =>
            {
                return await DoService_TryCatchAsync(async () =>
                {
                    return await func(parameters);

                }, methodName);
            }, methodName);
        }

        protected async Task<TOutput> DoService_TryCatchAsync<TOutput>(Func<Task<TOutput>> func, string methodName)
         where TOutput : BaseResponse, new()
        {
            TOutput response = new TOutput();
            try
            {
                response = await func();
            }
            catch (SzValidationException validEx)
            {
                response.SetValidationMessage(validEx.Message);
            }
            catch (Exception ex)
            {
                string methodFullName = GetType().Name + (string.IsNullOrWhiteSpace(methodName) ? "" : ("." + methodName));
                var logger = HttpContext.RequestServices.GetService<ILogService>();
                logger?.LogError(methodFullName, ex);
                response.SetErrorMessage(ex.Message);
            }
            return response;
        }

        protected async Task<TOutput> DoService_LogAsync<TInput, TOutput>(TInput parameters, Func<TInput, Task<TOutput>> func, string methodName)
        where TOutput : BaseResponse, new()
        {
            var logger = HttpContext.RequestServices.GetService<ILogService>();

            string methodFullName = GetType().Name + (string.IsNullOrWhiteSpace(methodName) ? "" : ("." + methodName));

            var paramJson = System.Text.Json.JsonSerializer.Serialize(parameters);
            logger?.LogInfo($"{methodFullName} - Start With Parameters = {paramJson}");

            TOutput result = await func(parameters);

            logger?.LogInfo($"{methodFullName} - End");

            return result;
        }

        protected IActionResult ResponseToActionResult<TResponse>(TResponse response)
        where TResponse : BaseResponse, new()
        {
            HttpStatusCode responseStatusCode = (HttpStatusCode)response.StatusCode;

            switch (responseStatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response);
                case HttpStatusCode.Unauthorized:
                    //Set message if needed.
                    if (string.IsNullOrWhiteSpace(response.Message))
                    {
                        response.Message = "You are not authorized.";
                    }
                    return Unauthorized(response);
                case HttpStatusCode.NotFound:
                    //Set message if needed.
                    if (string.IsNullOrWhiteSpace(response.Message))
                    {
                        response.Message = "Not found.";
                    }
                    return NotFound(response);
                default:
                    return StatusCode(response.StatusCode, response);
            }
        }
    }
}

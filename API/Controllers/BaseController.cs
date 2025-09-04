using System.Net;
using System.Runtime.CompilerServices;
using API.Models.DTOs.Exceptions;
using API.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            //Wrap with log
            return await this.DoService_LogAsync(parameters, async (parameters) =>
            {
                //Wrap with try catch
                return await this.DoService_TryCatchAsync(async () =>
                {
                    //execute original function
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
                string methodFullName = this.GetType().Name + (string.IsNullOrWhiteSpace(methodName) ? "" : ("." + methodName));
                // this.Services.Log.LogError(methodFullName, ex);
                response.SetErrorMessage(ex.Message);
            }
            return response;
        }

        protected async Task<TOutput> DoService_LogAsync<TInput, TOutput>(TInput parameters, Func<TInput, Task<TOutput>> func, string methodName)
        where TOutput : BaseResponse, new()
        {
            string methodFullName = this.GetType().Name + (string.IsNullOrWhiteSpace(methodName) ? "" : ("." + methodName));
            // this.Services.Log.LogInfo(methodFullName + " - Start With Parameters = " + JsonSerializer.Serialize(parameters));
            TOutput result = await func(parameters);
            // this.Services.Log.LogInfo(methodFullName + " - End");
            return result;
        }

        protected IActionResult ResponseToActionResult<TResponse>(TResponse response)
    where TResponse : BaseResponse, new()
        {
            HttpStatusCode responseStatusCode = (HttpStatusCode)response.StatusCode;

            switch (responseStatusCode)
            {
                case HttpStatusCode.OK:
                    return this.Ok(response);
                case HttpStatusCode.Unauthorized:
                    //Set message if needed.
                    if (string.IsNullOrWhiteSpace(response.Message))
                    {
                        response.Message = "You are not authorized.";
                    }
                    return this.Unauthorized(response);
                case HttpStatusCode.NotFound:
                    //Set message if needed.
                    if (string.IsNullOrWhiteSpace(response.Message))
                    {
                        response.Message = "Not found.";
                    }
                    return this.NotFound(response);
                default:
                    return this.StatusCode(response.StatusCode, response);
            }
        }
    }
}

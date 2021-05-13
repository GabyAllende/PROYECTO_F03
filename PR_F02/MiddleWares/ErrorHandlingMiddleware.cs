using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using UPB.FinalProject.Data.Exceptions;
using UPB.FinalProject.Logic.Exceptions;
using UPB.FinalProject.Services.Exceptions;

namespace UPB.FinalProject.PR_F02.MiddleWares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception ex)
        {
            int statusCode = GetCode(ex);
            string message = GetMessage(ex);

            var errorObj = new
            {
                StatusCode = statusCode,
                ErrorMessage = message
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)errorObj.StatusCode;
            await httpContext.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(errorObj));
        }
        private int GetCode(Exception ex)
        {
            int code = 0;

            if (ex is ServiceException)
            {
                code = (int)HttpStatusCode.OK;
            }
            else if (ex is DataBaseException)
            {
                code = (int)HttpStatusCode.InternalServerError;
            }
            else if (ex is ListEmptyException)
            {
                code = (int)HttpStatusCode.InternalServerError;
            }
            else if (ex is InvalidQuotationDataException)
            {
                code = (int)HttpStatusCode.InternalServerError;
            }

            return code;
        }
        private string GetMessage(Exception ex)
        {
            string msg = "";
            if (ex is ServiceException)
            {
                msg = "Something went wrong connecting to the server, error:" + ex.Message;
            }
            else if (ex is DataBaseException)
            {
                msg = "Database problem, error:" + ex.Message;
            }
            else if (ex is ListEmptyException)
            {
                msg = "Database is empty, error:" + ex.Message;
            }
            else if (ex is InvalidQuotationDataException)
            {
                msg = "Error in server, error:" + ex.Message;
            }

            return msg;
        }
    }


    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
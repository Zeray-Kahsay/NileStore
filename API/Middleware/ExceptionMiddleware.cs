using System;
using System.Net;
using System.Text.Json;
using API.Error;

namespace API.Middleware;

/*
 - IHostEnvironment: Provides info about the hosting env the app is running in
 - RequestDelegate: is a function that process an HTTP request
 - HttpContext: Encapsulates all HTTP-specific info about an individual HTTP request
*/
public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      // any logic here and then call the 'next' method to send the incoming request into the next pipline.
      // send the request with its all detail info, encapsulated in the 'context' 
      await next(context);
    }
    catch (Exception ex)
    {
      await HandleExceptionAsync(context, ex, env); //if there is any exception, handle it

    }
  }

  private static Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
  {
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    // check the env the app is running in and send back a response accordingly
    var response = env.IsDevelopment()
        ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
        : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal server error");

    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    var json = JsonSerializer.Serialize(response, options);

    return context.Response.WriteAsync(json);
  }
}

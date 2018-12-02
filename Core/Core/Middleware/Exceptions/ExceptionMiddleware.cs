using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

// Normally, you want to use the singular words for namespace names
// (so Exception instead of Exceptions), but having this namespace
// be Exceptions (plural), we can avoid a conflict with System.Exception.
namespace Core.Middleware.Exceptions {
    
    /// <summary>
    /// Middleware used to catch any exceptions thrown in the request pipeline.
    /// Should be added in the front of the pipeline.
    /// </summary>
    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }
        
        /// <summary>
        /// Allows this class to be used in the same manner as a lambda (which means
        /// it can be invoked as middleware in the request pipeline).
        /// </summary>
        public async Task Invoke(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception e) {
                // Replace this catch blocks with a bunch to handle different exceptions.
                Log.Error("Exception caught by exception handling middleware: {@exception}", e);
                
                // Example of how to send response after logging/handling exception
                if (context.Response.HasStarted) {
                    Log.Warning("Exception handling middleware could not modify response. " +
                                "Response has already started");
                    return;
                }
                await InternalServerError(context);
            }
        }
        
        /// <summary>
        /// Sends back a 500 response with no body.
        /// </summary>
        private static async Task InternalServerError(HttpContext context) {
            context.Response.Clear();
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("");
        }
    }
}
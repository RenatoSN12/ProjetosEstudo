using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;

namespace Blog.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Query.TryGetValue(Configuration.ApiKeyName, out var extractedApitKey)) 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey não encontrada."
                };
                return;
            }

            if (!Configuration.ApiKey.Equals(extractedApitKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Acesso não autorizado."
                };
                return;
            }

            await next();

        }



    }
}

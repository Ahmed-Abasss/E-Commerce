using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _DurationInMin;
        public RedisCacheAttribute(int Duration = 5)
        {
            _DurationInMin = Duration;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //GetCache Service From DI Container
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            //create Cache Key Based On Request Path and Query
            var CacheKey = CreateCacheKey(context.HttpContext.Request);


            // check If Cached Data Exist
            var CachedData =await CacheService.GetAsync(CacheKey);


            //If Existed, Return Cached Data and Skip EndPoint Execution    
            if (CachedData is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CachedData,
                    ContentType = "application/Json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            //if not Exist , invoke EndPoint and Store The Result in Cache if 200 OK Response
            var ExecutedEndPoint= await next.Invoke();

            if(ExecutedEndPoint.Result is OkObjectResult result)
            {
               await CacheService.SetAsync(CacheKey, result.Value, TimeSpan.FromMinutes(_DurationInMin));
            }


        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();

            Key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                Key.Append($"|{item.Key}-{item.Value}");
            }

            return Key.ToString();
        }
    }
}

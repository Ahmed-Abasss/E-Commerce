using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenereateApiValidationResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(x => x.Value.Errors.Count() > 0)
               .ToDictionary(x => x.Key, x => x.Value.Errors.Select(x => x.ErrorMessage).ToList());
            var problem = new ProblemDetails()
            {
                Title = "Validation Error",
                Detail = "One Or More Validation Error Occured",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { "Errors", errors } }
            };
            return new BadRequestObjectResult(problem);
        }
    }
}

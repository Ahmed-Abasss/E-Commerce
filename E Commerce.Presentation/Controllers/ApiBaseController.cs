using E_Commerce.Shared.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ApiBaseController : ControllerBase
    {

        //handle result with no value 
        // if success we'll return 204 no content
        //if result was failure rerurn problem with error details and status code

        protected IActionResult ResultHandler(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else
                return HandleProblem(result.Errors);
                
        }


        //handle error with value
        // if success we'll return ok 200 with (value)
        //if result was failure rerurn problem with error details and status code
    
        protected ActionResult<Tvalue> ResultHandler<Tvalue>(Result<Tvalue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            else
               return HandleProblem(result.Errors);
        
        }

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            //if no errors provided then 500 internal server error
            if (errors.Count == 0)
                return Problem(statusCode:StatusCodes.Status500InternalServerError , title:"UnExcepected Error");

            // if all errors are validation errors handle them as validation proplem
            if(errors.All(e=>e.ErrorType==ErrorType.Validation))
            return HandleValidationProblem(errors);

            // if one error provided single error it is 
            return SingleErrorHandler(errors[0]);
           
        }

        private ActionResult SingleErrorHandler(Error error)
        {
            return Problem(
                title: error.Code,
             detail: error.Description,
             type: error.ErrorType.ToString(),
             statusCode: MapErrorTypeToStatusCode(error.ErrorType)
                );
        }

        private static int MapErrorTypeToStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized=>StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden=>StatusCodes.Status403Forbidden,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.InvalidCredentials=>StatusCodes.Status401Unauthorized,
            ErrorType.Failure=> StatusCodes.Status500InternalServerError,
            _=> StatusCodes.Status500InternalServerError
        };

        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();
        
            foreach(var error in errors)
            {
                modelState.AddModelError(error.Code,error.Description);
            }
            return ValidationProblem(modelState);
        }

    }
}

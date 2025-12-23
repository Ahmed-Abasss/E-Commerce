using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Error
    {


        public string Code { get; }

        public string Description { get; }

        public ErrorType ErrorType { get; }


        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        #region Static Factory Method
        public static Error Failure(string code = "General.Failure", string description = "A General Failure Has Occured")
        {
            return new Error(code, description, ErrorType.Failure);
        }

        public static Error Validation(string code = "General.Validation", string description = "Validation Error Has Occured")
        {
            return new Error(code, description, ErrorType.Validation);
        }

        public static Error NotFound(string code = "General.NotFound", string description = "The Requested Resource Was Not Found")
        {
            return new Error(code, description, ErrorType.NotFound);
        }

        public static Error Unauthorized(string code = "General.Unauthorized", string description = "You Are Not Authorized To Access This Resource")
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }

        public static Error Forbidden(string code = "General.Forbidden", string description = "you don't have permission to access this resource")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "The Provided Credintials Are Not Valid")
        {
            return new Error(code, description, ErrorType.InvalidCredentials);
        }
        #endregion

    }
}

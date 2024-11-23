using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities.DTO
{
    // ApiValidationResponse is used to provide detailed error messages in case of validation failures. 
    // It inherits from ApiResponse to maintain consistency in API responses, 
    // while adding an Errors property to hold a list of validation error messages.
    public class ApiValidationResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationResponse(IEnumerable<string> errors = null, int? statusCode = 400) : base(statusCode)
        {
            Errors = errors ?? new List<string>();
        }
    }
}

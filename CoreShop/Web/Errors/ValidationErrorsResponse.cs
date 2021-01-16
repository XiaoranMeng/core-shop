using System.Collections.Generic;

namespace Web.Errors
{
    public class ValidationErrorsResponse : ResponseBody
    {
        public ValidationErrorsResponse() : base(400) { }

        public IEnumerable<string> Errors { get; set; }
    }
}

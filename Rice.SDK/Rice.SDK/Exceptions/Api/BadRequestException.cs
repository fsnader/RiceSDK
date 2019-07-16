using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rice.SDK.Exceptions.Api
{
    public class BadRequestException : Exception
    {
        public readonly List<string> ValidationErrors = new List<string>();
        
        public BadRequestException(IEnumerable<ValidationResult> validationErrors)
            : base()
        {
            ValidationErrors = validationErrors
                .Select(x => x.ErrorMessage)
                .ToList();
        }

        public BadRequestException() : base()
        {
        }
    }
}
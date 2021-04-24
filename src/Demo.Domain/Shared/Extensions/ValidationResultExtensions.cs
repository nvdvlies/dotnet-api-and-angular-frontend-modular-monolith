﻿using Demo.Domain.Shared.BusinessComponent;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Domain.Shared.Extensions
{
    internal static class ValidationResultExtensions
    {
        internal static IEnumerable<ValidationMessage> AsEnumerableOfValidationMessages(this ValidationResult result)
        {
            var validationMessages = new List<ValidationMessage>();
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    validationMessages.Add( new ValidationMessage { Message = $"{error.PropertyName}: {error.ErrorMessage}" });
                }
            }
            return validationMessages.AsEnumerable();
        }
    }
}

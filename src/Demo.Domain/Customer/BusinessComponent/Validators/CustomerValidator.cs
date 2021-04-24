﻿using Demo.Domain.Shared.BusinessComponent;
using Demo.Domain.Shared.Extensions;
using Demo.Domain.Shared.Interfaces;
using FluentValidation;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Domain.Customer.BusinessComponent.Validators
{
    internal class CustomerValidator : AbstractValidator<Customer>, Shared.Interfaces.IValidator<Customer>
    {
        public async Task<IEnumerable<ValidationMessage>> ValidateAsync(IBusinessComponentContext<Customer> context, CancellationToken cancellationToken = default)
        {
            RuleFor(customer => customer.Name).NotEmpty();
            RuleFor(customer => customer.Name).MaximumLength(200);
            RuleFor(customer => customer.InvoiceEmailAddress).MaximumLength(320);

            // How to: Scope validation rules to specific edit mode
            //When(x => context.EditMode == EditMode.Update, () =>
            //{
            //});

            var validationResult = await ValidateAsync(context.Entity, cancellationToken);
            return validationResult.AsEnumerableOfValidationMessages();
        }
    }
}

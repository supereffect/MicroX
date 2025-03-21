﻿using CQRSWebApiProject.Business.DTO.Request;
using FluentValidation;

namespace CQRSWebApiProject.Business.Validators

{
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(s => s.Id).NotEmpty().GreaterThan(0);
            RuleFor(s => s.Email).NotEmpty().EmailAddress();
            RuleFor(s => s.FirstName).Length(2, 30);
            RuleFor(s => s.LastName).NotEmpty().When(t => t.FirstName != null);
        }
    }
}

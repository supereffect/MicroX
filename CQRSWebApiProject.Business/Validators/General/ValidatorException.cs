using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CQRSWebApiProject.Business.Validators.General
{
    public class ValidatorException : Exception
    {
        private bool Success { get; set; }

        private int StatusCode { get; set; }

        public List<string> Errors { get; set; }


        public ValidatorException(List<string> errors)
        {
            Success = false;
            StatusCode = StatusCodes.Status400BadRequest;
            Errors = errors;
        }
    }
}

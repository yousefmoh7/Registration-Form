using Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructre.ValidatorExtentions
{
    public static class ValidatiorExtentions
    {
        public static async Task ValidateAndThrow<T>(this IValidator<T> validator, T model)
        {
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                var firstError = result.Errors.First();

                throw (firstError?.ErrorCode) switch
                {
                    ValidatorErrorCodes.NotFound => new KeyNotFoundException(firstError.ErrorMessage),
                    ValidatorErrorCodes.BadRequest => new BadHttpRequestException(firstError.ErrorMessage),
                    _ => new InvalidOperationException(),
                };
            }
        }
    }
}

using Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructre.ValidatorExtentions
{
    public static class ValidatiorExtentions
    {
        public static async Task ValidateAndThrow<T>(this IValidator<T> validator,T model){
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                //var dictionary = new Dictionary<string, HashSet<string>>();
                var firstError = result.Errors.First();

                //foreach (var error in result.Errors)
                //    if (dictionary.TryGetValue(error.PropertyName, out var list))
                //        list.Add(error.ErrorMessage);
                //    else dictionary[error.PropertyName] = new HashSet<string> { error.ErrorMessage };

                switch (firstError?.ErrorCode)
                {
                    case ValidatorErrorCodes.NotFound:
                        throw new KeyNotFoundException(firstError.ErrorMessage);
                    case ValidatorErrorCodes.BadRequest:
                        throw new BadHttpRequestException(firstError.ErrorMessage);
                    default:
                        throw new InvalidOperationException();
                }
            }
        }
    }
}

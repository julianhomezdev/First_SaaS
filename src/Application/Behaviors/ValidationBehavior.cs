using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Routing;
using SaaS.src.Application.Common;

namespace SaaS.src.Application.Behaviors
{
    // MediatR pipeline (Request -> ValidationBehavior -> Handler)
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        // Inyects all validators registered for TRequest
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;


        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {

                return await next();

            }



            var context = new ValidationContext<TRequest>(request);


            // Execute all validations in parallel
            var validationRestuls = await Task.WhenAll(

                _validators.Select(v => v.ValidateAsync(context, cancellationToken))

            );


            // Collect all errors
            var failures = validationRestuls
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();


            // If there are errors, return Result.Failure
            if (failures.Any()) {


                var errors = failures.Select(f => f.ErrorMessage).ToList();

                // Create result<T>.Failure
                var responseType = typeof(TResponse);
                if (responseType.IsGenericType && 
                    responseType.GetGenericTypeDefinition() == typeof(Result<>))
                {

                    var dataType = responseType.GetGenericArguments()[0];
                    var resultType = typeof(Result<>).MakeGenericType(dataType);
                    var failureMethod = resultType.GetMethod("Failure");

                    return (TResponse)failureMethod.Invoke(

                        null,
                        new object[] { "Validation errors", errors }

                    );
                }

                throw new ValidationException(failures);
            
            
            }



            // if ok, continue with the handler
            return await next();
        }
    }
}

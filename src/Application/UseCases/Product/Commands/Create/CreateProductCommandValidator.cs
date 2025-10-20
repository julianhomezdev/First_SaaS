using FluentValidation;
using SaaS.src.Application.Interfaces.Repositories;

namespace SaaS.src.Application.UseCases.Product.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {

        private readonly IProductRepository _productRepository;

        public CreateProductCommandValidator(IProductRepository productRepository) {

            _productRepository = productRepository;


            // Validations


            // Name validations
            RuleFor(p => p.ProductName)
                .NotEmpty()
                .WithMessage("The product name is required")
                .MaximumLength(250)
                .WithMessage("The product name cannot exceed 250 characters")
                .MinimumLength(3)
                .WithMessage("The product name must contain at least 3 characters");

            // Product reference validations
            RuleFor(p => p.ProductReference)
                .NotEmpty()
                .WithMessage("The product reference cannot be empty")
                .MinimumLength(3)
                .WithMessage("The product reference must contain at least 3 characters")
                .MustAsync(BeUniqueReference)
                .WithMessage("There is already a product with this reference");

            // Product type validations
            RuleFor(p => p.ProductTypeId)
                .NotEmpty()
                .WithMessage("The product must belong to a category");
        
        }





        // Method to validate is the product reference is unique (validates in database)
        private async Task<bool> BeUniqueReference(string reference, CancellationToken cancellationToken)
        {

            // Return true if not exists = is valid 
            return !await _productRepository.ExistsByReferenceAsync(reference);

        }



    }


    
}

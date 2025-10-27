using FluentValidation;

namespace SaaS.src.Application.UseCases.Product.Queries
{
    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {


        public GetAllProductsQueryValidator()
        {
            // Params validation
            When(x => x.PageSize.HasValue, () => {

                RuleFor(x => x.PageSize)
                    .GreaterThan(0).WithMessage("Page size must be higher than 0")
                    .LessThanOrEqualTo(100).WithMessage("Page size cannot be higher than 100");


            });

            When(x => x.PageNumber.HasValue, () => {
                RuleFor(x => x.PageNumber)
                    .GreaterThan(0).WithMessage("PageNumber debe ser mayor a 0");
            });

        }


    }
}

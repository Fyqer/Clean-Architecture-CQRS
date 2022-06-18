using FluentValidation;

namespace Webinar202103.Application.Tasks.Queries.GetTasksWithPagination
{
    public class GetTasksWithPaginationQueryValidator : AbstractValidator<GetTasksWithPaginationQuery>
    {
        public GetTasksWithPaginationQueryValidator()
        {
            RuleFor(x => x.ListId)
                .NotNull()
                .NotEmpty().WithMessage("ListId is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}

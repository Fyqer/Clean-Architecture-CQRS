using Webinar202103.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Webinar202103.Application.TaskLists.Commands.CreateTaskList
{
    public class CreateTaskListCommandValidator : AbstractValidator<CreateTaskListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTaskListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.TaskLists
                .AllAsync(l => l.Title != title);
        }
    }
}

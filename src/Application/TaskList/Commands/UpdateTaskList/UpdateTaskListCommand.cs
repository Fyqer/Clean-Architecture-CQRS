using Webinar202103.Application.Common.Exceptions;
using Webinar202103.Application.Common.Interfaces;
using Webinar202103.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Webinar202103.Application.TaskLists.Commands.UpdateTaskList
{
    public class UpdateTaskListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTaskListCommandHandler : IRequestHandler<UpdateTaskListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTaskListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTaskListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TaskLists.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TaskList), request.Id);
            }

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

using Webinar202103.Application.Common.Exceptions;
using Webinar202103.Application.Common.Interfaces;
using Webinar202103.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Webinar202103.Application.TaskLists.Commands.DeleteTaskList
{
    public class DeleteTaskListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTaskListCommandHandler : IRequestHandler<DeleteTaskListCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTaskListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTaskListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TaskLists
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TaskList), request.Id);
            }

            _context.TaskLists.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

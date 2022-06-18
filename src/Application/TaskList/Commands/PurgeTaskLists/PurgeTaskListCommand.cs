using Webinar202103.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Webinar202103.Application.TaskLists.Commands.PurgeTaskLists
{
    public class PurgeTaskListsCommand : IRequest
    {
    }

    public class PurgeTaskListsCommandHandler : IRequestHandler<PurgeTaskListsCommand>
    {
        private readonly IApplicationDbContext _context;

        public PurgeTaskListsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PurgeTaskListsCommand request, CancellationToken cancellationToken)
        {
            _context.TaskLists.RemoveRange(_context.TaskLists);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

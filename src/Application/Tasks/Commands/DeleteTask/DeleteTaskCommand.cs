using Webinar202103.Application.Common.Exceptions;
using Webinar202103.Application.Common.Interfaces;
using Webinar202103.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Task = Webinar202103.Domain.Entities.Task;

namespace Webinar202103.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTaskCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tasks.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Task), request.Id);
            }

            _context.Tasks.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

using Webinar202103.Application.Common.Interfaces;
using Webinar202103.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Task = Webinar202103.Domain.Entities.Task;

namespace Webinar202103.Application.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<int>
    {
        public int ListId { get; set; }

        public string Title { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTaskCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = new Task
            {
                ListId = request.ListId,
                Title = request.Title,
            };

            _context.Tasks.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

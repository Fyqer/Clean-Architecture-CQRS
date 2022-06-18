using Webinar202103.Application.Common.Interfaces;
using Webinar202103.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Webinar202103.Application.TaskLists.Commands.CreateTaskList
{
    public class CreateTaskListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateTaskListCommandHandler : IRequestHandler<CreateTaskListCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTaskListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTaskListCommand request, CancellationToken cancellationToken)
        {
            var entity = new TaskList();

            entity.Title = request.Title;

            _context.TaskLists.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Webinar202103.Application.Common.Interfaces;
using Webinar202103.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Webinar202103.Application.TaskLists.Queries.GetTodos
{
    public class GetTaskListsQuery : IRequest<TaskListsVm>
    {
    }

    public class GetTodosQueryHandler : IRequestHandler<GetTaskListsQuery, TaskListsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskListsVm> Handle(GetTaskListsQuery request, CancellationToken cancellationToken)
        {
            return new TaskListsVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

                Lists = await _context.TaskLists
                    .AsNoTracking()
                    .ProjectTo<TaskListDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.Title)
                    .ToListAsync(cancellationToken)
            };
        }
    }
}

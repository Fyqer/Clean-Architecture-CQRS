using AutoMapper;
using AutoMapper.QueryableExtensions;
using Webinar202103.Application.Common.Interfaces;
using Webinar202103.Application.Common.Mappings;
using Webinar202103.Application.Common.Models;
using Webinar202103.Application.TaskLists.Queries.GetTodos;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Webinar202103.Application.Tasks.Queries.GetTasksWithPagination
{
    public class GetTasksWithPaginationQuery : IRequest<PaginatedList<TaskDto>>
    {
        public int ListId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetTasksWithPaginationQueryHandler : IRequestHandler<GetTasksWithPaginationQuery, PaginatedList<TaskDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTasksWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TaskDto>> Handle(GetTasksWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Where(x => x.ListId == request.ListId)
                .OrderBy(x => x.Title)
                .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize); ;
        }
    }
}

using Webinar202103.Application.Common.Mappings;
using Webinar202103.Domain.Entities;
using System.Collections.Generic;

namespace Webinar202103.Application.TaskLists.Queries.GetTodos
{
    public class TaskListDto : IMapFrom<TaskList>
    {
        public TaskListDto()
        {
            Items = new List<TaskDto>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Colour { get; set; }

        public IList<TaskDto> Items { get; set; }
    }
}

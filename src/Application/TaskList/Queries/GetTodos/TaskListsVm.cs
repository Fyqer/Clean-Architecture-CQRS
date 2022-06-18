using System.Collections.Generic;

namespace Webinar202103.Application.TaskLists.Queries.GetTodos
{
    public class TaskListsVm
    {
        public IList<PriorityLevelDto> PriorityLevels { get; set; }

        public IList<TaskListDto> Lists { get; set; }
    }
}

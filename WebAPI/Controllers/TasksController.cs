using Webinar202103.Application.Common.Models;
using Webinar202103.Application.Tasks.Commands.CreateTask;
using Webinar202103.Application.Tasks.Commands.DeleteTask;
using Webinar202103.Application.Tasks.Commands.UpdateTask;

using Webinar202103.Application.Tasks.Queries.GetTasksWithPagination;
using Webinar202103.Application.TaskLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace Webinar202103.WebAPI.Controllers
{
    [Route("api/Tasks")]
    public class TasksController : ApiControllerBase
    {
        [HttpGet]
        [Description("Gets all Tasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<PaginatedList<TaskDto>>> GetTasksWithPagination([FromQuery] GetTasksWithPaginationQuery query)
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        [Description("Creates new Task")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Create(CreateTaskCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut]
        [Route("{id}")]
        [Description("Updates Task. Id must match Command Id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTaskCommand { Id = id });

            return NoContent();
        }
    }
}

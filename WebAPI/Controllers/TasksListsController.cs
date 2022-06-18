using Webinar202103.Application.TaskLists.Commands.CreateTaskList;
using Webinar202103.Application.TaskLists.Commands.DeleteTaskList;
using Webinar202103.Application.TaskLists.Commands.UpdateTaskList;
using Webinar202103.Application.TaskLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace Webinar202103.WebAPI.Controllers
{
    [Route("api/TaskLists")]

    public class TaskListsController : ApiControllerBase
    {
        [HttpGet]
        [Description("Gets all content of  TaskLists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskListsVm>> Get()
        {
            return await Mediator.Send(new GetTaskListsQuery());
        }

        [HttpPost]
        [Description("Creates new Task to do")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> Create(CreateTaskListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        [Route("{id}")]
        [Description("Updates Task. Id must match Command Id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, UpdateTaskListCommand command)
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
            await Mediator.Send(new DeleteTaskListCommand { Id = id });

            return NoContent();
        }
    }
}

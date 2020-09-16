using Application.Helper;
using Application.Interface;
using Application.ToDoItems.Command.AddToDoItem;
using Application.ToDoItems.Command.PatchUpdateToDoItem;
using Application.ToDoItems.Command.UpdateCommand;
using Application.ToDoItems.Query.DeleteToDoItemQuery;
using Application.ToDoItems.Query.SearchToDoItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ToDoService.Controllers
{
    /// <summary>
    /// Controller for CRUD for todoitems
    /// </summary>
    [Route("api/{v:apiVersion}/[controller]")]
    [Authorize]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPatchToDo _patchToDo;
        public ToDoItemsController(IPatchToDo patchToDo, IMediator mediator)
        {
            _patchToDo = patchToDo;
            _mediator = mediator;
        }

        /// <summary>
        /// Method to add new todoitem
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("AddToDoItem")]
        public async Task<ActionResult> AddToDoItem([FromBody]AddToDoItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Method to update todoitem
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("UpdateToDoItem")]
        public async Task<ActionResult> UpdateToDoItem(int itemId, [FromBody]UpdateToDoItemCommand command)
        {
            command.ToDoItem.Id = itemId;
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Method to update todoitem
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("PatchToDoItem")]
        public async Task<ActionResult> PatchToDoItem(int itemId, [FromBody]JsonPatchDocument<UpdateToDoItemCommand> patchDocument)
        {

            UpdatePatchToDoItemCommand command = _patchToDo.CommandToPatch(itemId, patchDocument);
            return Ok(await _mediator.Send(command));
        }


        /// <summary>
        /// Method to delete todoitem by id
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("DeleteToDoItem")]
        public async Task<ActionResult> DeleteToDoItem([FromQuery]DeleteToDoItemQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Method to get all list of todoitems
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Domain.Models.ToDoItemExt>), (int)HttpStatusCode.OK)]
        [Route("GetToDoItems")]
        public async Task<ActionResult> GetToDoItems([FromQuery]EmptyQuery<List<Domain.Models.ToDoItemExt>> command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Method to get all lists based on search criteria and pagesize
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Domain.Models.ToDoItemExt>), (int)HttpStatusCode.OK)]
        [Route("SearchToDoItem")]
        public async Task<ActionResult> SearchToDoItems([FromQuery]SearchToDoItemQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
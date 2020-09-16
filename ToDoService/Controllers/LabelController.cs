using Application.Helper;
using Application.Label.Command.AddLabel;
using Application.Label.Command.AssignItemLabel;
using Application.Label.Command.AssignLabelToList;
using Application.Label.Queries.DeleteLabelById;
using Application.Label.Queries.GetLabelById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


namespace ToDoService.Controllers
{
    /// <summary>
    /// Lable controller for crud operations.
    /// </summary>
    [Route("api/{v:apiVersion}/[controller]")]
    [Authorize]

    public class LabelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LabelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all labels
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Domain.Models.Label>), (int)HttpStatusCode.OK)]
        [Route("GetLabels")]
        public async Task<ActionResult> GetLabels([FromQuery]EmptyQuery<List<Domain.Models.Label>> query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Add label
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("AddLabel")]
        public async Task<IActionResult> AddLabel([FromBody]AddLabelCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(List<Domain.Models.ToDoItemExt>), (int)HttpStatusCode.OK)]
        //[Route("CheckContentLocation")]
        //public async Task<IActionResult> CheckContentLocation([FromBody]AddLabelWithContentLocationCommand command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(List<Domain.Models.ToDoItemExt>), (int)HttpStatusCode.OK)]
        //[Route("CheckContentLocation")]
        //public async Task<IActionResult> CheckContentLocation(int id, [FromBody]JsonPatchDocument<AddLabelWithContentLocationCommand> command)
        //{
        //    return Ok(await _mediator.Send(command));
        //}

        /// <summary>
        /// Get label by label id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Domain.Models.Label>), (int)HttpStatusCode.OK)]
        [Route("GetLabelById")]
        public async Task<ActionResult> GetLabelById([FromQuery] GetLabelByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Delete label by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("DeleteLabelById")]
        public async Task<ActionResult> DeleteLabelById([FromQuery]DeleteLabelByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// assign label to todoitems
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("AssignLabelToItem")]
        public async Task<ActionResult> AssignLabelToItem([FromBody]AssignItemLabelCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// assign label to todolists
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [Route("AssignLabelToList")]
        public async Task<ActionResult> AssignLabelToList([FromBody]AssignLabelToListCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
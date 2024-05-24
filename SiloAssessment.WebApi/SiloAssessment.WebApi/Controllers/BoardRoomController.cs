using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiloAssessment.Application.Commands.BoardRoom;
using SiloAssessment.Application.Queries;
using SiloAssessment.Application.Queries.BoardRoom;

namespace SiloAssessment.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardRoomController : ControllerBase
    {
      

        private readonly ILogger<BoardRoomController> _logger;
        private readonly IMediator _mediator;
        public BoardRoomController(ILogger<BoardRoomController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("getAllBoardRooms")]
        public async Task<IActionResult> GetAvailableRooms()
        {

            var result = await _mediator.Send(new GetBoardRoomsQuery());
            return Ok(result);
        }

        [HttpGet("getAllBoardRoom{id}")]
        public async Task<IActionResult> GetAvailableRoom(string id)
        {

            var result = await _mediator.Send(new GetBoardRoomsQuery());
            return Ok(result);
        }
        [HttpPost("CreateBoardRoom")]
        public async Task<IActionResult> CreateBoardRoom([FromBody] CreateBoardRoomCommand command)
        {
            var boardRoom = await _mediator.Send(command);
            if (boardRoom)
                return Ok(await GetAvailableRooms());
            return BadRequest();
        }

        [HttpPut("UpdateBoardRoom")]
        public async Task<IActionResult> UpdateBoardRoom([FromBody] UpdateBoardRoomCommand command)
        {
            var boardRoom = await _mediator.Send(command);
            if (boardRoom)
                return Ok(await GetAvailableRooms());
            return BadRequest();
        }
    }
}

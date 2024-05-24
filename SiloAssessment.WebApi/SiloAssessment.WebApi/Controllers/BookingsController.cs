using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiloAssessment.Application.Commands.BoardRoom;
using SiloAssessment.Application.Queries;
using SiloAssessment.Application.Queries.BoardRoom;

namespace SiloAssessment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IMediator _mediator;
        public BookingsController(ILogger<BookingsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("getAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {

            var result = await _mediator.Send(new GetBookingQuery());
            return Ok(result);
        }

        [HttpGet("getAllBookings{id}")]
        public async Task<IActionResult> GetAllBookings(string id)
        {

            var result = await _mediator.Send(new GetBookingByIDQuery(id));
            return Ok(result);
        }
        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBookingRoom([FromBody] CreateBookingCommand command)
        {
            var boardRoom = await _mediator.Send(command);
            if (boardRoom)
                return Ok(await GetAllBookings());
            return BadRequest();
        }

        [HttpPut("UpdateBooking")]
        public async Task<IActionResult> UpdateBookingRoom([FromBody] UpdateBookingCommand command)
        {
            var boardRoom = await _mediator.Send(command);
            if (boardRoom)
                return Ok(await GetAllBookings());
            return BadRequest();
        }
    }
}

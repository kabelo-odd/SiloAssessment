using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SiloAssessment.Application.Commands.Requests;
using SiloAssessment.Application.Mapping;
using SiloAssessment.Domain.DTO;
using SiloAssessment.Domain.Interfaces;
using SiloAssessment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Application.Queries
{
    public class UpdateBookingCommand : IRequest<bool>
    {
        [JsonProperty("bookingDetail")]
        public BookingDetail BookingDetail { get; set; }

        [JsonProperty("bookingID")]
        public string BookingId { get; set;}
    }
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, bool>
    {

        private readonly ILogger<UpdateBookingCommand> _logger;
        private readonly IBookingRepository _dataContext;
        public UpdateBookingCommandHandler(ILogger<UpdateBookingCommand> logger, IBookingRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<bool> Handle(UpdateBookingCommand query, CancellationToken cancellationToken)
        {
            try
            {
                var bookings = await _dataContext.GetBookingsByIDAsync(Guid.Parse(query.BookingId));
                if (bookings == null)
                {
                    throw new ArgumentException("Booking doesnt exists");
                }
                var mappedBoardRoom = BookingsMapping.MapToBooking(query.BookingDetail, query.BookingId);
                await _dataContext.UpdateBooking(mappedBoardRoom,bookings);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


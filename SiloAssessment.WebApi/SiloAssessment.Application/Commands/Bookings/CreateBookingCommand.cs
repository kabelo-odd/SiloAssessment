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
    public class CreateBookingCommand : IRequest<bool>
    {
        [JsonProperty("bookingDetail")]
        public BookingDetail BookingDetail { get; set; }
    }
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, bool>
    {

        private readonly ILogger<CreateBookingCommand> _logger;
        private readonly IBookingRepository _dataContext;
        public CreateBookingCommandHandler(ILogger<CreateBookingCommand> logger, IBookingRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<bool> Handle(CreateBookingCommand query, CancellationToken cancellationToken)
        {
            try
            {
                var returnedRooms = new List<BookingDTO>();
                var rooms = await _dataContext.GetBookingsAsync();
                var confirmedBookings = rooms.Where(x => x.BoardRoomId == query.BookingDetail.BoardRoomId && ((x.StartTime >= query.BookingDetail.StartTime && x.StartTime < query.BookingDetail.EndTime) ||
                            (x.EndTime > query.BookingDetail.StartTime && x.EndTime <= query.BookingDetail.EndTime) ||
                            (x.EndTime <= query.BookingDetail.StartTime && x.EndTime >= query.BookingDetail.EndTime))).ToList();
                if (confirmedBookings.Any())
                {
                    throw new ArgumentException("Time slot has already been booked");
                }
                var mappedBooking = BookingsMapping.MapToBooking(query.BookingDetail);
                await _dataContext.AddBookingAsync(mappedBooking);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


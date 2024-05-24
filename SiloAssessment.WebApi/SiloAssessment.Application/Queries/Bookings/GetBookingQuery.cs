using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class GetBookingQuery : IRequest<IEnumerable<BookingDTO>>
    {
    }
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, IEnumerable<BookingDTO>>
    {

        private readonly ILogger<GetBookingQuery> _logger;
        private readonly IBookingRepository _dataContext;
        public GetBookingQueryHandler(ILogger<GetBookingQuery> logger, IBookingRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<IEnumerable<BookingDTO>> Handle(GetBookingQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var returnedBookings = new List<BookingDTO>();
                var bookings = await _dataContext.GetBookingsAsync();
                foreach (var booking in bookings)
                {
                    returnedBookings.Add(new BookingDTO
                    {
                        BoardRoomId = booking.BoardRoomId,
                        BoardRoomName = booking.BoardRoom.Name,
                        EndTime = booking.EndTime,
                        StartTime = booking.StartTime,
                        UserId = booking.UserId,
                        Id = booking.Id
                    });
                }
                return returnedBookings;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


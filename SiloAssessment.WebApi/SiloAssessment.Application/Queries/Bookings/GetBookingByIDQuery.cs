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
    public class GetBookingByIDQuery : IRequest<IEnumerable<BookingDTO>>
    {
        public string BookingID { get; set; }
        public GetBookingByIDQuery(string roomId)
        {
            BookingID = Guard.Argument(roomId, nameof(roomId)).NotNull().Value;
        }
    }
    public class GetBookingByIDQueryHandler : IRequestHandler<GetBookingByIDQuery, IEnumerable<BookingDTO>>
    {

        private readonly ILogger<GetBookingByIDQuery> _logger;
        private readonly IBookingRepository _dataContext;
        public GetBookingByIDQueryHandler(ILogger<GetBookingByIDQuery> logger, IBookingRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<IEnumerable<BookingDTO>> Handle(GetBookingByIDQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var returnedBookings = new List<BookingDTO>();
                var bookings = await _dataContext.GetBookingsByIDAsync(Guid.Parse(query.BookingID));

                returnedBookings.Add(new BookingDTO
                {
                    BoardRoomId = bookings.BoardRoomId,
                    BoardRoomName = bookings.BoardRoom.Name,
                    EndTime = bookings.EndTime,
                    StartTime = bookings.StartTime,
                    UserId = bookings.UserId,
                    Id = bookings.Id
                });

                return returnedBookings;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


using SiloAssessment.Application.Commands.Requests;
using SiloAssessment.Domain.Entities;
using SiloAssessment.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Application.Mapping
{
    public static class BookingsMapping
    {
        public static Booking MapToBooking(BookingDetail bookingDetail, string bookingID = "")
        {
            if (bookingID == null || bookingID == "")
                bookingID = Guid.NewGuid().ToString();
            return new Booking
            {
                Id = Guid.Parse(bookingID),
                BoardRoomId = bookingDetail.BoardRoomId,
                StartTime = bookingDetail.StartTime,
                EndTime = bookingDetail.EndTime,
                UserId = bookingDetail.UserId,
                IsActive = bookingDetail.IsActive
            };
        }
    }
}

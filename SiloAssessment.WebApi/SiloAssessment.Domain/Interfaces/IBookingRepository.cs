using SiloAssessment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<bool> AddBookingAsync(Booking booking);
        Task<Booking> GetBookingsByIDAsync(Guid id);
        Task<List<Booking>> GetBookingsAsync();
        Task<bool> UpdateBooking(Booking updatedBooking,Booking oldBooking);
    }
}

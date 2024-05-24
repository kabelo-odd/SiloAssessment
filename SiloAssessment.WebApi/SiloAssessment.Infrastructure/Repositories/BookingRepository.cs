using Microsoft.EntityFrameworkCore;
using SiloAssessment.Domain.Entities;
using SiloAssessment.Domain.Interfaces;
using SiloAssessment.Infrastructure.Data;
using SiloAssessment.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly DataContext _dataContext;
    public BookingRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<List<Booking>> GetBookingsAsync()
    {
        return await _dataContext.Bookings.Include(x => x.BoardRoom).Where(x => x.IsActive).ToListAsync();
    }
    public async Task<Booking> GetBookingsByIDAsync(Guid id)
    {
        return await _dataContext.Bookings.Include(x => x.BoardRoom).FirstOrDefaultAsync(x => x.IsActive && x.Id == id);
    }

    public async Task<bool> AddBookingAsync(Booking booking)
    {
        await _dataContext.Bookings.AddAsync(booking);
        _dataContext.SaveChanges();
        return true;
    }

    public async Task<bool> UpdateBooking(Booking updatedBooking, Booking oldBooking)
    {
        var foundBooking = await _dataContext.Bookings.FindAsync(updatedBooking.Id);
        foundBooking.BoardRoomId = updatedBooking.BoardRoomId;
        foundBooking.StartTime = updatedBooking.StartTime;
        foundBooking.EndTime = updatedBooking.EndTime;
        foundBooking.IsActive = updatedBooking.IsActive;
        foundBooking.UserId = updatedBooking.UserId;
        await _dataContext.SaveChangesAsync();
        return true;
    }
}



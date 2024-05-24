using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SiloAssessment.Domain.Entities;
using SiloAssessment.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<BoardRoom> BoardRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardRoom>()
                 .Property(e => e.Id)
                 .HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Booking>()
                .HasOne(x => x.BoardRoom)
                .WithMany(bk => bk.Bookings)
                .HasForeignKey(x => x.BoardRoomId);
        }
    }
}

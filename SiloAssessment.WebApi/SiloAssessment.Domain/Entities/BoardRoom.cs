using Microsoft.EntityFrameworkCore.ChangeTracking;
using SiloAssessment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Infrastructure.Entities
{
    public class BoardRoom 
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}

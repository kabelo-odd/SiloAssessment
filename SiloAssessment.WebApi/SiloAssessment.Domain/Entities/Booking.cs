using SiloAssessment.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid BoardRoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public BoardRoom BoardRoom { get; set; }
    }
}

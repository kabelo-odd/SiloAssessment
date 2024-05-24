using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Domain.DTO
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public Guid BoardRoomId { get; set; }
        public string BoardRoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UserId { get; set; }
    }
}

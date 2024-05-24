using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Domain.DTO
{
    public class BoardRoomDTO
    {
        public Guid BoardRoomId { get; set; }
        public string BoardRoomName { get; set; }
        public string BoardRoomDescription { get; set; }
        public bool IsActive { get; set; }
    }
}

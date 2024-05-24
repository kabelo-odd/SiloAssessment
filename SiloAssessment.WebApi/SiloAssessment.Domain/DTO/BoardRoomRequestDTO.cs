using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Domain.DTO
{
    public class BoardRoomRequestDTO
    {
        public string BoardRoomName { get; set; }
        public string BoardDescriptionName { get; set; }
        public bool IsActive { get; set; }
    }
}

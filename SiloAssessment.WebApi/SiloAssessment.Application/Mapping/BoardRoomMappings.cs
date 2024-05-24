using SiloAssessment.Application.Commands.Requests;
using SiloAssessment.Domain.DTO;
using SiloAssessment.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Application.Mapping
{
    public static class BoardRoomMappings
    {
        public static BoardRoom MapToBoardRoom(BoardRoomDetail boardRoomDTO, string boardRoomID)
        {
            if (boardRoomID == null || boardRoomID == "")
                boardRoomID = Guid.NewGuid().ToString();
            return new BoardRoom
            {
                Description = boardRoomDTO.BoardRoomDescription,
                Name = boardRoomDTO.BoardRoomName,
                IsActive = boardRoomDTO.IsActive,
                Id = Guid.Parse(boardRoomID)
            };
        }
    }
}

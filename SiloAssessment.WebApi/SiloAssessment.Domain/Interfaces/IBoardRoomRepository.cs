using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiloAssessment.Domain.DTO;
using SiloAssessment.Infrastructure.Entities;

namespace SiloAssessment.Domain.Interfaces;
public interface IBoardRoomRepository
{
    Task<List<BoardRoom>> GetBoardRoomsAsync();
    Task<BoardRoom> GetBoardRoomByIdAsync(string roomId);
    Task<bool> AddBoardRoomAsync(BoardRoom boardRoomRequest);
    Task<bool> UpdateBoardRoomAsync(BoardRoom boardRoomRequest,BoardRoom oldBoardRoomID);
}


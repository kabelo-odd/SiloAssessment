using Microsoft.EntityFrameworkCore;
using SiloAssessment.Domain.DTO;
using SiloAssessment.Domain.Interfaces;
using SiloAssessment.Infrastructure.Data;
using SiloAssessment.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Infrastructure.Repositories;

public class BoardRoomRepository : IBoardRoomRepository
{
    private readonly DataContext _dataContext;
    public BoardRoomRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<bool> AddBoardRoomAsync(BoardRoom boardRoomRequest)
    {
        try
        {
            await _dataContext.BoardRooms.AddAsync(boardRoomRequest);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }
    }
    public async Task<bool> UpdateBoardRoomAsync(BoardRoom boardRoomRequest, BoardRoom oldBoardRoom)
    {
        try
        {
            var boardRoom = await _dataContext.BoardRooms.FindAsync(oldBoardRoom.Id);
            boardRoom.Description = boardRoomRequest.Description;
            boardRoom.Name = boardRoomRequest.Name;
            boardRoom.IsActive = boardRoomRequest.IsActive;
            // _dataContext.BoardRooms.Update(boardRoomRequest);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }
    }

    public async Task<BoardRoom> GetBoardRoomByIdAsync(string roomId)
    {
        try
        {
            return await _dataContext.BoardRooms.FindAsync(Guid.Parse(roomId));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }
    }

    public async Task<List<BoardRoom>> GetBoardRoomsAsync()
    {
        return await _dataContext.BoardRooms.Where(x => x.IsActive).ToListAsync();
    }
}



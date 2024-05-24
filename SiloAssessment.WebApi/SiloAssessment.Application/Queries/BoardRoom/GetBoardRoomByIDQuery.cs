using Dawn;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SiloAssessment.Domain.DTO;
using SiloAssessment.Domain.Interfaces;
using SiloAssessment.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Application.Queries.BoardRoom
{
    public class GetBoardRoomByIDQuery : IRequest<IEnumerable<BoardRoomDTO>>
    {
        public string RoomId { get; set; }
        public GetBoardRoomByIDQuery(string roomId)
        {
            RoomId = Guard.Argument(roomId, nameof(roomId)).NotNull().Value;
        }
    }
    public class GetBoardRoomByIDQueryHandler : IRequestHandler<GetBoardRoomByIDQuery, IEnumerable<BoardRoomDTO>>
    {

        private readonly ILogger<GetBoardRoomByIDQuery> _logger;
        private readonly IBoardRoomRepository _dataContext;
        public GetBoardRoomByIDQueryHandler(ILogger<GetBoardRoomByIDQuery> logger, IBoardRoomRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<IEnumerable<BoardRoomDTO>> Handle(GetBoardRoomByIDQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var returnedRooms = new List<BoardRoomDTO>();
                var rooms = await _dataContext.GetBoardRoomByIdAsync(query.RoomId);
                returnedRooms.Add(new BoardRoomDTO
                {
                    BoardRoomId = rooms.Id,
                    BoardRoomName = rooms.Name,
                    IsActive = rooms.IsActive,
                });

                return returnedRooms;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured in Method: {nameof(Handle)}, class : {nameof(GetBoardRoomByIDQuery)}. Exception {ex.Message}");
                throw;
            }
        }
    }
}


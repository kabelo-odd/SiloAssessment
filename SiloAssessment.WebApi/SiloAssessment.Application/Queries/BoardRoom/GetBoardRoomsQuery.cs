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
    public class GetBoardRoomsQuery : IRequest<IEnumerable<BoardRoomDTO>>
    {
    }
    public class GetBoardRoomsQueryHandler : IRequestHandler<GetBoardRoomsQuery, IEnumerable<BoardRoomDTO>>
    {

        private readonly ILogger<GetBoardRoomsQuery> _logger;
        private readonly IBoardRoomRepository _dataContext;
        public GetBoardRoomsQueryHandler(ILogger<GetBoardRoomsQuery> logger, IBoardRoomRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<IEnumerable<BoardRoomDTO>> Handle(GetBoardRoomsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var returnedRooms = new List<BoardRoomDTO>();
                var rooms = await _dataContext.GetBoardRoomsAsync();
                foreach (var ro in rooms)
                {
                    returnedRooms.Add(new BoardRoomDTO
                    {
                        BoardRoomId = ro.Id,
                        BoardRoomName = ro.Name,
                        IsActive = ro.IsActive,
                        BoardRoomDescription = ro.Description,
                    });
                }
                return returnedRooms;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


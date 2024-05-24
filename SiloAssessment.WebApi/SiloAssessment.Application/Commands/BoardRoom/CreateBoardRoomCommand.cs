using Dawn;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SiloAssessment.Application.Commands.Requests;
using SiloAssessment.Application.Mapping;
using SiloAssessment.Domain.DTO;
using SiloAssessment.Domain.Interfaces;
using SiloAssessment.Infrastructure.Data;
using SiloAssessment.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Application.Commands.BoardRoom
{
    public class CreateBoardRoomCommand : IRequest<bool>
    {
        [JsonProperty("boardRoomDetail")]
        public BoardRoomDetail BoardRoomDetail { get; set; }

    }

    public class CreateBoardRoomCommandValidator : AbstractValidator<CreateBoardRoomCommand>
    {
        public CreateBoardRoomCommandValidator()
        {
            RuleFor(x => x.BoardRoomDetail).NotEmpty().WithMessage("Board Room detail cannot be null");
        }
    }
    public class CreateBoardRoomCommandHandler : IRequestHandler<CreateBoardRoomCommand, bool>
    {

        private readonly ILogger<CreateBoardRoomCommand> _logger;
        private readonly IBoardRoomRepository _dataContext;
        public CreateBoardRoomCommandHandler(ILogger<CreateBoardRoomCommand> logger, IBoardRoomRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<bool> Handle(CreateBoardRoomCommand query, CancellationToken cancellationToken)
        {
            try
            {
                var returnedRooms = new List<BoardRoomDTO>();
                var rooms = await _dataContext.GetBoardRoomsAsync();
                var roomExists = rooms.Where(x => x.Name == query.BoardRoomDetail.BoardRoomName && x.IsActive == true).ToList();
                if (roomExists.Count != 0)
                {
                    throw new ArgumentException("Board Room Name already exists");
                }
                var mappedBoardRoom = BoardRoomMappings.MapToBoardRoom(query.BoardRoomDetail, "");
                await _dataContext.AddBoardRoomAsync(mappedBoardRoom);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


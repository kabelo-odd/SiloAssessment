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
    public class UpdateBoardRoomCommand : IRequest<bool>
    {
        [JsonProperty("boardRoomDetail")]
        public BoardRoomDetail BoardRoomDetail { get; set; }

        [JsonProperty("boardRoomID")]
        public string BoardRoomID { get; set; }
    }

    public class UpdateBoardRoomCommandValidator : AbstractValidator<UpdateBoardRoomCommand>
    {
        public UpdateBoardRoomCommandValidator()
        {
            RuleFor(x => x.BoardRoomDetail).NotEmpty().WithMessage("Board Room detail cannot be null");
        }
    }
    public class UpdateBoardRoomCommandHandler : IRequestHandler<UpdateBoardRoomCommand, bool>
    {

        private readonly ILogger<UpdateBoardRoomCommand> _logger;
        private readonly IBoardRoomRepository _dataContext;
        public UpdateBoardRoomCommandHandler(ILogger<UpdateBoardRoomCommand> logger, IBoardRoomRepository dataContext)
        {
            _logger = Guard.Argument(logger, nameof(logger)).NotNull().Value;
            _dataContext = Guard.Argument(dataContext, nameof(dataContext)).NotNull().Value;
        }
        public async Task<bool> Handle(UpdateBoardRoomCommand query, CancellationToken cancellationToken)
        {
            try
            {
                var rooms = await _dataContext.GetBoardRoomByIdAsync(query.BoardRoomID);
                
                if (rooms is null)
                {
                    throw new ArgumentException("Board Room Name doesnt exists");
                }
                var mappedBoardRoom = BoardRoomMappings.MapToBoardRoom(query.BoardRoomDetail,query.BoardRoomID);
                await _dataContext.UpdateBoardRoomAsync(mappedBoardRoom, rooms);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}


using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Application.Commands.Requests
{
    public class BoardRoomDetail
    {
        [JsonProperty("boardRoomName")]
        public string BoardRoomName { get; set; }

        [JsonProperty("boardRoomDescription")]
        public string BoardRoomDescription { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

    }
    public class BoardRoomDetailValidator : AbstractValidator<BoardRoomDetail>
    {
        public BoardRoomDetailValidator()
        {
            RuleFor(x => x.BoardRoomName.ToString()).NotEmpty();
            RuleFor(x => x.BoardRoomDescription.ToString()).NotEmpty();
        }
    }
}

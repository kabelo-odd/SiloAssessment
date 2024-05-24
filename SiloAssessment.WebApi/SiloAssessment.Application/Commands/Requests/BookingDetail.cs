using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiloAssessment.Application.Commands.Requests
{
    public class BookingDetail
    {
        public Guid BoardRoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
        public class BookingDetailValidator : AbstractValidator<BookingDetail>
        {
            public BookingDetailValidator()
            {
                RuleFor(x => x.BoardRoomId.ToString()).NotEmpty();
                RuleFor(x => x).Must(x => x.StartTime <= x.EndTime).WithMessage(x => $"Start time must be before End Time");
                RuleFor(x => x.EndTime).LessThanOrEqualTo(DateTime.MaxValue).WithMessage((x,endTime) => $"End time must be before or equal to maximum date");
            }
        }
    }
}

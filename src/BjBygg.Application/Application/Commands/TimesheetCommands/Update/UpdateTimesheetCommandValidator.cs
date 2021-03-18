﻿using BjBygg.Core;
using FluentValidation;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommandValidator : AbstractValidator<UpdateTimesheetCommand>
    {
        public UpdateTimesheetCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty();

            RuleFor(v => v.MissionId)
                .NotEmpty()
                .WithName("Oppdrag");

            RuleFor(v => v.StartTime)
                .NotEmpty()
                .LessThan(x => x.EndTime)
                .WithName("Starttidspunkt");

            RuleFor(v => v.EndTime)
                .NotEmpty()
                .GreaterThan(x => x.StartTime)
                .WithName("Sluttidspunkt");

            RuleFor(v => v.Comment)
                .NotEmpty()
                .MaximumLength(ValidationRules.TimesheetCommentMaxLength)
                .WithName("Kommentar");
        }
    }
}

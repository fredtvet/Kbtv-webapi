﻿using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Create
{
    public class CreateTimesheetCommandProfile : Profile
    {
        public CreateTimesheetCommandProfile()
        {
            CreateMap<CreateTimesheetCommand, Timesheet>();
        }
    }
}

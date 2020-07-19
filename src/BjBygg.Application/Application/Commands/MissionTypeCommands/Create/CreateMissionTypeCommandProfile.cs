﻿using AutoMapper;
using CleanArchitecture.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands.Create
{
    public class CreateMissionTypeCommandProfile : Profile
    {
        public CreateMissionTypeCommandProfile()
        {
            CreateMap<CreateMissionTypeCommand, MissionType>();
            CreateMap<MissionType, CreateMissionTypeCommand>();
        }
    }
}
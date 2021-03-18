using AutoMapper;
using BjBygg.Core.Entities;

namespace BjBygg.Application.Application.Commands.MissionTypeCommands.Update
{
    class UpdateMissionTypeCommandProfile : Profile
    {
        public UpdateMissionTypeCommandProfile()
        {
            CreateMap<UpdateMissionTypeCommand, MissionType>();
        }
    }
}

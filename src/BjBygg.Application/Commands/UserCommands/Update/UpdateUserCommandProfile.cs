using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BjBygg.Application.Commands.UserCommands.Update
{
    class UpdateUserCommandProfile : Profile
    {
        public UpdateUserCommandProfile()
        {
            CreateMap<UpdateUserCommand, ApplicationUser>()
                .ForSourceMember(x => x.Role, opt => opt.DoNotValidate());
        }
    }
}

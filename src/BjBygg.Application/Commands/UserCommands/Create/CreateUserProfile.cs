using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BjBygg.Application.Commands.UserCommands.Create
{
    class CreateUserProfile : Profile
    {
        public CreateUserProfile()
        {
            CreateMap<CreateUserCommand, ApplicationUser>()
                .ForSourceMember(x => x.Password, opt => opt.DoNotValidate())
                .ForSourceMember(x => x.Role, opt => opt.DoNotValidate())
                .ForMember(x => x.Role, opt => opt.Ignore());
        }
    }
}
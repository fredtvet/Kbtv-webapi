using AutoMapper;
using BjBygg.Application.Application.Common.Dto;
using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Application.Commands.TimesheetCommands.Update
{
    public class UpdateTimesheetCommandHandler : IRequestHandler<UpdateTimesheetCommand, TimesheetDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateTimesheetCommandHandler(IAppDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TimesheetDto> Handle(UpdateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var dbTimesheet = await _dbContext.Timesheets.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (dbTimesheet == null)
                throw new EntityNotFoundException(nameof(Timesheet), request.Id);

            if (dbTimesheet.UserName != _currentUserService.UserName) //Can only update self
                throw new ForbiddenException();

            if (dbTimesheet.Status != TimesheetStatus.Open)
                throw new BadRequestException($"Timesheet is closed for manipulation.");

            var ignoredProperties = new List<string>(){ "Id", "UserName", "StartTime", "EndTime" };

            foreach (var property in request.GetType().GetProperties())
            {
                if (ignoredProperties.Contains(property.Name)) continue;
                dbTimesheet.GetType().GetProperty(property.Name).SetValue(dbTimesheet, property.GetValue(request), null);
            }

            dbTimesheet.StartTime = DateTimeHelper.ConvertEpochToDate(request.StartTime);
            dbTimesheet.EndTime = DateTimeHelper.ConvertEpochToDate(request.EndTime);

            dbTimesheet.TotalHours = (dbTimesheet.EndTime - dbTimesheet.StartTime).TotalHours;

            await _dbContext.SaveChangesAsync(); 

            //Assign values after creation to prevent unwanted changes
            return _mapper.Map<TimesheetDto>(dbTimesheet);
        }
    }
}
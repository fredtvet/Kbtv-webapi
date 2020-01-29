using AutoMapper;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BjBygg.Application.Commands.EmployerCommands.Update
{
    public class UpdateEmployerHandler : IRequestHandler<UpdateEmployerCommand, EmployerDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateEmployerHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<EmployerDto> Handle(UpdateEmployerCommand request, CancellationToken cancellationToken)
        {
            var employer = _mapper.Map<Employer>(request);

            try {
                _dbContext.Entry(employer).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EntityNotFoundException($"Entity does not exist with id {request.Id}");
            }

            return _mapper.Map<EmployerDto>(employer);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using BjBygg.Application.Commands.EmployerCommands.Create;
using BjBygg.Application.Commands.EmployerCommands.Delete;
using BjBygg.Application.Commands.EmployerCommands.Update;
using BjBygg.Application.Queries.EmployerQueries.List;
using BjBygg.Application.Shared;
using CleanArchitecture.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BjBygg.WebApi.Controllers
{
    public class EmployersController : BaseController
    {
        private readonly IMediator _mediator;

        public EmployersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IEnumerable<EmployerDto>> Index()
        {
            return await _mediator.Send(new EmployerListQuery());
        }

        [Authorize(Roles = "Leder, Mellomleder, Ansatt")]
        [HttpGet]
        [Route("api/[controller]/Range")]
        public async Task<IEnumerable<MissionDto>> GetDateRange(MissionByDateRangeQuery query)
        {
            if (query.FromDate == null) query.FromDate = DateTime.Now.AddYears(-25);
            if (query.ToDate == null) query.ToDate = DateTime.Now;
            return await _mediator.Send(query);
        }

        [Authorize(Roles = "Leder, Mellomleder")]
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<EmployerDto> Create([FromBody] CreateEmployerCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpPut]
        [Route("api/[controller]/{Id}")]
        public async Task<EmployerDto> Update([FromBody] UpdateEmployerCommand command)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState.Values.ToString());

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "Leder")]
        [HttpDelete]
        [Route("api/[controller]/{Id}")]
        public async Task<bool> Delete(DeleteEmployerCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}

using BjBygg.Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BjBygg.Application.Commands.MissionCommands.Images.Upload
{
    public class UploadMissionImageCommand : IRequest<IEnumerable<MissionImageDto>>
    {
        [Required]
        public IFormFileCollection Files { get; set; }

        [Required]
        public int MissionId { get; set; }

    }
}

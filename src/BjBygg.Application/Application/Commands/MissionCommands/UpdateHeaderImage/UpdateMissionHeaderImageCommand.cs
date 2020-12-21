using BjBygg.Application.Common.Interfaces;
using CleanArchitecture.SharedKernel;
using System.Text.Json.Serialization;

namespace BjBygg.Application.Application.Commands.MissionCommands.UpdateHeaderImage
{
    public class UpdateMissionHeaderImageCommand : IOptimisticCommand
    {
        public string Id { get; set; }

        [JsonIgnore]
        public BasicFileStream Image { get; set; }
    }
}

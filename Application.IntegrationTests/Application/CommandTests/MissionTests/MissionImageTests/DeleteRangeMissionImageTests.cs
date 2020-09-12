﻿using BjBygg.Application.Application.Commands.EmployerCommands;
using BjBygg.Application.Application.Commands.MissionCommands.Images;
using BjBygg.Application.Common.Exceptions;
using CleanArchitecture.Core.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Application.CommandTests.MissionTests.MissionImageTests
{
    using static AppTesting;
    public class DeleteRangeMissionImageTests : AppTestBase
    {
        [Test]
        public void ShouldNotRequireValidMissionImageId()
        {
            var command = new DeleteRangeMissionImageCommand { Ids = new string[] { "notvalid", "notvalid2" } };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().NotThrow();
        }

        [Test]
        public async Task ShouldDeleteMissionImages()
        {
            var ids = new string[] { "test", "test2" };

            await SendAsync(new DeleteRangeMissionImageCommand { Ids = ids });

            var entities = (await GetAllAsync<MissionImage>())
                .Where(x => ids.Contains(x.Id));

            entities.Should().BeEmpty();
        }
    }
}

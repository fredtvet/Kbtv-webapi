﻿using BjBygg.Application.Application.Common.Interfaces;
using BjBygg.Application.Common;
using CleanArchitecture.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    public class TestIdentitySeeder
    {

        public static async Task SeedAllAsync(RoleManager<IdentityRole> roleManager)
        {       
            await SeedRolesAsync(roleManager);          
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employee });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Management }); 
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Leader });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Employer });
        }
    }
}
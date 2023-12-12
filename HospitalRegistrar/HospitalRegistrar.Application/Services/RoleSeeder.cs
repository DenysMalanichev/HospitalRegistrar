using HospitalRegistrar.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HospitalRegistrar.Application.Services;

public class RoleSeeder
{
    private readonly RoleManager<Role> _roleManager;

    public RoleSeeder(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRolesAsync()
    {
        await EnsureRoleExists("Doctor");
        await EnsureRoleExists("Patient");
    }

    private async Task EnsureRoleExists(string roleName)
    {
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await _roleManager.CreateAsync(new Role(roleName));
        }
    }
}
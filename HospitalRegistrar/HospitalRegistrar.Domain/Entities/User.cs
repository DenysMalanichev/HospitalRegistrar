using HospitalRegistrar.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HospitalRegistrar.Domain.Entities;

public class User : IdentityUser<int>
{
    public Doctor? Doctor { get; set; } 
    
    public Patient? Patient { get; set; }

    public Role Role { get; set; } = default!;
}
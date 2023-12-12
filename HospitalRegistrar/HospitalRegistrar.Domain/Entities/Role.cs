using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace HospitalRegistrar.Domain.Entities;

public class Role : IdentityRole<int>
{
    public Role() : base()
    {
    }
    
    public Role(string roleName) : base(roleName)
    {
    }
}
using Domain.Exceptions;
using Domain.Validators;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class Role : IdentityRole<Guid>
{
  

    public static Role Create(string name)
    {
        var validator = new RoleValidator();
        var roleToValidate = new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
            NormalizedName = name.ToUpper()
        };
        var result = validator.Validate(roleToValidate);
        if (result.IsValid) return roleToValidate;
        var exception = new RoleException("Role creation failed");
        if (!result.IsValid)
        {
            result.Errors
                .ToList()
                .ForEach(x => exception.ValidationErrors.Add(x.ErrorMessage));
        }

        throw exception;
    }
}
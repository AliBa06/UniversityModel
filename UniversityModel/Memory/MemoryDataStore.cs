using UniversityModel.Domain.Entities;

namespace UniversityModel.Memory;

public class MemoryDataStore
{
    public List<User> Users { get; } = new();

    public List<Role> Roles { get; } = new();

    public List<UserRole> UserRoles { get; } = new();

    public MemoryDataStore()
    {
        Roles.Add(new Role
        {
            RoleId = 1,
            RoleName = "Admin"
        });

        UserRoles.Add(new UserRole
        {
            UserId = 1,
            RoleId = 1
        });
    }
}
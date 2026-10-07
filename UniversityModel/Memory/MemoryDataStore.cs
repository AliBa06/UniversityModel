using UniversityModel.Domain.Entities;

namespace UniversityModel.Memory;

public class MemoryDataStore
{
    public List<User> Users { get; } = new();

    public List<Role> Roles { get; } = new();

    public List<UserRole> UserRoles { get; } = new();

    public List<RoleRequest> RoleRequests { get; } = new();      //have a list that saves all the request from users for role request
    public MemoryDataStore()
    {
        Roles.Add(new Role
        {
            RoleId = 1,
            RoleName = "Admin"
        });

        Roles.Add(new Role
        {
            RoleId = 2,
            RoleName = "Student"
        });

        Roles.Add(new Role
        {
            RoleId = 3,
            RoleName = "Professor"
        });

        Roles.Add(new Role
        {
            RoleId = 4,
            RoleName = "SuperAdmin"
        });

        UserRoles.Add(new UserRole
        {
            UserId = 1,
            RoleId = 1
        });

        UserRoles.Add(new UserRole
        {
            UserId = 1,
            RoleId = 4
        });
    }
}
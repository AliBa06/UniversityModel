using UniversityModel.Domain.Entities;

namespace UniversityModel.Memory;

public class MemoryDataStore
{
    public List<User> Users { get; } = new();

    public List<Role> Roles { get; } = new();

    public List<UserRole> UserRoles { get; } = new();
}
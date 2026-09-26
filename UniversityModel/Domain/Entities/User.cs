namespace UniversityModel.Domain.Entities;

public class User
{
    public User() { }

    public int UserId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public Student? Student { get; set; }
    public Professor? Professor { get; set; }
    public Employee? Employee { get; set; }
}
namespace UniversityModel.Domain.Entities;

public class User
{
    public User() { }

    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;     // password salting 1.2
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public Student? Student { get; set; }
    public Professor? Professor { get; set; }
    public Employee? Employee { get; set; }
}
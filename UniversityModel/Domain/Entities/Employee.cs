namespace UniversityModel.Domain.Entities
{
    public class Employee
    {
        public Employee() { }
        public int EmployeeId { get; set; }
        public int PersonalId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
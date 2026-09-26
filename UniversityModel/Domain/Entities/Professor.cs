namespace UniversityModel.Domain.Entities
{
    public class Professor
    {
        public Professor() { }
        public int ProfessorId { get; set; }
        public int PersonalId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string specialist { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
    }
}
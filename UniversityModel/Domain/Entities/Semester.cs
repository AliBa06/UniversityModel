namespace UniversityModel.Domain.Entities
{
    public class Semester
    {
        public Semester() { }
        public int SemesterId { get; set; }
        public string SemesterName { get; set; } = string.Empty;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private  set; }
        public int AcademicYear { get; set; }
        public ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
    }
}

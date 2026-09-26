namespace UniversityModel.Domain.Entities
{
    public class Course
    {
        public Course() { }
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public int Units { get; set; }
        public ICollection<CourseOffering> CourseOfferings { get; set; } = new List <CourseOffering>();
        public ICollection<CourseField> CourseFields { get; set; } = new List<CourseField>();
    }
}
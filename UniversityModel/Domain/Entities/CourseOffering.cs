
namespace UniversityModel.Domain.Entities
{
    public class CourseOffering
    {
        public CourseOffering() { }
        public int CourseOfferingId { get; set; }
        public int Capacity { get; set; }
        public void ChangeCapacity(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.");

            Capacity = capacity;
        }
        public string Status { get; set; } = string.Empty;
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public int SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
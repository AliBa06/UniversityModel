namespace UniversityModel.Domain.Entities
{
    public class Enrollment
    {
        public Enrollment() { }
        public int EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int OfferingId { get; set; }
        public CourseOffering CourseOffering { get; set; } = null!;
    }
}
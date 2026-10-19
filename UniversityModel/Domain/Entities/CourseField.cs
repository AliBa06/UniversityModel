namespace UniversityModel.Domain.Entities
{
    public class CourseField            // ارتباط میان رشته تحصیلی و دروس مانند طراحی پایگاه داده 
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public int FieldOfStudyId { get; set; }
        public FieldOfStudy FieldOfStudy { get; set; } = null!;
        public bool IsRequired { get; set; }
    }
}
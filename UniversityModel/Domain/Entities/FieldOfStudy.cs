namespace UniversityModel.Domain.Entities
{
    public class FieldOfStudy              // Field of study like computer engineering
    {
        public FieldOfStudy() { }
        public int FieldOfStudyId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string DegreeName { get; set; } = string.Empty;
        public ICollection<CourseField> CourseFields { get; set; } = new List<CourseField>();

    }
}
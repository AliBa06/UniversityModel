namespace UniversityModel.Domain.Entities
{
    public class FieldOfStudy
    {
        public FieldOfStudy() { }
        public int FieldOfStudyId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string DegreeName { get; set; } = string.Empty;
        public ICollection<CourseField> CourseFields { get; set; } = new List<CourseField>();

    }
}
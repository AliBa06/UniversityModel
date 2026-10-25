
namespace UniversityModel.Dtos;
public class CourseOfferingListDto
{
    public int CourseOfferingId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string ProfessorFullName { get; set; } = string.Empty;
    public int SemesterId { get; set; }
    public int Capacity { get; set; }
    public string Status { get; set; } = string.Empty;
}

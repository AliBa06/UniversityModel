namespace UniversityModel.Dtos;
public class CreateCourseOfferingDto
{
    public int CourseId { get; set; }
    public int SemesterId { get; set; }
    public int Capacity { get; set; }
}
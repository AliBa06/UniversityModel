using UniversityModel.Domain.Entities;
using UniversityModel.Dtos;
using UniversityModel.Memory;
namespace UniversityModel.Service;
public class CourseOfferingService
{
    private readonly MemoryDataStore _dataStore;

    public CourseOfferingService(MemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    public List<AvailableCourseDto> GetAvailableCourses()
    {
        return _dataStore.Courses
            .Select(course => new AvailableCourseDto
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName
            })
            .ToList();
    }
    public CourseOffering? Create( CreateCourseOfferingDto dto, int userId)   //NEW METHOD
       

    {
        // پیدا کردن استاد مربوط به حساب کاربری واردشده
        var professor = _dataStore.Professors
            .FirstOrDefault(x => x.UserId == userId);

        if (professor is null)
            return null;

        // بررسی اینکه درس انتخاب‌شده وجود دارد
        var course = _dataStore.Courses
            .FirstOrDefault(x => x.CourseId == dto.CourseId);

        if (course is null)
            return null;

        // بررسی وجود ترم
        var semester = _dataStore.Semesters
            .FirstOrDefault(x => x.SemesterId == dto.SemesterId);

        if (semester is null)
            return null;

        // ظرفیت باید بیشتر از صفر باشد
        if (dto.Capacity <= 0)
            return null;

        // ساخت ارائه‌ی جدید
        var offering = new CourseOffering
        {
            CourseOfferingId =
                _dataStore.CourseOfferings.Count + 1,

            CourseId = course.CourseId,
            ProfessorId = professor.ProfessorId,
            SemesterId = semester.SemesterId,
            Capacity = dto.Capacity,
            Status = "Open"
        };

        // ذخیره در حافظه
        _dataStore.CourseOfferings.Add(offering);

        return offering;
    }
    public List<CourseOfferingListDto> GetAll()
    {
        return _dataStore.CourseOfferings
            .Select(offering =>
            {
                var course = _dataStore.Courses
                    .FirstOrDefault(x =>
                        x.CourseId == offering.CourseId);

                var professor = _dataStore.Professors
                    .FirstOrDefault(x =>
                        x.ProfessorId == offering.ProfessorId);

                return new CourseOfferingListDto
                {
                    CourseOfferingId = offering.CourseOfferingId,

                    CourseName = course?.CourseName
                        ?? "نام درس مشخص نیست",

                    ProfessorFullName = professor is null
                        ? "نام استاد مشخص نیست"
                        : $"{professor.FirstName} {professor.LastName}",

                    SemesterId = offering.SemesterId,
                    Capacity = offering.Capacity,
                    Status = offering.Status
                };
            })
            .ToList();
    }

    public List<CourseOfferingListDto> GetAllForStudent(int userId)
    {
        
        var student = _dataStore.Students.FirstOrDefault(x => x.UserId == userId);    // دانشجویی را پیدا کن که در memorydata وجود داشته باشد

        if (student is null)
            return new List<CourseOfferingListDto>();


        return _dataStore.CourseOfferings     //دروسی را پیدا کن که مربوط به رشته تحصیلی فرد باشد
            .Where(offering => _dataStore.CourseFields.Any(courseField =>
                courseField.CourseId == offering.CourseId &&
                courseField.FieldOfStudyId == student.FieldOfStudyId))
            .Select(offering =>
            {
                var course = _dataStore.Courses
                    .FirstOrDefault(x => x.CourseId == offering.CourseId);

                var professor = _dataStore.Professors
                    .FirstOrDefault(x => x.ProfessorId == offering.ProfessorId);

                return new CourseOfferingListDto
                {
                    CourseOfferingId = offering.CourseOfferingId,
                    CourseName = course?.CourseName ?? "نام درس مشخص نیست",
                    ProfessorFullName = professor is null
                        ? "نام استاد مشخص نیست"
                        : $"{professor.FirstName} {professor.LastName}",
                    SemesterId = offering.SemesterId,
                    Capacity = offering.Capacity,
                    Status = offering.Status
                };
            })
            .ToList();
    }

}
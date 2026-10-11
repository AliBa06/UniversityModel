using UniversityModel.Domain.Entities;
using UniversityModel.Memory;

namespace UniversityModel.Service;

public class EnrollmentService
{
    private readonly MemoryDataStore _dataStore;

    public EnrollmentService(MemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public Enrollment? Enroll(int userId, int offeringId)
    {
     
        var student = _dataStore.Students .FirstOrDefault(x => x.UserId == userId);   // پیدا کردن دانشجوی مربوط به کاربر واردشده
        if (student is null)
            return null;
        var offering = _dataStore.CourseOfferings.FirstOrDefault(x =>x.CourseOfferingId == offeringId);  // پیدا کردن ارائه‌ی درس
        if (offering is null)
            return null;

        var courseBelongsToStudentField = _dataStore.CourseFields.Any(x =>x.CourseId == offering.CourseId && x.FieldOfStudyId == student.FieldOfStudyId);
        if (!courseBelongsToStudentField)
        {
            return null;
        }

        if (offering.Status != "Open")  // بررسی وضعیت ارائه
            return null;

        
        var alreadyEnrolled = _dataStore.Enrollments .Any(x =>x.StudentId == student.StudentId && x.OfferingId == offeringId);// جلوگیری از انتخاب دوباره‌ی همان ارائه

        if (alreadyEnrolled)
            return null;
       
        var enrolledCount = _dataStore.Enrollments.Count(x => x.OfferingId == offeringId); // بررسی ظرفیت
        if (enrolledCount >= offering.Capacity)
            return null;

   
        var enrollment = new Enrollment     // ایجاد انتخاب واحد
        {
            EnrollmentId =
                _dataStore.Enrollments.Count + 1,

            StudentId = student.StudentId,
            OfferingId = offeringId,
            EnrollmentDate = DateTime.UtcNow
        };

        _dataStore.Enrollments.Add(enrollment);

        return enrollment;
    }

    public List<Enrollment> GetStudentEnrollments(int userId)
    {
        var student = _dataStore.Students
            .FirstOrDefault(x => x.UserId == userId);

        if (student is null)
            return new List<Enrollment>();

        return _dataStore.Enrollments
            .Where(x => x.StudentId == student.StudentId)
            .ToList();
    }
}
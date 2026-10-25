using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityModel.Service;
namespace UniversityModel.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnrollmentController : ControllerBase
{
    private readonly EnrollmentService _service;
    public EnrollmentController(EnrollmentService service)
    {
        _service = service;
    }
    [HttpPost("{offeringId}")]
    [Authorize(Roles = "Student")]
    public IActionResult Enroll(int offeringId)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return Unauthorized();
        if (!int.TryParse(userIdClaim.Value, out var userId))
            return Unauthorized();
        var enrollment = _service.Enroll(userId, offeringId);
        if (enrollment is null)
        {
            return BadRequest(
                "ثبت‌نام انجام نشد؛ ممکن است ارائه وجود نداشته باشد، ظرفیت پر شده باشد یا قبلاً ثبت‌نام کرده باشید.");
        }
        return Ok(enrollment);
    }
    [HttpGet("my-enrollments")]
    [Authorize(Roles = "Student")]
    public IActionResult GetMyEnrollments()
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return Unauthorized();
        var userId = int.Parse(userIdClaim.Value);
        var enrollments =
            _service.GetStudentEnrollments(userId);
        return Ok(enrollments);
    }
}
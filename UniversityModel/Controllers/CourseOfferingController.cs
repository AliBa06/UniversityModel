using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityModel.Dtos;
using UniversityModel.Service;

namespace UniversityModel.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CourseOfferingController : ControllerBase
{
    private readonly CourseOfferingService _service;

    public CourseOfferingController(
        CourseOfferingService service)
    {
        _service = service;
    }

    // استاد: مشاهده تمام درس‌های موجود
    [HttpGet("available-courses")]
    [Authorize(Roles = "Professor")]
    public IActionResult GetAvailableCourses()
    {
        var courses = _service.GetAvailableCourses();

        return Ok(courses);
    }

    // استاد: ارائه یک درس برای یک ترم
    [HttpPost]
    [Authorize(Roles = "Professor")]
    public IActionResult Create(
        [FromBody] CreateCourseOfferingDto dto)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
            return Unauthorized();

        if (!int.TryParse(
            userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        var offering = _service.Create(dto, userId);

        if (offering is null)
        {
            return BadRequest(
                "شما قادر به ایجاد این درس نیستید.");
        }

        return Ok(offering);
    }

    // دانشجو: مشاهده ارائه‌های ثبت‌شده
    [HttpGet]
    [Authorize(Roles = "Student")]
    public IActionResult GetAll()
    {
        var offerings = _service.GetAll();

        return Ok(offerings);
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityModel.Service;
namespace UniversityModel.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoleRequestController : ControllerBase
{
    private readonly RoleRequestService _roleRequestService;

    public RoleRequestController(RoleRequestService roleRequestService)
    {
        _roleRequestService = roleRequestService;
    }

    [HttpPost]
    [HttpPost]
    [HttpPost]
    public IActionResult CreateRequest(int roleId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim.Value);

        var request = _roleRequestService.CreateRequest(
            userId,
            roleId);

        if (request is null)
            return BadRequest("This role cannot be requested.");

        return Ok(request);
    }
    [HttpPut("{id}/approve")]
    [Authorize(Roles = "SuperAdmin")]
    public IActionResult Approve(int id)
    {
        var request = _roleRequestService.ApproveRequest(id);

        if (request is null)
            return NotFound();

        return Ok(request);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/reject")]
    public IActionResult RejectRequest(int id, [FromBody] string reason)
    {
        var result = _roleRequestService.RejectRequest(id, reason);

        if (!result)
            return NotFound("Role request not found.");

        return Ok("Role request rejected.");
    }
    [HttpGet("roles")]
    public IActionResult GetAvailableRoles()
    {
        var roles = _roleRequestService.GetAvailableRoles();

        return Ok(roles);
    }
    [HttpGet("{id}/status")]
    [Authorize]

    public IActionResult GetStatus(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
            return Unauthorized();

        var userId = int.Parse(userIdClaim.Value);

        var request = _roleRequestService.GetRequestById(id);

        if (request is null)
            return NotFound();

        if (request.UserId != userId)
            return Forbid();

        return Ok(new
        {
            request.RoleRequestId,
            request.Status,
            request.RejectionReason
        });
    }
    [Authorize(Roles = "SuperAdmin")]
    [HttpGet("all")]
    public IActionResult GetAllRequests()
    {
        var requests = _roleRequestService.GetAllRequests();

        return Ok(requests);
    }
}   


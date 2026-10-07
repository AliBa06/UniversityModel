namespace UniversityModel.Domain.Entities;

public class RoleRequest
{
    public int RoleRequestId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string Status { get; set; } = "Pending";                                 // when user request something it will appear as default
    public string? RejectionReason { get; set; }
}
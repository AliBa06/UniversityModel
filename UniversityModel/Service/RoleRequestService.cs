using UniversityModel.Domain.Entities;
using UniversityModel.Memory;

public class RoleRequestService
{
    private readonly MemoryDataStore _dataStore;

    public RoleRequestService(MemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public RoleRequest CreateRequest(int userId, int roleId)
    {

        var alreadyHasRole = _dataStore.UserRoles 
            .Any(x =>
                x.UserId == userId &&
                x.RoleId == roleId);

        if (alreadyHasRole)
        {
            throw new Exception(
                "شما درحال حاضر این سمت را دارید. .");
        }


        var hasPendingRequest = _dataStore.RoleRequests   
            .Any(x =>
                x.UserId == userId &&
                x.Status == "Pending");

        if (hasPendingRequest)
        {
            throw new Exception(
                "شما یک درخواست بررسی نشده دارید.");
        }


        var request = new RoleRequest    
        {
            RoleRequestId = _dataStore.RoleRequests.Count + 1,
            UserId = userId,
            RoleId = roleId,
            Status = "Pending"
        };

        _dataStore.RoleRequests.Add(request);

        return request;
    }
    public List<RoleRequest> GetAllRequests()
    {
        return _dataStore.RoleRequests;
    }
    public RoleRequest? ApproveRequest(int requestId)
    {
        var request = _dataStore.RoleRequests
            .FirstOrDefault(x => x.RoleRequestId == requestId);

        if (request is null)
            return null;

        request.Status = "Approved";

        _dataStore.UserRoles.Add(new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        });

        return request;
    }
    public bool RejectRequest(int requestId, string reason)
    {
        var request = _dataStore.RoleRequests
            .FirstOrDefault(x => x.RoleRequestId == requestId);

        if (request == null)
            return false;

        request.Status = "Rejected";
        request.RejectionReason = reason;

        return true;
    }
    public List<Role> GetAvailableRoles()
    {
        return _dataStore.Roles
            .Where(x =>
                x.RoleName == "Student" ||
                x.RoleName == "Professor")
            .ToList();
    }
    public RoleRequest? GetRequestById(int requestId)
    {
        return _dataStore.RoleRequests
            .FirstOrDefault(x => x.RoleRequestId == requestId);
    }

}   



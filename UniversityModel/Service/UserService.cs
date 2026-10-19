using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UniversityModel.Data;
using UniversityModel.Domain.Entities;
using UniversityModel.Dtos;
using UniversityModel.DTOs;
using UniversityModel.Memory;
namespace UniversityModel.Service;

public class UserService
{

    private readonly MemoryDataStore _dataStore;
    private readonly IConfiguration _configuration;          //4.3 adding a new field 
    private readonly ILogger _logger;
    public UserService(MemoryDataStore dataStore , IConfiguration configuration, ILogger<UserService> logger)   //4.4 giving Iconfiguration to Userservice
    {
        _dataStore = dataStore;
        _configuration = configuration;
        _logger = logger;
    }

    public List<UserResponse> GetAll()
    {
        return _dataStore.Users
            .Select(user => new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            })
            .ToList();
    }
    public User? GetById(int id)
    {   
        _logger.LogInformation("searching for user with ID {Userid}",id);
        var user = _dataStore.Users.FirstOrDefault(x => x.UserId == id);
        if (user == null) {
            _logger.LogWarning("User with Id {UserId} was not found", id);
                return null;
        }
        _logger.LogInformation("User with Id {UserId} was found ", id);
        return _dataStore.Users
            .FirstOrDefault(x => x.UserId == id);
            
    }
    private List<string> GetUserRoles(int userId)
    {
        return _dataStore.UserRoles
            .Where(x => x.UserId == userId)
            .Join(
                _dataStore.Roles,
                userRole => userRole.RoleId,
                role => role.RoleId,
                (userRole, role) => role.RoleName
            )
            .ToList();
    }
    public string Login(string userName, string password)     //3.1 new method login
    {
        var user = _dataStore.Users
            .FirstOrDefault(x => x.UserName == userName);            //finding user in userlists

        if (user is null)
            throw new Exception("اطلاعات وارد شده صحیح نیست");

        var isPasswordCorrect = VerifyPassword(
            password,
            user.PasswordSalt,
            user.PasswordHash);


        if (isPasswordCorrect ==false)
            throw new Exception("اطلاعات وارد شده صحیح نیست");

        return GenerateToken(user);
    }

    private string GenerateToken(User user)
    {
        var roles = GetUserRoles(user.UserId);

        var claims = new List<Claim>
    {
        new Claim(
            ClaimTypes.NameIdentifier,
            user.UserId.ToString()),

        new Claim(
            ClaimTypes.Name,
            user.UserName)
    };

        foreach (var role in roles)
        {
            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(
                    _configuration["Jwt:ExpireMinutes"]!)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
    private bool VerifyPassword( string password, string salt,string storedHash)   // 2.1 new method password verifying
    {
        var hash = HashPassword(password, salt);

        return hash == storedHash;
    }
    private string GenerateSalt()                         //new method called salting 1.1
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        return Convert.ToHexString(salt);
    }
    private string HashPassword(string password, string salt) // adding string salt 1.4
    {
        using SHA256 sha256 = SHA256.Create();
        string passwordwithSalt = password + salt; // adding passwordwithSalt 1.5

        byte[] bytes = Encoding.UTF8.GetBytes(passwordwithSalt); //1.6

        byte[] hash = sha256.ComputeHash(bytes);

        return Convert.ToHexString(hash);
    }
    public void Create(UserRequestDto user)
    {
        var newUser = new User
        {
            UserId = _dataStore.Users.Count + 1,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        var usernameExists = _dataStore.Users   // TODO: check username is not exist in db & we have to use _dataStore exept Context beacuse we are using _datastore in memory datastore
            .Any(x => x.UserName == user.UserName);

        if (usernameExists)
        {
            throw new Exception("کاربری با این اطلاعات قبلا ایجاد شده است.");
        }
        if (user == null)
        {
            throw new Exception("user is null");
        }

        if (user.Password != user.ConfirmPassword)
        {
            throw new Exception("password does not match");
        }
        var salt = GenerateSalt(); //using the Generatesalt method 1.3

        var passwordHash = HashPassword(user.Password, salt);      // calling the hashpassword method and putting salt and password as inputs (password + salt)

        newUser.PasswordSalt = salt;
        newUser.PasswordHash = passwordHash;

        _dataStore.Users.Add(newUser);
    }
    public bool Delete(int id)
    {
        var user = GetById(id);

        if (user is null)
            return false;

        _dataStore.Users.Remove(user);

        return true;
    }
}

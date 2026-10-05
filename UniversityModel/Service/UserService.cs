using Microsoft.EntityFrameworkCore;
using UniversityModel.Domain.Entities;
using UniversityModel.Dtos;
using UniversityModel.Memory;
using UniversityModel.Data;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Runtime.Intrinsics.Arm;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace UniversityModel.Service;

public class UserService
{

    private readonly MemoryDataStore _dataStore;
    private readonly IConfiguration _configuration;          //4.3 adding a new field 
    public UserService(MemoryDataStore dataStore , IConfiguration configuration)   //4.4 giving Iconfiguration to Userservice
    {
        _dataStore = dataStore;
        _configuration = configuration;
    }

    public List<User> GetAll()
    {
        return _dataStore.Users;
    }
    public User? GetById(int id)
    {
        return _dataStore.Users
            .FirstOrDefault(x => x.UserId == id);
    }
    private string? GetUserRole(int userId)                           // a method named getuserrole , we give it an userid 
    {
        var userRole = _dataStore.UserRoles                                 // search the whole userrole list
            .FirstOrDefault(x => x.UserId == userId);                       // 

        if (userRole is null)
            return null;

        var role = _dataStore.Roles
            .FirstOrDefault(x => x.RoleId == userRole.RoleId);

        if (role is null)
            return null;

        return role.RoleName;                 //now that you find the roleid find the role
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

    private string GenerateToken(User user)                                                //4.5  this method needs user to build token and will return this as string
    {
        var role = GetUserRole(user.UserId);

        var claims = new[]                                                                // claim is the informations that we put in token now make a bunch of claims
        {
        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),                         // the first class is about naem identifier if userid is 15 nameidentifier will be 15 so save name identifier in this token
          
        new Claim(ClaimTypes.Role,role ?? ""),
    
    
        new Claim(ClaimTypes.Name,user.UserName)
            
                                                                                                                //so we save userid and name
    };

        var key = new SymmetricSecurityKey(                                                                        // after changing the jwt key to bytes change it to a securitykey
            Encoding.UTF8.GetBytes( _configuration["Jwt:Key"]!));                                                        // ! we are sure that key is not null
               

        var credentials = new SigningCredentials( key,SecurityAlgorithms.HmacSha256);                                            // sign the key with algorithm hmacsha
           
            

        var token = new JwtSecurityToken(                                                                                     //build a jwt
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,                                                                                              // put the claims in jwt
            expires: DateTime.UtcNow.AddMinutes(double.Parse( _configuration["Jwt:ExpireMinutes"]!)),                 // expires utc + expireminute
            signingCredentials: credentials);                                                                               // sign with the same algorithm we used for key
                   
            

        return new JwtSecurityTokenHandler()
            .WriteToken(token);                                                                                                 //we transfer the jwt object that we wrote in to jwt string
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
    public void Create(UserDto user)
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
            throw new Exception("Username already exists");
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

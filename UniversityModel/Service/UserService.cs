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
namespace UniversityModel.Service;

public class UserService
{

    private readonly MemoryDataStore _dataStore;                
    public UserService(MemoryDataStore dataStore)
    {
        _dataStore = dataStore;
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
    public bool Login(string userName, string password)     //3.1 new method login
    {
        var user = _dataStore.Users
            .FirstOrDefault(x => x.UserName == userName);            //finding user in userlists

        if (user is null)
            return false;

        var isPasswordCorrect = VerifyPassword(
            password,
            user.PasswordSalt,
            user.PasswordHash);

        return isPasswordCorrect;
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


        var newUser = new User();
        newUser.UserName = user.UserName;
        newUser.FirstName = user.FirstName;
        newUser.LastName = user.LastName;

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

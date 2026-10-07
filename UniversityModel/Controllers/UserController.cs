using Microsoft.AspNetCore.Mvc;
using UniversityModel.Domain.Entities;
using UniversityModel.Dtos;
using UniversityModel.Service;
using Microsoft.AspNetCore.Authorization;
namespace UniversityModel.Controllers;
using System.Security.Claims;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }


    [HttpGet]                                                                  // GET: api/User
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();

        return Ok(users);
    }

    
    [HttpGet("{id}")]                                                             // GET: api/User/5
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

                                                                                         // POST: api/User
    [HttpPost]
    [AllowAnonymous]
    public IActionResult Create(UserDto user)
    {

        _userService.Create(user);                                                       //add user in userservice

        return Ok(user);
    }

    [HttpDelete("{id}")]                                                                        // DELETE: api/User/5
    [Authorize(Roles = "Admin")]                                                                                                
    public IActionResult Delete(int id)
    {
        var deleted = _userService.Delete(id);

        if (!deleted)
            return NotFound();

        return Ok();

    }
    [HttpPost("login")]
    [AllowAnonymous]//creating login method in controller
    public IActionResult Login(LoginDto loginDto)
    {
        var result = _userService.Login(
            loginDto.UserName,
            loginDto.Password);

        
          

        return Ok (result);
    }
}

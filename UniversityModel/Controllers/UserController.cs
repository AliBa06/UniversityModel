using Microsoft.AspNetCore.Mvc;
using UniversityModel.Domain.Entities;
using UniversityModel.Dtos;
using UniversityModel.Service;

namespace UniversityModel.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public IActionResult Create(UserDto user)
    {

        _userService.Create(user);                                                       //add user in userservice

        return Ok(user);
    }

    [HttpDelete("{id}")]                                                                        // DELETE: api/User/5
                                                                                                    
    public IActionResult Delete(int id)
    {
        var deleted = _userService.Delete(id);

        if (!deleted)
            return NotFound();

        return Ok();
    }
}
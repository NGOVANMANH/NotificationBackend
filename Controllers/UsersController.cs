using Microsoft.AspNetCore.Mvc;
using notify.Dtos;
using notify.Exceptions;
using notify.Interfaces;

namespace notify.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
    {
        var user = await _userService.CreateUserAsync(userCreateDto);
        return Ok(user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userService.GetUserAsync(id);

        if (user is null)
        {
            throw new GlobalException("User not found.", System.Net.HttpStatusCode.NotFound);
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }
}
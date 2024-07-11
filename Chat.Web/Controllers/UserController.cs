using AutoMapper;
using Chat.BLL.Service.Interface;
using Chat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateModel user,CancellationToken cancellationToken)
    {
        var userModel = await userService.CreateUserAsync(user.Name, cancellationToken);
        return Ok(mapper.Map<UserViewModel>(userModel));
    }
    
    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> DeleteUserAsync(int userId, CancellationToken cancellationToken)
    {
        await userService.DeleteUserAsync(userId, cancellationToken);
        return Ok("User was deleted");
    }
}
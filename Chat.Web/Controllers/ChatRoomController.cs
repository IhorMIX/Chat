using AutoMapper;
using Chat.BLL.Services.Interface;
using Chat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Web.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class ChatRoomController(IChatRoomService chatRoomService, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateChatRoomAsync([FromBody] ChatRoomCreateModel chatRoom,CancellationToken cancellationToken)
    {
        var chatRoomModel = await chatRoomService.CreateChatRoomAsync(chatRoom.CreatorId,chatRoom.Name, cancellationToken);
        return Ok(mapper.Map<ChatRoomViewModel>(chatRoomModel));
    }
    
    [HttpDelete("{userId:int}/{chatRoomId:int}")]
    public async Task<IActionResult> DeleteChatRoomAsync([FromRoute] int userId,[FromRoute] int chatRoomId, CancellationToken cancellationToken)
    {
        await chatRoomService.DeleteChatRoomAsync(userId, chatRoomId, cancellationToken);
        return Ok("ChatRoom was deleted");
    }
}
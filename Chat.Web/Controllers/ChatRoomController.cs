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
    
    [HttpPut("join")]
    public async Task<IActionResult> JoinRoom([FromQuery] int userId, [FromQuery] int chatRoomId, CancellationToken cancellationToken)
    {
        await chatRoomService.JoinChatRoomAsync(userId, chatRoomId, cancellationToken);
        return Ok("User has just became a chatRoomMember");
    }
    
    [HttpPut("leave")]
    public async Task<IActionResult> LeaveRoom([FromQuery] int userId, [FromQuery] int chatRoomId, CancellationToken cancellationToken)
    {
        await chatRoomService.LeaveChatRoomAsync(userId, chatRoomId, cancellationToken);
        return Ok("User has been leave from chatRoom");
    }
}
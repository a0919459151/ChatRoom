using ChatRoom.Contracts.ChatRoom;
using ChatRoom.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

[Authorize]
public class ChatRoomController : Controller
{
    private readonly IChatRoomService _chatRoomService;

    public ChatRoomController(IChatRoomService chatRoomService)
    {
        _chatRoomService = chatRoomService;
    }

    public IActionResult Index()
    {
        ChatRoomViewModel vm = new();

        return View(vm);
    }

    public IActionResult ChatRoom()
    {
        return View();
    }
}

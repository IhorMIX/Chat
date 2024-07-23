namespace Chat.BLL.Exceptions;

public class UserAlreadyInChatRoomException(string message) : CustomException(message);
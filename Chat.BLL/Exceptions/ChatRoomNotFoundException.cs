namespace Chat.BLL.Exceptions;

public class ChatRoomNotFoundException(string message) : CustomException(message);
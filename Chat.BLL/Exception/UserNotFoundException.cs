using System;
namespace Chat.BLL.Exception;

public class UserNotFoundException(string message) : CustomException(message);
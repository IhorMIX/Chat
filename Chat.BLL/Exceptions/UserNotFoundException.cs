using System;
namespace Chat.BLL.Exceptions;

public class UserNotFoundException(string message) : CustomException(message);
public class LoginException : Exception
{
    public LoginException(String message) : base(message)
    {
    }
}
public class InvalidUKAddressException : Exception
{
    public InvalidUKAddressException(String message) : base(message)
    {
    }
}

public class TooManyLoginTriesException : Exception
{
    public TooManyLoginTriesException() : base("Login failed more than 3 times")
    {
    }
}


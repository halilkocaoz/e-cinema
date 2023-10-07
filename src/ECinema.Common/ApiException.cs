namespace ECinema.Common;

public class ApiException(string message, int code) : Exception
{
    public override string Message { get; } = message;
    public int Code { get; } = code;
}
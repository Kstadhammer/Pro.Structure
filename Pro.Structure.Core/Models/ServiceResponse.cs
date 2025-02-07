namespace Pro.Structure.Core.Models;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public static ServiceResponse<T> Ok(T data, string message = "")
    {
        return new ServiceResponse<T>
        {
            Data = data,
            Success = true,
            Message = message,
        };
    }

    public static ServiceResponse<T> Fail(string message)
    {
        return new ServiceResponse<T> { Success = false, Message = message };
    }
}

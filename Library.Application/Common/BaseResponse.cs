namespace Library.Application.Common;

public class BaseResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public List<string> Errors { get; set; } = new();
}

public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }
}
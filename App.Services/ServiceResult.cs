using System.Net;

namespace Services;

public class ServiceResult<T>
{
    public T? Data { get; private set; }

    public IEnumerable<string> ErrorMessages { get; private set; }
        = Enumerable.Empty<string>();

    public bool IsSuccess => !ErrorMessages.Any();

    public bool IsFailed => !IsSuccess;
    
    public HttpStatusCode statusCode { get; set; }

    public static ServiceResult<T> Success(T data,HttpStatusCode statusCode=HttpStatusCode.OK)
        => new()
        {
            Data = data,
            ErrorMessages = Enumerable.Empty<string>(),
            statusCode = statusCode,
        };

    public static ServiceResult<T> Failed(IEnumerable<string> errors,HttpStatusCode statusCode=HttpStatusCode.BadRequest)
        => new()
        {
            ErrorMessages = errors,
            statusCode = statusCode
        };
}


//Update ve Delete işlemlerimde genelde ben bir data dondurmuyorum bu yuzden bunu bu sekilde olusturdum//
public class ServiceResult
{
    public IEnumerable<string> ErrorMessages { get; private set; }
        = Enumerable.Empty<string>();

    public bool IsSuccess => !ErrorMessages.Any();

    public bool IsFailed => !IsSuccess;
    
    public HttpStatusCode statusCode { get; set; }

    public static ServiceResult Success(HttpStatusCode statusCode=HttpStatusCode.OK)
        => new()
        {
            ErrorMessages = Enumerable.Empty<string>(),
            statusCode = statusCode,
        };

    public static ServiceResult Failed(IEnumerable<string> errors,HttpStatusCode statusCode=HttpStatusCode.BadRequest)
        => new()
        {
            ErrorMessages = errors,
            statusCode = statusCode
        };
}

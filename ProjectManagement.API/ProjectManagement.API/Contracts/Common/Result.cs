namespace ProjectManagement.API.Contracts.Common
{
    public static class Result
    {
        public static Result<T> Success<T>(T data)
        {
            return new Result<T>() { Status = StatusCode.Success, Data = data };
        }
    }

    public class Result<T>
    {
        public static Result<T> Create(StatusCode status)
        {
            return new Result<T>() { Status = status };
        }

        public static Result<T> Create(StatusCode status, T data)
        {
            return new Result<T>() { Status = status, Data = data };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>() { Status = StatusCode.Success, Data = data };
        }
        public static Result<T> Success()
        {
            return new Result<T>() { Status = StatusCode.Success };
        }

        public bool IsSuccess => ((int)Status) > 0;
        public StatusCode Status { get; set; }
        public T Data { get; set; }
    }
}
namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public Status Status { get; set; }

        public static Result<T> Sucess(T value,Status status=Status.OK) => new Result<T> {IsSuccess = true, Value = value,Status = status};
        public static Result<T> Failure(string error,Status status=Status.ERROR) => new Result<T> {IsSuccess = false, Error = error,Status =  status};
    }

    public enum Status
    {
        OK,NOT_FOUND,CREATED,ERROR,NO_CONTENT
    }
}
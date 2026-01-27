namespace SimpleBankingSystem.Domain.ErrorHandler
{
    public sealed class Result (bool success, string? message)
    {
        public bool IsSuccess { get; } = success;
        public bool IsFailure => !IsSuccess;
        public string? Message {  get; } = message;

        public static Result Success()
        {
            return new Result(true, null);
        }

        public static Result Success(string message)
        {
            return new Result(true, message);
        }

        public static Result Failure(string message)
        {
            return new Result(false, message);
        }
    }
}

using System;
using System.Collections.Generic;


namespace OpenStandup.SharedKernel
{
    public class Result<T> : IResult<T>
    {
        public T Value { get; }
        public ResultStatus Status { get; }
        public IEnumerable<string> Errors { get; private set; } = new List<string>();
        public Exception Exception { get; private set; }

        public bool Succeeded => Status == ResultStatus.Success;

        public Result(T value)
        {
            Value = value;
        }

        private Result(ResultStatus status)
        {
            Status = status;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failed(params string[] errorMessages)
        {
            return new Result<T>(ResultStatus.Failed) { Errors = errorMessages };
        }

        public static Result<T> Failed(ResultStatus resultStatus, params string[] errorMessages)
        {
            return new Result<T>(resultStatus) { Errors = errorMessages };
        }

        public static Result<T> Failed(ResultStatus resultStatus, Exception exception, params string[] errorMessages)
        {
            return new Result<T>(resultStatus) { Errors = errorMessages, Exception = exception };
        }
    }
}



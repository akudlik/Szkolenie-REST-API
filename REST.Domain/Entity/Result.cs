using System.Collections.Generic;

namespace REST.Domain.Entity
{
    public class Result<T>
    {
        public T Value { get; private set; }

        public bool Successed { get; private set; }

        public IList<string> Errors { get; private set; }

        public Result(T value, bool successed, IList<string> errors)
        {
            Value = value;
            Successed = successed;
            Errors = errors;
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, true, null);
        }

        public static Result<T> Fail(IList<string> errorList)
        {
            return new Result<T>(default(T), false, errorList);
        }
    }
}
namespace ApplicationCore.Seedwork.Exceptions
{
    public class Result
    {
        private static readonly Result ResultOk = new(isError: false, error: null);
        private readonly ExceptionResult _exceptionResult;

        protected Result(bool isError, Error error)
        {
            _exceptionResult = new ExceptionResult(isError, error);
        }

        public bool IsError => _exceptionResult.IsError;
        public bool IsSuccess => _exceptionResult.IsSuccess;
        public Error Error => _exceptionResult.Error;

        public static Result Ok()
        {
            return ResultOk;
        }

        public static Result NotOk(Error error)
        {
            return new Result(isError: true, error);
        }
    }

    public sealed class Result<TSuccess> : Result
    {
        internal Result(bool isError, TSuccess isSuccess, Error error) : base(isError, error)
        {
            _isSuccess = isSuccess;
        }

        public readonly TSuccess _isSuccess;
        public TSuccess Success
        {
            get
            {
                return IsError ? throw new ArgumentException("Fail in return the success.") : _isSuccess;
            }
        }

        public static implicit operator Result<TSuccess>(TSuccess success)
        {
            return new Result<TSuccess>(false, success, null);
        }

        public static implicit operator Result<TSuccess>(Error error)
        {
            return new Result<TSuccess>(true, default, error);
        }
    }

    public readonly struct Result<TSuccess, TError>
    {
        internal Result(TSuccess success)
        {
            IsError = false;
            Success = success;
            Error = default;
        }

        internal Result(TError error)
        {
            IsError = true;
            Success = default;
            Error = error;
        }

        public TSuccess Success { get; }
        public TError Error { get; }
        public bool IsError { get; }
        public bool IsSuccess => !IsError;

        public static implicit operator Result<TSuccess, TError>(TError error)
        {
            return new Result<TSuccess, TError>(error);
        }

        public static implicit operator Result<TSuccess, TError>(TSuccess success)
        {
            return new Result<TSuccess, TError>(success);
        }

        public static Result<TSuccess, TError> NewSuccess(TSuccess success) => success;
        public static Result<TSuccess, TError> NewError(TError error) => error;

        public override bool Equals(object obj)
        {
            try
            {
                if (obj is not Result<TSuccess, TError>) return false;
                var other = (Result<TSuccess, TError>)obj;
                return IsSuccess
                    ? Success.Equals(other.Success)
                    : Error.Equals(other.Error);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return IsSuccess
                ? Success.GetHashCode()
                : Error.GetHashCode();
        }

        public static bool operator ==(Result<TSuccess, TError> left, Result<TSuccess, TError> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result<TSuccess, TError> left, Result<TSuccess, TError> right)
        {
            return !(left == right);
        }
    }
}

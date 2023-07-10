namespace ApplicationCore.Seedwork.Exceptions
{
    internal sealed class ExceptionResult
    {
        internal ExceptionResult(bool isError, Error error)
        {
            const int SecondLevelHResult = unchecked((int)0x81234567);
            string hResult = string.Format("(HRESULT:0x{1:X8}) {0}", "Error cannot to be null.", SecondLevelHResult);
            IsError = isError && error == null ? throw new ArgumentNullException(hResult) : isError;
            _error = !isError && error != null ? throw new ArgumentException("Error") : error;
        }

        private readonly Error _error;
        public bool IsError { get; }
        public bool IsSuccess => !IsError;

        public Error Error
        {
            get
            {
                return IsSuccess ? throw new ArgumentException("Fail in return the error.") : _error;
            }
        }
    }
}

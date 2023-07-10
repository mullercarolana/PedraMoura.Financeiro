namespace ApplicationCore.Seedwork.Exceptions
{
    public sealed class Error : ValueObject
    {
        private Error(string type, string message, DateTime date)
        {
            Type = type;
            Message = message;
            Date = date;
        }

        public string Type { get; }
        public string Message { get; }
        public DateTime Date { get; }

        public static Error New(string type, string message)
        {
            return new Error(type, message, DateTime.Now);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Message;
            yield return Date;
        }
    }
}

namespace DictionaryApp.Exceptions
{
    public class UncorrectStorageTimeFormatException : Exception
    {
        private const string message = "Storage time must be a not null positive integer";

        public UncorrectStorageTimeFormatException()
            : base(message)
        {
        }
    }
}

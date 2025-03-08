namespace BookStore.Common.Exceptions
{
    [Serializable]
    public abstract class CriticalException : Exception
    {
        public Guid TraceId { get; }

        protected CriticalException(string message, Guid traceId) : base(message)
        {
            TraceId = traceId;
        }
        protected CriticalException(string message, Exception innerException, Guid traceId)
        {
            TraceId = traceId;
        }
    }
}

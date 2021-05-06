using System;

namespace Deliveroo.Cron.Exceptions
{
    [Serializable]
    public class ExpressionParsingException : Exception
    {
        public ExpressionParsingException(string message) : base(message)
        { }

        public ExpressionParsingException(string message, Exception ex) : base(message, ex)
        { }
    }
}

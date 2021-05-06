using System;

namespace Deliveroo.Cron.Exceptions
{
    [Serializable]
    public class InvalidNotationArgumentException : Exception
    {
        public InvalidNotationArgumentException(string argument) : base(string.Format("Invalid argument provided : {0}", argument))
        { }
    }
}

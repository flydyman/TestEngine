using System;

namespace TestEngine.Exceptions
{
    [Serializable]
    public class WrongSourceException: Exception
    {
        public WrongSourceException() :base(){}
        public WrongSourceException(string message): base(message){}
        public WrongSourceException(string message, Exception innerException): base(message,innerException){}
    }
}
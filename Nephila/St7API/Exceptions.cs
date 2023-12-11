using System;

namespace Nephila.St7API.Exceptions
{
    [Serializable]
    public class Strand7Exception : Exception
    {
        public string ErrorType { get; } = "ERR7";
        public int ErrorCode { get; }
        public string ErrorMessage { get; }

        public Strand7Exception() { }

        public Strand7Exception(string message)
            : base(message) { }

        public Strand7Exception(string message, Exception inner)
            : base(message, inner) { }

        public Strand7Exception(string message, int errorCode)
            : this(message)
        {
            ErrorCode = errorCode;
            ErrorMessage = $"{ErrorType}-{errorCode}: {message}";
        }
    }

    [Serializable]
    public class Strand7SolverException : Exception
    {
        public string ErrorType { get; } = "SE";
        public int ErrorCode { get; }
        public string ErrorMessage { get; }
        
        public Strand7SolverException() { }

        public Strand7SolverException(string message)
            : base(message) { }

        public Strand7SolverException(string message, Exception inner)
            : base(message, inner) { }

        public Strand7SolverException(string message, int errorCode)
            : this(message)
        {
            ErrorCode = errorCode;
            ErrorMessage = $"{ErrorType}-{errorCode}: {message}";
        }
    }

    [Serializable]
    public class Strand7UnknownException : Exception
    {
        public string ErrorType { get; } = "UE";
        public int ErrorCode { get; }
        public string ErrorMessage { get; }

        public Strand7UnknownException() { }

        public Strand7UnknownException(string message)
            : base(message) { }

        public Strand7UnknownException(string message, Exception inner)
            : base(message, inner) { }

        public Strand7UnknownException(string message, int errorCode)
            : this(message)
        {
            ErrorCode = errorCode;
            ErrorMessage = $"{ErrorType}-{errorCode}: {message}";
        }
    }

}

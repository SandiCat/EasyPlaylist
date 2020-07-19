namespace Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class InvalidLoginInfoException : Exception
    {
        public InvalidLoginInfoException()
        {
        }

        public InvalidLoginInfoException(string message)
            : base(message)
        {
        }

        public InvalidLoginInfoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

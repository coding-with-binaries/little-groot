using System;
using System.Globalization;

namespace LittleGrootServer.Exceptions {
    public class LittleGrootRegistrationException : Exception {
        public LittleGrootRegistrationException() : base() { }

        public LittleGrootRegistrationException(string message) : base(message) { }

        public LittleGrootRegistrationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace HarUtilities.DirectoryPoller
{
    [Serializable]
    internal class DirectoryPollerException : Exception
    {
        public DirectoryPollerException() : this("An error occurred in the Directory Poller")
        {
        }

        public DirectoryPollerException(string message) : base(message)
        {
        }

        public DirectoryPollerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DirectoryPollerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
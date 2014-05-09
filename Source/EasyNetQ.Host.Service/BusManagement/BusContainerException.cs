using System;
using System.Runtime.Serialization;

namespace EasyNetQ.Host.Service.BusManagement
{
    public class BusContainerException : Exception
    {
        public BusContainerException()
        {
        }

        public BusContainerException(string message) : base(message)
        {
        }

        public BusContainerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusContainerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
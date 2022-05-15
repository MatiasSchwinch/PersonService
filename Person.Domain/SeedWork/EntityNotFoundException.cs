﻿using System.Runtime.Serialization;

namespace Person.Domain.SeedWork
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }

        public EntityNotFoundException(string? message) : base(message) { }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

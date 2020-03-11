using System;

namespace BackendTest.Domain.ValueObjects
{
    public class Adult : ValueObject
    {
        public bool Value { get; private set; }
        private Adult() { }
        public Adult(bool adult)
        {
            Value = adult;
        }

        public static implicit operator bool(Adult date) => date.Value;
    }
}
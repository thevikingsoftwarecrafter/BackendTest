using System;

namespace BackendTest.Domain.ValueObjects
{
    public class Size : ValueObject
    {
        public string Value { get; private set; }
        private Size() { }
        public Size(string size)
        {
            if (string.IsNullOrWhiteSpace(size))
                throw new ArgumentException("Size can't be empty or null");
            Value = size;
        }

        public static implicit operator string(Size size) => size.Value;
    }
}
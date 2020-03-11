using System;

namespace BackendTest.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; private set; }
        private Name() { }
        public Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name can't be empty or null");
            Value = name;
        }

        public static implicit operator string(Name name) => name.Value;
    }
}
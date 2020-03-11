using System;

namespace BackendTest.Domain.ValueObjects
{
    public class OriginalLanguage : ValueObject
    {
        public string Value { get; private set; }
        private OriginalLanguage() { }
        public OriginalLanguage(string originalLanguage)
        {
            if (string.IsNullOrWhiteSpace(originalLanguage))
                throw new ArgumentException("Original Language can't be empty or null");
            Value = originalLanguage;
        }

        public static implicit operator string(OriginalLanguage originalLanguage) => originalLanguage.Value;
    }
}
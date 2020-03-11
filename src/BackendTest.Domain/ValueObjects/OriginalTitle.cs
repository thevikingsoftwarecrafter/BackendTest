using System;

namespace BackendTest.Domain.ValueObjects
{
    public class OriginalTitle : ValueObject
    {
        public string Value { get; private set; }
        private OriginalTitle() { }
        public OriginalTitle(string originalTitle)
        {
            if (string.IsNullOrWhiteSpace(originalTitle))
                throw new ArgumentException("Original Title can't be empty or null");
            Value = originalTitle;
        }

        public static implicit operator string(OriginalTitle originalTitle) => originalTitle.Value;
    }
}
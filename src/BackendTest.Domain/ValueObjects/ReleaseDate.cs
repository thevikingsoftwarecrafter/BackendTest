using System;

namespace BackendTest.Domain.ValueObjects
{
    public class ReleaseDate : ValueObject
    {
        public DateTime Value { get; private set; }
        private ReleaseDate() { }
        public ReleaseDate(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("Release Date must have a valid date");
            Value = date;
        }

        public static implicit operator DateTime(ReleaseDate date) => date.Value;
    }
}
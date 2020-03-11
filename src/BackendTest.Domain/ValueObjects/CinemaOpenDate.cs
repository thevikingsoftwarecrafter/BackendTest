using System;

namespace BackendTest.Domain.ValueObjects
{
    public class CinemaOpenDate : ValueObject
    {
        public DateTime Value { get; private set; }
        private CinemaOpenDate() { }
        public CinemaOpenDate(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("Cinema Open Date must have a valid date");
            Value = date;
        }

        public static implicit operator DateTime(CinemaOpenDate date) => date.Value;
    }
}
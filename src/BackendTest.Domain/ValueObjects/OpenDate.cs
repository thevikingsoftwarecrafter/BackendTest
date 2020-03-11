using System;

namespace BackendTest.Domain.ValueObjects
{
    public class OpenDate : ValueObject
    {
        public DateTime Value { get; private set; }
        private OpenDate() { }
        public OpenDate(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("Open Date must have a valid date");
            Value = date;
        }

        public static implicit operator DateTime(OpenDate date) => date.Value;
    }
}
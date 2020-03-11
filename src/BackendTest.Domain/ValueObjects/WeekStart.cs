using System;

namespace BackendTest.Domain.ValueObjects
{
    public class WeekStart : ValueObject
    {
        public DateTime Value { get; private set; }
        private WeekStart() { }
        public WeekStart(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("Week Start must have a valid date");
            Value = date;
        }

        public static implicit operator DateTime(WeekStart date) => date.Value;
    }
}
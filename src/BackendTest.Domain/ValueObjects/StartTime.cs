using System;

namespace BackendTest.Domain.ValueObjects
{
    public class StartTime : ValueObject
    {
        public DateTime Value { get; private set; }
        private StartTime() { }
        public StartTime(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("Start Time must have a valid date");
            Value = date;
        }

        public static implicit operator DateTime(StartTime date) => date.Value;
    }
}
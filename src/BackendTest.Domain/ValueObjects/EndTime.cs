using System;

namespace BackendTest.Domain.ValueObjects
{
    public class EndTime : ValueObject
    {
        public DateTime Value { get; private set; }
        private EndTime() { }
        public EndTime(DateTime date)
        {
            if (date == default)
                throw new ArgumentException("End Time must have a valid date");
            Value = date;
        }

        public static implicit operator DateTime(EndTime date) => date.Value;
    }
}
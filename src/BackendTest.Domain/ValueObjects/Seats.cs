using System;

namespace BackendTest.Domain.ValueObjects
{
    public class Seats : ValueObject
    {
        public int Value { get; private set; }
        private Seats() { }
        public Seats(int seats)
        {
            if (seats <= 0)
                throw new ArgumentException("Seats must be greater than 0");
            Value = seats;
        }

        public static implicit operator int(Seats seats) => seats.Value;
    }
}
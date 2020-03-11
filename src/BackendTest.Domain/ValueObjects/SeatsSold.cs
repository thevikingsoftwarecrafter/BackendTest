using System;

namespace BackendTest.Domain.ValueObjects
{
    public class SeatsSold : ValueObject
    {
        public int? Value { get; private set; }
        private SeatsSold() { }
        public SeatsSold(int? seatsSold)
        {
            if (seatsSold <= 0)
                throw new ArgumentException("Seats Sold must be greater than 0");
            Value = seatsSold;
        }

        public static implicit operator int?(SeatsSold seatsSold) => seatsSold.Value;
    }
}
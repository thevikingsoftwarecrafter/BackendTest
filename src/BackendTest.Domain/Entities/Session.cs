using System;
using BackendTest.Domain.ValueObjects;

namespace BackendTest.Domain.Entities
{
    public partial class Session : Entity<int>
    {
        public StartTime StartTime { get; private set; }
        public EndTime EndTime { get; private set; }
        public SeatsSold SeatsSold { get; set; }

        public virtual Movie Movie { get; private set; }
        public virtual Room Room { get; private set; }

        private Session()
        {

        }

        public Session(StartTime starTime, EndTime endTime, SeatsSold seatsSold, Movie movie, Room room) : base(Guid.NewGuid().GetHashCode())
        {
            StartTime = starTime;
            EndTime = endTime;
            SeatsSold = seatsSold;
            Movie = movie;
            Room = room;
        }
    }
}

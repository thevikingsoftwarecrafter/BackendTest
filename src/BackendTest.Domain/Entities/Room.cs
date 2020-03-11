using System;
using System.Collections.Generic;
using System.Linq;
using BackendTest.Domain.ValueObjects;

namespace BackendTest.Domain.Entities
{
    public class Room : Entity<int>
    {
        public Name Name { get; private set; }

        public Size Size { get; private set; }

        public Seats Seats { get; private set; }

        public virtual Cinema Cinema { get; private set; }

        private readonly List<Session> _session = new List<Session>();

        public virtual ICollection<Session> Session => _session.ToList();

        private Room()
        {

        }

        public Room(Name name, Size size, Seats seats, Cinema cinema, List<Session> session) : base(Guid.NewGuid().GetHashCode())
        {
            Name = name;
            Size = size;
            Seats = seats;
            Cinema = cinema;
            _session = session;
        }
    }
}

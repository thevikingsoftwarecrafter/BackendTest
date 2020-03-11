using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BackendTest.Domain.ValueObjects;
using BackendTest.Infrastructure.Data.DBContext;

namespace BackendTest.Domain.Entities
{
    public class Cinema : Entity<int>
    {
        private Cinema()
        {
        }

        public Name Name { get; private set; }

        public OpenDate OpenSince { get; private set; }

        public virtual City City { get; private set; }

        private readonly List<Room> _room = new List<Room>();
        public virtual IReadOnlyList<Room> Room => _room.ToList();

        public Cinema(Name name, OpenDate openSince, City city, List<Room> room) : base(Guid.NewGuid().GetHashCode())
        {
            Name = name;
            OpenSince = openSince;
            City = city;
            _room = room;
        }
    }
}

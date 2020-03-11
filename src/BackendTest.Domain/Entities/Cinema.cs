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

        public CinemaOpenDate OpenSince { get; private set; }

        public City City { get; private set; }

        private readonly List<Room> _rooms = new List<Room>();
        public virtual IReadOnlyList<Room> Room => _rooms.ToList();

        public Cinema(Name name, CinemaOpenDate openSince, City city, List<Room> rooms) : base(Guid.NewGuid().GetHashCode())
        {
            Name = name;
            OpenSince = openSince;
            City = city;
            _rooms = rooms;
        }
    }
}

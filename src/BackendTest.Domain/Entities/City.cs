using System;
using System.Collections.Generic;
using System.Linq;
using BackendTest.Domain.Entities;
using BackendTest.Domain.ValueObjects;

namespace BackendTest.Infrastructure.Data.DBContext
{
    public class City : Entity<int>
    { 
        public Name Name { get; private set; }
        public Population Population { get; private set; }

        private readonly List<Cinema> _cinema = new List<Cinema>();
        public virtual IReadOnlyList<Cinema> Cinema => _cinema.ToList();

        private City()
        {

        }

        public City(Name name, Population population) : base(Guid.NewGuid().GetHashCode())
        {
            Name = name;
            Population = population;
        }
    }
}

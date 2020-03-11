using System;
using BackendTest.Domain.ValueObjects;

namespace BackendTest.Domain.Entities
{
    public class Genre : Entity<int>
    {
        public Name Name { get; private set; }

        private Genre()
        {

        }

        public Genre(Name name) : base(Guid.NewGuid().GetHashCode())
        {
            Name = name;
        }
    }
}

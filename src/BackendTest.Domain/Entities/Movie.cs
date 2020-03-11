using BackendTest.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackendTest.Domain.Entities
{
    public class Movie : Entity<int>
    {
        public OriginalTitle OriginalTitle { get; private set; }

        public ReleaseDate ReleaseDate { get; private set; }

        public OriginalLanguage OriginalLanguage { get; private set; }

        public Adult Adult { get; private set; }

        private readonly List<Session> _session = new List<Session>();

        public virtual IReadOnlyList<Session> Session => _session.ToList();

        private Movie()
        {

        }

        public Movie(OriginalTitle originalTitle, ReleaseDate releaseDate, OriginalLanguage originalLanguage, Adult adult, List<Session> session) : base(Guid.NewGuid().GetHashCode())
        {
            OriginalTitle = originalTitle;
            ReleaseDate = releaseDate;
            OriginalLanguage = originalLanguage;
            Adult = adult;
            _session = session;
        }
    }
}

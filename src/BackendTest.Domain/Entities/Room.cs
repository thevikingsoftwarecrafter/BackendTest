using System;
using System.Collections.Generic;

namespace BackendTest.Infrastructure.Data.DBContext
{
    public partial class Room
    {
        public Room()
        {
            Session = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int Seats { get; set; }
        public int CinemaId { get; set; }

        public virtual Cinema Cinema { get; set; }
        public virtual ICollection<Session> Session { get; set; }
    }
}

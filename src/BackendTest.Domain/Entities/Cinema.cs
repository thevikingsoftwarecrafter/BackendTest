using System;
using System.Collections.Generic;

namespace BackendTest.Infrastructure.Data.DBContext
{
    public partial class Cinema
    {
        public Cinema()
        {
            Room = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpenSince { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Room> Room { get; set; }
    }
}

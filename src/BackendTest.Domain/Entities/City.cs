using System;
using System.Collections.Generic;

namespace BackendTest.Infrastructure.Data.DBContext
{
    public partial class City
    {
        public City()
        {
            Cinema = new HashSet<Cinema>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }

        public virtual ICollection<Cinema> Cinema { get; set; }
    }
}

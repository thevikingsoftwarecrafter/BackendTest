using System;
using System.Collections.Generic;
using BackendTest.Domain.ValueObjects;

namespace BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class QueriedMovie
    {
        public OriginalTitle Title { get; }
        public string Overview { get; }
        public string Genre { get; }
        public OriginalLanguage Language { get; }
        public DateTime ReleaseDate { get; }
        public string WebSite { get; }
        public IReadOnlyList<string> Keywords { get; }
        public SeatsSold SeatsSold { get; }
        public Size Size { get; }

        public QueriedMovie(
            OriginalTitle title, 
            string overview, 
            string genre, 
            OriginalLanguage language, 
            DateTime releaseDate, 
            string webSite, 
            IReadOnlyList<string> keywords,
            SeatsSold seatsSold = null,
            Size size = null)
        {
            Title = title;
            Overview = overview;
            Genre = genre;
            Language = language;
            ReleaseDate = releaseDate;
            WebSite = webSite;
            Keywords = keywords;
            SeatsSold = seatsSold;
            Size = size;
        }
    }
}
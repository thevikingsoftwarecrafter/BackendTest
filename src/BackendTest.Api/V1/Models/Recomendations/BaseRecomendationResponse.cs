using System;
using System.Collections.Generic;

namespace BackendTest.Api.V1.Models.Recomendations
{
    public abstract class BaseRecomendationResponse
    {
        public string Title { get; set; }
        public string Overview { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string WebSite { get; set; }
        public List<string> Keywords { get; set; }
    }
}
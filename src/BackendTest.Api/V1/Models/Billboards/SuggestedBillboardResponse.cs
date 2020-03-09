using System;
using System.Collections.Generic;
using BackendTest.Api.V1.Models.Recomendations;

namespace BackendTest.Api.V1.Models.BillBoards
{
    public sealed class SuggestedBillboardResponse
    {
        public DateTime InitialDate { get; set; }
        public List<MovieOnScreenRecomendation> MoviesOnScreen { get; set; }
    }
}
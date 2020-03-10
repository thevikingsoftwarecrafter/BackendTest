using System;
using System.Collections.Generic;
using BackendTest.Api.V1.Models.Recomendations;

namespace BackendTest.Api.V1.Models.BillBoards
{
    public sealed class SuggestedBillboardResponse : BaseBillboardResponse
    {
        public List<MovieOnScreenRecomendation> MoviesOnScreen { get; set; }
    }
}
using System;
using System.Collections.Generic;
using BackendTest.Api.V1.Models.Recomendations;

namespace BackendTest.Api.V1.Models.BillBoards
{
    public sealed class IntelligentBillboardResponse : BaseBillboardResponse
    {
        public List<MovieOnScreenRecomendation> MoviesOnBigScreen { get; set; }
        public List<MovieOnScreenRecomendation> MoviesOnSmallScreen { get; set; }
    }
}
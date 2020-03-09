using BackendTest.Api.V1.Models.Recomendations;

namespace BackendTest.Api.V1.Models.BillBoards
{
    public sealed class MovieOnScreenRecomendation
    {
        public int Screen { get; set; }
        public MovieRecomendationResponse MovieRecomendation { get; set; }
    }
}
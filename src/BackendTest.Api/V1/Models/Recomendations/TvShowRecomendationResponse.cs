namespace BackendTest.Api.V1.Models.Recomendations
{
    public sealed class TvShowRecomendationResponse : BaseRecomendationResponse
    {
        public int Seasons { get; set; }
        public int Episodes { get; set; }
        public bool Concluded { get; set; }
    }
}
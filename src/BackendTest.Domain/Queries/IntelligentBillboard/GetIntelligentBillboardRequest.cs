namespace BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardRequest : QueryBase<GetIntelligentBillboardResponse>
    {
        public int PeriodOfTimeInDays { get; }
        public int BigScreens { get; }
        public int SmallScreens { get; }
        public bool City { get; }

        public GetIntelligentBillboardRequest(int periodOfTimeInDays, int bigScreens, int smallScreens, bool city)
        {
            PeriodOfTimeInDays = periodOfTimeInDays;
            BigScreens = bigScreens;
            SmallScreens = smallScreens;
            City = city;
        }
    }
}
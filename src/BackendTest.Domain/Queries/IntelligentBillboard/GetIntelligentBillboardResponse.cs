using System.Collections.Generic;
using System.Collections.ObjectModel;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Optional;

namespace BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardResponse
    {
        public Option<ReadOnlyCollection<BillboardLine>> Billboard { get; }

        public GetIntelligentBillboardResponse(Option<List<BillboardLine>> billboard)
        {
            Billboard = billboard.Map(x => x.AsReadOnly());
        }
    }
}
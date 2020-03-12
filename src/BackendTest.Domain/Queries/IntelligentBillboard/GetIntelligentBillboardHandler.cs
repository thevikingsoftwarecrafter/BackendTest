using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using BackendTest.Domain.Repositories;
using BackendTest.Domain.Services;
using BackendTest.Domain.ValueObjects;
using MediatR;
using Optional;

namespace BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardHandler : IRequestHandler<GetIntelligentBillboardRequest, GetIntelligentBillboardResponse>
    {
        private readonly IQueriedMoviesRepository _repository;
        private readonly IDateTimeService _dateTimeService;
        private Option<ReadOnlyCollection<QueriedMovie>> _queriedMovies;
        private GetIntelligentBillboardRequest _request;

        public GetIntelligentBillboardHandler(IQueriedMoviesRepository repository, IDateTimeService dateTimeService)
        {
            _repository = repository;
            _dateTimeService = dateTimeService;
        }

        public async Task<GetIntelligentBillboardResponse> Handle(GetIntelligentBillboardRequest request, CancellationToken cancellationToken)
        {
            _request = request;
            _queriedMovies = await GetQueriedMoviesFromSource();
            return _queriedMovies.Match(
                none: () => new GetIntelligentBillboardResponse(new List<BillboardLine>().SomeNotNull()),
                some: x => new GetIntelligentBillboardResponse(ObtainBillboard().ToList().SomeNotNull()));
        }

        private async Task<Option<ReadOnlyCollection<QueriedMovie>>> GetQueriedMoviesFromSource()
        {
            if (_request.City) return await _repository.GetAllMoviesFromCity();
            return await _repository.GetAllMovies();
        }

        private IEnumerable<BillboardLine> ObtainBillboard()
        {
            var weekStartDateTime = _dateTimeService.Now();
            int remainingDays = _request.PeriodOfTimeInDays;
            const int weekLength = 7;
            do
            {
                yield return new BillboardLine(
                    new WeekStart(weekStartDateTime),
                    null,
                    null);
                remainingDays -= weekLength;
                weekStartDateTime = weekStartDateTime.AddDays(weekLength);
            } while (remainingDays > 0);
        }
    }
}

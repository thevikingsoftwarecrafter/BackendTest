using System.Collections.Generic;
using System.Threading.Tasks;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Optional;

namespace BackendTest.Domain.Repositories
{
    public interface IQueriedMoviesRepository
    {
        Task<Option<IReadOnlyList<QueriedMovie>>> GetAllMovies();
        Task<Option<IReadOnlyList<QueriedMovie>>> GetAllMoviesFromCity();
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Optional;

namespace BackendTest.Domain.Repositories
{
    public interface IQueriedMoviesRepository
    {
        Task<Option<ReadOnlyCollection<QueriedMovie>>> GetAllMovies();
        Task<Option<ReadOnlyCollection<QueriedMovie>>> GetAllMoviesFromCity();
    }
}
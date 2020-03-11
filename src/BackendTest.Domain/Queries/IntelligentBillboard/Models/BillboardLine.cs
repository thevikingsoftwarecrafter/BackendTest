using System.Collections.Generic;
using BackendTest.Domain.Entities;
using BackendTest.Domain.ValueObjects;

namespace BackendTest.Domain.Queries.IntelligentBillboard.Models
{
    public class BillboardLine
    {
        public WeekStart WeekStart { get; }

        public IReadOnlyList<(Screen, QueriedMovie)> MoviesOnBigScreen { get; }
        public IReadOnlyList<(Screen, QueriedMovie)> MoviesOnSmallScreen { get; }

        public BillboardLine(
            WeekStart weekStart, 
            IReadOnlyList<(Screen, QueriedMovie)> moviesOnBigScreen,
            IReadOnlyList<(Screen, QueriedMovie)> moviesOnSmallScreen)
        {
            WeekStart = weekStart;
            MoviesOnBigScreen = moviesOnBigScreen;
            MoviesOnSmallScreen = moviesOnSmallScreen;
        }
    }
}
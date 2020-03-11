using System;
using BackendTest.Domain.Services;

namespace BackendTest.Api.Services
{
    public class DateTimeTimeService : IDateTimeService
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
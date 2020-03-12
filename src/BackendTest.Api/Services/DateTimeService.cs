using System;
using BackendTest.Domain.Services;

namespace BackendTest.Api.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}
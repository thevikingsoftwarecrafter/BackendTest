using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace BackendTest.Domain.Queries
{
    public class QueryBase<TResult> : IRequest<TResult> where TResult : class
    {
    }
}

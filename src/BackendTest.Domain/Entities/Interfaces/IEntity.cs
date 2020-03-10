using System;

namespace BackendTest.Domain.Entities.Interfaces
{
    public interface IEntity<T> : IEquatable<IEntity<T>>
    {
        T Id { get; }
    }
}
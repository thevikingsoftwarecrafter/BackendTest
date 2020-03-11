using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using BackendTest.Domain.ValueObjects.Interfaces;

namespace BackendTest.Domain.ValueObjects
{
    public abstract class ValueObject : IValueObject
    {
        public override bool Equals([AllowNull] object obj)
        {
            if (obj == null) return false;

            var t = GetType();
            var otherType = obj.GetType();
            if (t != otherType) return false;

            var other = obj as IValueObject;
            return Equals(other);
        }

        public virtual bool Equals(IValueObject other)
        {
            if (other == null) return false;

            var properties = GetProperties();

            return properties.All(property => SameValues(property.GetValue(other), property.GetValue(this)));
        }

        private bool SameValues(object value1, object value2)
        {
            if (value1 == null && value2 != null) return false;
            return value1?.Equals(value2) ?? true;
        }

        public override int GetHashCode()
        {
            var properties = GetProperties();

            const int startValue = 17;
            const int multiplier = 59;

            return properties.Select(
                    property => property.GetValue(this))
                .Where(value => value != null)
                .Aggregate(
                    startValue, (current, value) => current * multiplier + value.GetHashCode()
                );
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            var t = GetType();
            return t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        }

        public static bool operator ==(ValueObject x, ValueObject y)
        {
            if (x is null) return y is null;
            return x.Equals(y);
        }

        public static bool operator !=(ValueObject x, ValueObject y)
        {
            return !(x == y);
        }
    }
}
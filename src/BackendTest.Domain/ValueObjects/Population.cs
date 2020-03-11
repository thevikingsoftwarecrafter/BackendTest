using System;

namespace BackendTest.Domain.ValueObjects
{
    public class Population : ValueObject
    {
        public int Value { get; private set; }
        private Population() { }
        public Population(int population)
        {
            if (population <= 0)
                throw new ArgumentException("Population must be greater than 0");
            Value = population;
        }

        public static implicit operator int(Population population) => population.Value;
    }
}
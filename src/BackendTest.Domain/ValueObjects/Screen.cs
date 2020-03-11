using System;

namespace BackendTest.Domain.ValueObjects
{
    public class Screen : ValueObject
    {
        public int Value { get; private set; }
        private Screen() { }
        public Screen(int screen)
        {
            if (screen <= 0)
                throw new ArgumentException("Screen must be greater than 0");
            Value = screen;
        }

        public static implicit operator int(Screen screen) => screen.Value;
    }
}
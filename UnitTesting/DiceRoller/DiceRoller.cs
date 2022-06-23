using System;

namespace UnitTesting.DiceRoller
{
    public class DiceRoller : IDiceRoller
    {
        public int RollD20()
        {
            return new Random().Next(20);
        }
    }
}

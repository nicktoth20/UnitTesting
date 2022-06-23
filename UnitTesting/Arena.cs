using UnitTesting.DiceRoller;
using UnitTesting.Repository;

namespace UnitTesting
{
    public class Arena
    {
        private readonly ICharactersRepository _charactersRepository;
        private readonly IDiceRoller _diceRoller;

        public Arena(
            ICharactersRepository charactersRepository,
            IDiceRoller diceRoller)
        {
            _charactersRepository = charactersRepository;
            _diceRoller = diceRoller;
        }

        public int Fight(int firstCharacterId, int secondCharacterId)
        {
            var firstCharacter = _charactersRepository.RetrieveCharacter(firstCharacterId);
            var secondCharacter = _charactersRepository.RetrieveCharacter(secondCharacterId);

            while (firstCharacter.Health > 0 && secondCharacter.Health > 0)
            {
                var attackValue = _diceRoller.RollD20() + firstCharacter.Attack;
                if (attackValue >= secondCharacter.Defense)
                {
                    secondCharacter.Health -= firstCharacter.Attack;
                }

                if (secondCharacter.Health > 0)
                {
                    attackValue = _diceRoller.RollD20() + secondCharacter.Attack;
                    if (attackValue >= firstCharacter.Defense)
                    {
                        firstCharacter.Health -= secondCharacter.Attack;
                    }
                }
            }

            return firstCharacter.Health > 0 ? firstCharacterId : secondCharacterId;
        }
    }
}

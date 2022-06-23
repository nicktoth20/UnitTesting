using System.Collections.Generic;
using System.Linq;

namespace UnitTesting.Repository
{
    public class CharactersRepository : ICharactersRepository
    {
        private List<Character> _characters = new List<Character>
        {
            new Character
            {
                Id = 1,
                Name = "Gideon",
                Health = 35,
                Attack = 9,
                Defense = 17
            },
            new Character
            {
                Id = 2,
                Name = "Daekas",
                Health = 52,
                Attack = 7,
                Defense = 15
            }
        };

        public Character RetrieveCharacter(int id)
        {
            return _characters.First(c => c.Id == id);
        }
    }
}

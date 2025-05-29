using static DataAccess.CharacterData;

namespace DataAccess
{
    public class CharacterData
    {
        public class CharacterWrapper
        {
            public List<DomainCharacter> Characters { get; set; }
        }

        public class DomainCharacter
        {
            public string name { get; set; }
            public string name_ja { get; set; }
            public string domain { get; set; }
            public List<string> weekday { get; set; }
            public string material { get; set; }
            public int rarity { get; set; }

            public DomainCharacter() { }

            public DomainCharacter(string name, string name_ja, string domain, List<string> weekday, string material, int rarity)
            {
                this.name = name;
                this.name_ja = name_ja;
                this.domain = domain;
                this.weekday = weekday;
                this.material = material;
                this.rarity = rarity;
            }
        }
    }

    public class Search
    {
        public static List<DomainCharacter> SearchCharacters(List<DomainCharacter> all, string keyword)
        {
            return all
                .Where(c => c.name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
    }
}

using Newtonsoft.Json;

namespace JsonFileIO.Jsons
{
    [JsonObject("CharacterJson")]
    public sealed class CharacterJson
    {
        [JsonProperty("name")]
        public string Name { get; set; }    // 名前

        [JsonProperty("rarity")]
        public int rarity { get; set; }
    }
}

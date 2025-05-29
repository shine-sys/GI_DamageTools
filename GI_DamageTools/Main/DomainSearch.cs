using Newtonsoft.Json;
using System.Globalization;
using Terminal.Gui;
using System.Text;
using static GI_DamageTools.Core.DamageCalculator;

namespace GI_DamageTools.Core
{
    public class DomainSearch
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

            public class Program
            {
                private const string jsonUrl = "https://shine-sys.github.io/GI_Json/character.json";

                [STAThread()]
                public async Task StartCalculation()
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine(" ");
                    Console.WriteLine("原神育成素材計算ツール (GitHub連携版) - Created by Ashika\n");
                    Console.WriteLine("---------------------------------------------------------\n");

                    List<DomainCharacter> allCharacters = await LoadCharactersFromGitHub();

                    Console.Write("キャラ名を入力してください: ");
                    string keyword = Console.ReadLine().Trim();
                    List<DomainCharacter> matched = SearchCharacters(allCharacters, keyword);

                    DomainCharacter selectedCharacter;
                    if (matched.Count == 0)
                    {
                        Console.WriteLine("⚠ 該当キャラが見つかりません。手動で続けます。");
                        selectedCharacter = new DomainCharacter();
                    }
                    else if (matched.Count == 1)
                    {
                        selectedCharacter = matched[0];
                        Console.WriteLine($"✅ {selectedCharacter.name_ja}（★ {selectedCharacter.rarity}）が見つかりました。");
                    }
                    else
                    {
                        selectedCharacter = SelectCharacterFromCandidates(matched);
                    }

                    string result = "\n=== 結果 ===\n" +
                                    $"キャラ名　　：{selectedCharacter.name_ja}\n" +
                                    $"レアリティ　：★ {selectedCharacter.rarity}\n" +
                                    $"必要素材　  ：{selectedCharacter.material + " の 教え / 導き / 哲学 "}\n" +
                                    $"天賦秘境名　：{selectedCharacter.domain ?? "不明"}\n" +
                                    $"開放曜日　　：{string.Join(" ・ ", selectedCharacter.weekday ?? new List<string> { "不明" })}\n";

                    Console.Write(result);

                    string date = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    string fileName = $"Result_{date}_{selectedCharacter.name_ja}_{selectedCharacter.domain}_天賦素材.txt";

                    try
                    {
                        File.WriteAllText(fileName, result);
                        Console.WriteLine($"✅ 計算結果を「{fileName}」に保存しました。");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠ ファイル保存中にエラーが発生しました: {ex.Message}");
                    }

                    Console.WriteLine("\nEnterキーを押して続行...");
                    Console.ReadLine();
                }

                public static async Task<List<DomainCharacter>> LoadCharactersFromGitHub()
                {
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            string json = await client.GetStringAsync(jsonUrl);
                            CharacterWrapper wrapper = JsonConvert.DeserializeObject<CharacterWrapper>(json);
                            return wrapper?.Characters ?? new List<DomainCharacter>();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ キャラデータの読み込み中にエラーが発生しました: {ex.Message}");
                            return new List<DomainCharacter>();
                        }
                    }
                }

                public static List<DomainCharacter> SearchCharacters(List<DomainCharacter> all, string keyword)
                {
                    return all
                        .Where(c => c.name.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToList();
                }

                public static DomainCharacter SelectCharacterFromCandidates(List<DomainCharacter> candidates)
                {
                    Console.WriteLine("\n複数の候補が見つかりました:");
                    for (int i = 0; i < candidates.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {candidates[i].name_ja}（★ {candidates[i].rarity}）");
                    }

                    Console.Write("番号を選択してください: ");
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out int selectedIndex) &&
                            selectedIndex >= 1 &&
                            selectedIndex <= candidates.Count)
                        {
                            return candidates[selectedIndex - 1];
                        }
                        Console.Write("❌ 無効な番号です。再入力してください: ");
                    }
                }
            }
        }
    }
}

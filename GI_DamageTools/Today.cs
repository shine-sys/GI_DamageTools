using Newtonsoft.Json;
using System.Globalization;
using Terminal.Gui;
using System.Text;
using static GI_Tools.DamageCalculator;

namespace GI_Tools
{   
    public class Today
    {
        public class DomainCharacter
        {
            public string name { get; set; }
            public string name_ja { get; set; }
            public string domain { get; set; }
            public List<string> weekday { get; set; }
            public string material { get; set; }
            public int rarity { get; set; }
        }

        public class CharacterWrapper
        {
            public List<DomainCharacter> Characters { get; set; }
        }

        public class Program
        {
            private const string jsonUrl = "https://shine-sys.github.io/GI_Json/character.json";

            public async Task StartCalculation()
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("開放秘境照会 - Created by Ashika\n");
                Console.WriteLine("---------------------------------------------------------\n");

                var allCharacters = await LoadCharactersFromGitHub();

                var todayDomains = SearchToday(allCharacters);

                if (todayDomains.Count == 0)
                {
                    Console.WriteLine("本日開放されている秘境はありません。");
                }
                else
                {
                    Console.WriteLine("🔎 本日開放されている秘境一覧：\n");

                    var sb = new System.Text.StringBuilder();

                    foreach (var domain in todayDomains)
                    {
                        string line = $"・{domain.domain}（{domain.name_ja} / ★ {domain.rarity}） - 必要素材: {domain.material}";
                        Console.WriteLine(line);
                        sb.AppendLine(line);
                    }

                    // 日付でファイル名生成
                    string dateStr = DateTime.Now.ToString("yyyy-MM-dd");
                    string fileName = $"TodayDomains_{dateStr}.txt";

                    try
                    {
                        File.WriteAllText(fileName, sb.ToString());
                        Console.WriteLine($"\n✅ 計算結果を「{fileName}」に保存しました。");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠ ファイル保存中にエラーが発生しました: {ex.Message}");
                    }
                }

                Console.WriteLine("\nEnterキーを押して終了...");
                Console.ReadLine();
            }

            public static async Task<List<DomainCharacter>> LoadCharactersFromGitHub()
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string json = await client.GetStringAsync(jsonUrl);
                        var wrapper = JsonConvert.DeserializeObject<CharacterWrapper>(json);
                        return wrapper?.Characters ?? new List<DomainCharacter>();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ キャラデータの読み込み中にエラーが発生しました: {ex.Message}");
                        return new List<DomainCharacter>();
                    }
                }
            }

            public static List<DomainCharacter> SearchToday(List<DomainCharacter> allDomains)
            {
                string today = DateTime.Now.ToString("dddd", new CultureInfo("ja-JP"));
                return allDomains
                    .Where(domain => domain.weekday != null && (domain.weekday.Contains(today) || domain.weekday.Contains("日曜日")))
                    .ToList();
            }
        }
    }
}


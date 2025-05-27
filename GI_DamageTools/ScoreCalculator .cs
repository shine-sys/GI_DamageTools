using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI_Tools
{
    public class ScoreCalculator
    {
        public class Program
        {
            private const string jsonUrl = "https://shine-sys.github.io/GI_Json/character.json";

            [STAThread()]
            public async Task StartCalculation()
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;

                Console.WriteLine("原神聖遺物スコア簡易計算ツール - Created by Ashika\n");
                Console.WriteLine("---------------------------------------------------------\n");

                double critDamage = ReadPercent("会心ダメージを入力してください（例: 140% または 1.4）:");
                double critRate = ReadPercent("会心率を入力してください（例: 70% または 0.7）:");
                double baseAttack = ReadPercent("攻撃力を入力してください:");

                // Step 1:聖遺物スコア計算
                double critMultiplier = (critDamage + critRate * 2 + baseAttack) * 10 / 10 ;

                string result = "\n=== 結果 ===\n" +
                                $"結果　：{critMultiplier + "%" }\n";

                Console.Write(result);

                // 日付取得（例：2025-05-25）
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

                // ファイル名作成（例：Result_2025-05-25_xx.txt）
                string fileName = $"Result_{date}_artifact.txt";

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

            // 汎用的な数値入力処理
            static double ReadDouble(string prompt)
            {
                double value;
                while (true)
                {
                    Console.Write(prompt + " ");
                    string input = Console.ReadLine().Trim();
                    if (double.TryParse(input, out value))
                        return value;
                    Console.WriteLine("⚠ 数字を正しく入力してください。");
                }
            }

            static double ReadPercent(string prompt)
            {
                double value;
                while (true)
                {
                    Console.Write(prompt + " ");
                    string input = Console.ReadLine().Trim();
                    try
                    {
                        value = ParsePercent(input);
                        return value;
                    }
                    catch
                    {
                        Console.WriteLine("⚠ パーセント形式（例：46.6% や 0.466）で入力してください。");
                    }
                }
            }

            // パーセント入力を処理
            static double ParsePercent(string input)
            {
                if (input.EndsWith("%"))
                {
                    input = input.Replace("%", "").Trim();
                    if (double.TryParse(input, out double value))
                        return value / 100.0;
                }
                else if (double.TryParse(input, out double value))
                {
                    return value;
                }
                throw new FormatException("無効なパーセント形式です。");
            }
        }
    }
}

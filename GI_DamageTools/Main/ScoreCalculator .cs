using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI_DamageTools.Core
{
    public class ScoreCalculator
    {
        public class Program
        {
            [STAThread()]
            public async Task StartCalculation()
            {
                Console.OutputEncoding = Encoding.UTF8;

                Console.WriteLine(" ");
                Console.WriteLine("原神聖遺物スコア簡易計算ツール (攻撃)- Created by Ashika\n");
                Console.WriteLine("---------------------------------------------------------\n");

                double critDamage = ReadPercent("会心ダメージを%で入力してください（例: 140% または 1.4）:");
                double critRate = ReadPercent("会心率を%で入力してください（例: 70% または 0.7）:");
                double baseAttack = ReadPercent("攻撃力を%で入力してください:");
                double def = ReadPercent("防御力を%で入力してください:");
                double hp = ReadPercent("HPを%で入力してください:");
                double charge = ReadPercent("元素チャージを%で入力してください:");
                double jyukuti = ReadDouble("元素熟知を入力してください:");

                // Step 1:聖遺物スコア計算
                double critMultiplier = (critDamage + critRate * 2 + baseAttack) * 10 / 10 ;

                double defMultiplier = (critDamage + critRate * 2 + def) * 10 / 10;

                double hpMultiplier = (critDamage + critRate * 2 + hp) * 10 / 10;

                double chargeMultiplier = (critDamage + critRate * 2 + charge) * 10 / 10;

                double jyukutiMultiplier = (critDamage + critRate * 2 + jyukuti * 0.25) * 10 / 10;

                string result = "\n=== 結果 ===\n" +
                                $"攻撃スコア　：{critMultiplier + "%" }\n"+
                                $"防御スコア　：{defMultiplier + "%" }\n"+
                                $"HPスコア　：{hpMultiplier + "%"}\n"+
                                $"元素チャージスコア　：{chargeMultiplier + "%"}\n"+
                                $"元素熟知　：{jyukutiMultiplier + "%"}\n";

                Console.Write(result);

                // 日付取得（例：2025-05-25）
                string date = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

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
                    Console.WriteLine("スキップします...");
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

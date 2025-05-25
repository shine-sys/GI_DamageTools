using System;
using System.IO;

namespace GenshinDamageCalculator
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("原神ダメージ計算ツール Test Version Created By Ashika\r");
            Console.WriteLine("------------------------\n");

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double baseAttack = ReadDouble("基礎攻撃力を入力してください:");
            double additionalAttack = ReadDouble("攻撃力加算値を入力してください:");
            double critRate = ReadPercent("会心率を入力してください（例: 70% または 0.7）:");
            double critDamage = ReadPercent("会心ダメージを入力してください（例: 140% または 1.4）:");
            double dmgBonus = ReadPercent("ダメージバフ（属性/会心/通常など）を入力してください（例: 46.6% または 0.466）:");
            double multiplier = ReadDouble("天賦倍率（％）を入力してください（例：250 → 2.5）:");

            double enemyDefense = ReadDouble("敵のレベルを入力してください:");
            double characterLevel = ReadDouble("キャラのレベルを入力してください:");
            double defReduction = ReadPercent("防御ダウン（％）を入力してください（例: 60% → 0.6）:");
            double defIgnore = ReadPercent("防御無視（％）を入力してください（例: 30% → 0.3）:");

            double enemyResistance = ReadPercent("敵の耐性（例: 10% → 0.1, -20% → -0.2）:");
            double resReduction = ReadPercent("耐性ダウン（％）を入力してください（例: 20% → 0.2）:");

            // Step 1: 合計攻撃力
            double totalAttack = baseAttack + additionalAttack;

            // Step 2: 防御補正
            double defMultiplier = (characterLevel + 100) /
                ((1 - defReduction) * (1 - defIgnore) * (enemyDefense + 100) + (characterLevel + 100));

            // Step 3: 耐性補正
            double resistance = enemyResistance - resReduction;
            double resMultiplier = resistance < 0
                ? 1 - resistance / 2
                : resistance < 0.75
                    ? 1 - resistance
                    : 1 / (4 * resistance + 1);

            // Step 4: 会心なし、あり、期待値
            double critMultiplier = 1 + critDamage;
            double nonCritDamage = totalAttack * multiplier * (1 + dmgBonus) * defMultiplier * resMultiplier;
            double critDamageValue = nonCritDamage * critMultiplier;
            double expectedDamage = nonCritDamage * (1 + critRate * critDamage);

            string result = "\n=== ダメージ結果 ===\n" +
                            $"・会心なしダメージ　：{Math.Floor(nonCritDamage)}\n" +
                            $"・会心ありダメージ　：{Math.Floor(critDamageValue)}\n" +
                            $"・ダメージ期待値　　：{Math.Floor(expectedDamage)}\n";

            Console.WriteLine(result);

            // ファイルに書き出し
            try
            {
                File.WriteAllText("damage_result.txt", result);
                Console.WriteLine("✅ 計算結果を「damage_result.txt」に保存しました。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ ファイル保存中にエラーが発生しました: {ex.Message}");
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

            // パーセント対応の数値入力処理
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


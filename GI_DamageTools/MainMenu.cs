//参考 https://systemcraft.biz/archives/1098
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace GI_Tools
{
    public class MenuConsole
    {
        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                writelogo();
                Console.WriteLine("╔═══════════════════════════════════════╗");
                Console.WriteLine("║    原神ダメージ計算ツール - メニュー  ║");
                Console.WriteLine("╠═══════════════════════════════════════╣");
                Console.WriteLine("║ 1. ダメージ計算を開始する             ║");
                Console.WriteLine("║ 2. 聖遺物スコア計算を開始する         ║");
                Console.WriteLine("║ 3. クレジットを見る                   ║");
                Console.WriteLine("║ 0. 終了する                           ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.WriteLine(" ");
                Console.Write("選択肢を入力してください: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DamageCalculator.Program calculator = new DamageCalculator.Program();
                        calculator.StartCalculation().Wait();
                        break;
                    case "2":
                        ScoreCalculator.Program Scorecalculator = new ScoreCalculator.Program();
                        Scorecalculator.StartCalculation().Wait();
                        break;
                    case "3":
                        ShowCredits();
                        break;
                    case "0":
                        Console.WriteLine("\nアプリを終了します。お疲れ様でした。\n");
                        return;
                    default:
                        Console.WriteLine("⚠ 無効な選択肢です。Enterで再表示します...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ShowCredits()
        {
            Console.Clear();
            Console.WriteLine("=== クレジット ===");
            Console.WriteLine("制作: Ashika");
            Console.WriteLine("ツール名: 原神計算ツール");
            Console.WriteLine("GitHub: https://github.com/shine-sys/GI_DamageTools");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("ライブラリ 1: Newtonsoft.Json ");
            Console.WriteLine("制作者: James Newton-King");
            Console.WriteLine("GitHub: https://github.com/JamesNK/Newtonsoft.Json");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("ライブラリ 2: Terminal.GUI ");
            Console.WriteLine("制作者: gui.cs");
            Console.WriteLine("GitHub: https://github.com/gui-cs/Terminal.Gui");
            Console.WriteLine("\nEnterで戻ります...");
            Console.ReadLine();
        }

        public static void writelogo()
        {
            string logo = @" 
                                                                                                                      
                                                                                                                      
    //   ) )                                                                   /__  ___/                              
   //            ___         __        ___       / __       ( )       __         / /         ___        ___       //  
  //  ____     //___) )   //   ) )   ((   ) )   //   ) )   / /     //   ) )     / /        //   ) )   //   ) )   //   
 //    / /    //         //   / /     \ \      //   / /   / /     //   / /     / /        //   / /   //   / /   //    
((____/ /    ((____     //   / /   //   ) )   //   / /   / /     //   / /     / /        ((___/ /   ((___/ /   //     
";


            Console.WriteLine(logo);
        }
    }
}

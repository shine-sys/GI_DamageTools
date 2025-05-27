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
                Console.WriteLine(" 原神 計算ツール ");
                Console.WriteLine(" ");
                Console.WriteLine(" Created by Ashika(shine-sys) バージョン 1.0 ");
                Console.WriteLine(" ");
                Console.WriteLine(" コマンドを確認する場合は、HELP と入力してください。 ");
                Console.WriteLine(" ");

                Console.Write("> ");
                string input = Console.ReadLine().Trim().ToUpper();

                switch (input)
                {
                    case "HELP":
                        ShowHelp();
                        break;
                    case "RUN DAMAGE":
                        Console.Clear();
                        DamageCalculator.Program calculator = new DamageCalculator.Program();
                        calculator.StartCalculation().Wait();
                        break;
                    case "RUN SCORE":
                        Console.Clear();
                        ScoreCalculator.Program Scorecalculator = new ScoreCalculator.Program();
                        Scorecalculator.StartCalculation().Wait();
                        break;
                    case "SHOW CREDITS":
                        ShowCredits();
                        break;
                    case "EXIT":
                    case "QUIT":
                        Console.WriteLine("\nアプリを終了します...\n");
                        return;
                    default:
                        Console.WriteLine("⚠ 無効なコマンドです。Enterで再表示します...");
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
            Console.WriteLine("\nEnterで戻ります...");
            Console.ReadLine();
        }

        private void ShowHelp()
        {
            Console.Clear();
            Console.WriteLine("=== HELP ===");
            Console.WriteLine("ダメージ計算を行う     : RUN DAMAGE ");
            Console.WriteLine("聖遺物スコア計算を行う : RUN SCORE");
            Console.WriteLine("クレジットを表示する   : SHOW CREDITS");
            Console.WriteLine("終了する               : EXIT or QUIT");
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

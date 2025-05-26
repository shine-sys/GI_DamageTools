using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI_DamageTools
{
    public class Menu
    {
        public static void SelectMenu()
        {
            Console.Clear();
            writelogo();
            Console.WriteLine("[");
            Console.WriteLine("1");
            Console.WriteLine("] ダメージ計算");
        }

        public static void writelogo()
        {
            string logo = @" _  _ 
                                                                                                                      
                                                                                                                      
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

using CsvHelper.Configuration.Attributes;
using GI_DamageTools.Core;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Terminal.Gui;

namespace GI_Tools.Core
{
    class Menu()
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MenuConsole menu = new MenuConsole();
            menu.ShowMainMenu();
        }
    }
}


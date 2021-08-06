using System;
using System.IO;

namespace ProceduralMapGenerator
{
    class Program
    {
        const int MAP_WIDTH = 60;
        const int MAP_HEIGHT = 30;
        const int INITIAL_DENSITY = 45;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Clear();

            MapBrowser();

        }

        static void MapBrowser()
        {

            Map map = new Map(MAP_WIDTH, MAP_HEIGHT, INITIAL_DENSITY);
            map.Init();

            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.N:
                        map.Reset();
                        break;
                    case ConsoleKey.OemPlus:
                        map.ChangeDensity(1);
                        break;
                    case ConsoleKey.OemMinus:
                        map.ChangeDensity(-1);
                        break;
                    case ConsoleKey.S:
                        SaveMap(map.MapToString());
                        break;
                }
                map.Draw();
                DisplayMenu(map.Density);

            }

        }

        static void DisplayMenu(int d)
        {
            Console.SetCursorPosition(MAP_WIDTH + 3, 0);
            Console.Write("Controls: ");
            Console.SetCursorPosition(MAP_WIDTH + 3, 1);
            Console.Write(" [N] - next map.");
            Console.SetCursorPosition(MAP_WIDTH + 3, 2);
            Console.Write(" [+/-] - change fill density.");
            Console.SetCursorPosition(MAP_WIDTH + 3, 4);
            Console.Write(" [S] - save map to file. ");
            Console.SetCursorPosition(MAP_WIDTH + 3, 5);
            Console.Write(" [Esc] - exit app. ");
            Console.SetCursorPosition(MAP_WIDTH + 3, 3);
            Console.Write("Fill density: {0}     ", d);
        }

        static void SaveMap(string map)
        {
            string fileName = string.Empty;

            Console.Clear();
            Console.WriteLine("Give file name: \n");
            fileName = Console.ReadLine();

            fileName += ".txt";

            WriteToFile(fileName, map);

        }

        static void WriteToFile(string fileName, string content)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    throw new ArgumentException("File with this name already exists!");
                }
                
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("New map saved at {0}", DateTime.Now.ToString());
                    sw.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }
    }
}

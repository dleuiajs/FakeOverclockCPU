using System;

namespace OverclockCPU // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ResetColors();
            Random rnd = new Random();
            bool enterPressed = false;
            int lastNum = -1;

            List<string> FSBModes = new List<string>() { "100 MHz", "110 MHz", "120 MHz", "130 MHz", "140 MHz", "150 MHz", "200 MHz", "300 MHz", "400 MHz", "500 MHz", "1000 MHz", "Back" };
            List<string> MultiplierModes = new List<string>() { "40x", "41x", "42x", "43x", "44x", "45x", "50x", "75x", "100x", "150x", "200x", "Back" };

            List<int> FSBInts = new List<int>() { 100, 110, 120, 130, 140, 150, 200, 300, 400, 500, 1000 };
            List<int> MultiplierInts = new List<int>() { 40, 41, 42, 43, 44, 45, 50, 75, 100, 150, 200 };

            int multiplierD = 42;
            int fsbD = 100;

            int temperature = 21;

            Console.WriteLine("Enter the name of the processor");
            string? cpuName = Console.ReadLine();
            Console.Clear();
            while (true)
            {
                int multiplier = rnd.Next(multiplierD - 1, multiplierD + 2);
                double fsb = rnd.Next((fsbD - 1) * 1000, (fsbD + 2) * 1000) / 1000.0;
                string fsbString = fsb.ToString("F1");
                string frequency = (fsb * multiplier).ToString("F1");

                if (multiplierD <= 42 && fsbD <= 100)
                {
                    temperature = 21 + Convert.ToInt32(fsb * multiplier / 200);
                }
                else
                {
                    temperature += Convert.ToInt32(fsb * multiplier / 200);
                }
                if (Console.BufferHeight > 7) // Проверка, что буфер достаточно большой
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("[Current CPU]       ");
                    Console.SetCursorPosition(0, 1);
                    Console.Write("CPU: " + cpuName); //Intel(R) Core(TM) i3-10105F
                    Console.SetCursorPosition(0, 2);
                    Console.Write("CPU Clock: " + frequency + " MHz");
                    Console.SetCursorPosition(0, 3);
                    Console.Write("CPU Clock Ratio: " + multiplier + "x");
                    Console.SetCursorPosition(0, 4);
                    Console.Write("CPU Frequency: " + fsbString + " MHz ");
                    Console.SetCursorPosition(0, 5);
                    Console.Write("CPU Temperature: " + temperature + " °C ");
                    Console.SetCursorPosition(0, 7);
                    Console.Write("Program by dleuiajs (tiktok: @eeglebguy)");
                }
                else
                {
                    Console.WriteLine("The size of your window is too small, make it bigger.");
                }
                if (Console.KeyAvailable) // если игрок куда-то нажал
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.E)
                    {
                        string fsbDString = fsbD.ToString("N1");
                        string frequencyD = (multiplierD * fsbD).ToString("F1");

                        Console.Clear();
                        Console.SetCursorPosition(0, 0);
                        Console.Write("[CPU Overclocking]");
                        Console.SetCursorPosition(0, 1);
                        Console.Write("CPU Clock: " + frequencyD + " MHz");
                        Console.SetCursorPosition(0, 2);
                        Console.Write("CPU Clock Ratio: " + multiplierD + "x");
                        Console.SetCursorPosition(0, 3);
                        Console.Write("CPU Frequency: " + fsbDString + " MHz ");
                        while (true)
                        {
                            Selector("", new List<string>() { "CPU Configuration", "Back" }, 5);
                            if (enterPressed)
                            {
                                enterPressed = false;
                                if (lastNum == 0)
                                {
                                    Selector("[CPU Configuration]", new List<string>() { "CPU Clock Ratio", "CPU Frequency", "CPU Voltage", "Back" }, 1);
                                    if (lastNum == 0)
                                    {
                                        Selector("Select CPU Clock Ratio:", MultiplierModes, 1);
                                        if (lastNum < 11)
                                        {
                                            multiplierD = MultiplierInts[lastNum];
                                            Console.Clear();
                                            Console.WriteLine("Applying Settings...");
                                            break;
                                        }
                                        else if (lastNum == 11)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Going Back...");
                                            break;
                                        }
                                    }
                                    else if (lastNum == 1)
                                    {
                                        Selector("Select CPU Frequency:", FSBModes, 1);
                                        if (lastNum < 11)
                                        {
                                            fsbD = FSBInts[lastNum];
                                            Console.Clear();
                                            Console.WriteLine("Applying Settings...");
                                            break;
                                        }
                                        else if (lastNum == 11)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Going Back...");
                                            break;
                                        }
                                    }
                                    else if (lastNum >= 2)
                                    {
                                        Console.Clear();
                                        break;
                                    }
                                }
                                else if (lastNum == 1)
                                {
                                    Console.Clear();
                                    break;
                                }
                            }

                        }
                    }
                }

                // if (lastNum == -1)
                // {
                Thread.Sleep(1000);
                // }
                // else if (lastNum != -1)
                // {
                //     lastNum = -1;
                // }
            }

            void Selector(string firstText, List<string> texts, int row)
            {
                if (firstText != "")
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, row - 1);
                    Console.Write(firstText);
                }
                int n = row;
                foreach (string text in texts)
                {
                    Console.SetCursorPosition(0, n);
                    Console.Write(text);
                    n++;
                }
                Console.SetCursorPosition(0, row);
                int y = row;
                int yBuffer = row;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(texts[y - row]);
                ResetColors();
                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        lastNum = (y - row);
                        enterPressed = true;
                        break;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        yBuffer = y;
                        if (y == row)
                            y = row + texts.Count - 1;
                        else
                            y--;
                        Console.SetCursorPosition(0, y);
                    }
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        yBuffer = y;
                        if (y == row + texts.Count - 1)
                            y = row;
                        else
                            y++;
                        Console.SetCursorPosition(0, y);
                    }
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(texts[y - row]);
                    ResetColors();
                    Console.SetCursorPosition(0, yBuffer);
                    Console.Write(texts[yBuffer - row]);
                    Console.SetCursorPosition(0, y);

                }
            }
        }

        static void ResetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
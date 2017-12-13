using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    public class myMenuClass
    {
        ConsoleColor TextColor;
        ConsoleColor ChoiseTextColor;
        ConsoleColor BackColor;
        int cursorLeft = 40;
        int cursorTop = 15;
        int Answer;

        public myMenuClass(int CurPosLeft = 40,
                            int CurPosTop = 15,
                            ConsoleColor textColor = ConsoleColor.DarkGray,
                            ConsoleColor choiseTextColor = ConsoleColor.Black,
                            ConsoleColor backColor = ConsoleColor.White)

        {
            cursorLeft = CurPosLeft;
            cursorTop = CurPosTop;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.CursorVisible = false;
            Console.SetWindowSize(100, 40);
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backColor;
            TextColor = textColor;
            ChoiseTextColor = choiseTextColor;
            BackColor = backColor;
           
            Answer = 0;
        }
        public bool YesNoChoise(string Question = "Вы уверены?")
        {
            int curentCursorLeft = 50 - Question.Length / 2;
            Answer = 0;
            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(curentCursorLeft, cursorTop);
                Console.ForegroundColor = TextColor;
                Console.WriteLine(Question);
                if (Answer == 0) Console.ForegroundColor = ChoiseTextColor;
                Console.SetCursorPosition(40, cursorTop+3);
                Console.WriteLine("ДА");
                Console.ForegroundColor = TextColor;
                if (Answer == 1) Console.ForegroundColor = ChoiseTextColor;
                Console.SetCursorPosition(60, cursorTop+3);
                Console.WriteLine("НЕТ");
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        Answer = 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        Answer = 0;
                        break;
                    case ConsoleKey.Enter:
                        Console.ForegroundColor = ChoiseTextColor;
                        Console.CursorVisible = true;
                        Console.Clear();
                        return Answer == 0 ? true : false;
                        break;
                    default:
                        break;
                }
            }
        }


        public int DrowMenuTitle(List<string> menuStrings, string title)
        {
            Answer = 0;
            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.ForegroundColor = TextColor;
                Console.WriteLine(title);
                Console.SetCursorPosition(cursorLeft+1, cursorTop+2);
                for (int i = 0; i < menuStrings.Count; i++)
                {
                    if (menuStrings[i].Length > 95)
                    {
                        menuStrings[i] = menuStrings[i].Substring(0, 92);
                        menuStrings[i] += "...";
                    }
                    if (Answer == i)
                    {
                        Console.ForegroundColor = ChoiseTextColor;
                        
                        Console.WriteLine(menuStrings[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = TextColor;
                        Console.WriteLine(menuStrings[i]);
                    }
                    Console.SetCursorPosition(cursorLeft+1, cursorTop + i + 3);
                }
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (Answer - 1 >= 0) Answer--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (Answer + 1 < menuStrings.Count) Answer++;
                        break;
                    case ConsoleKey.Enter:
                        Console.ForegroundColor = ChoiseTextColor;
                        Console.CursorVisible = true;
                        Console.Clear();
                        return Answer;
                        break;
                    default:
                        break;
                }
            }
        }

        public int DrowMenu(List<string> menuStrings)
        {
            Answer = 0;
            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(cursorLeft, cursorTop);
                for (int i = 0; i < menuStrings.Count; i++)
                {
                    if (menuStrings[i].Length > 95)
                    {
                        menuStrings[i] = menuStrings[i].Substring(0, 92);
                        menuStrings[i] += "...";
                    }
                    if (Answer == i)
                    {
                        Console.ForegroundColor = ChoiseTextColor;
                        Console.WriteLine(menuStrings[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = TextColor;
                        Console.WriteLine(menuStrings[i]);
                    }
                    Console.SetCursorPosition(cursorLeft, cursorTop + i + 1);
                }
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (Answer - 1 >= 0) Answer--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (Answer + 1 < menuStrings.Count) Answer++;
                        break;
                    case ConsoleKey.Enter:
                        Console.ForegroundColor = ChoiseTextColor;
                        Console.CursorVisible = true;
                        Console.Clear();
                        return Answer;
                        break;
                    default:
                        break;
                }
            }
            return -1;
        }


    }
}

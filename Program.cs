using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleQuizApp
{
    class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public Question(string text, string[] options, int correctIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctIndex;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
        static int GetValidInput(int max)
        {
            int val;
            while (true)
            {
                Console.Write("Ваш выбор > ");
                if (int.TryParse(Console.ReadLine(), out val) && val >= 1 && val <= max)
                    return val;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Введите число от 1 до {max}!");
                Console.ResetColor();
            }
        }
    }
}
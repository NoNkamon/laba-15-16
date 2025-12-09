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
        static void RunSurvivalMode()
        {
            var allQuestions = GetHugeMixedPool();
            var shuffledQuestions = allQuestions.OrderBy(q => Guid.NewGuid()).ToList();
            int lives = 7;
            int score = 0;
            int total = shuffledQuestions.Count;
            foreach (var q in shuffledQuestions)
            {
                if (lives <= 0) break;
                Console.WriteLine("----------------------------------------------------------------");
                Console.Write($"ПРОГРЕСС: {score}/{total} | ЖИЗНИ: ");
                Console.ForegroundColor = ConsoleColor.Red;
                for (int l = 0; l < lives; l++) Console.Write("❤ ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int l = 0; l < (5 - lives); l++) Console.Write("💀 ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n\n>> {q.Text}");
                Console.ResetColor();
                for (int j = 0; j < q.Options.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {q.Options[j]}");
                }
                int choice = GetValidInput(q.Options.Length);
                if (choice - 1 == q.CorrectAnswerIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(">> В ТОЧКУ! (+1)");
                    score++;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($">> ОШИБКА! Был ответ: {q.Options[q.CorrectAnswerIndex]}");
                    lives--;
                }
                Console.ResetColor();
                System.Threading.Thread.Sleep(700);
                Console.Clear();
            }
            Console.Clear();
            Console.WriteLine("########################################");
            if (lives > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("     ТЫ ПРОШЕЛ ЭТО БЕЗУМИЕ! ПОБЕДА!");
                Console.WriteLine("     ЗВАНИЕ: ПОВЕЛИТЕЛЬ ВИКТОРИНЫ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("     GAME OVER. ТВОЙ ПУТЬ ОКОНЧЕН.");
            }
            Console.ResetColor();
            Console.WriteLine($"\nИТОГОВЫЙ СЧЕТ: {score} из {total}");
            Console.WriteLine("########################################");
        }
        static void RunRound(List<Question> qs)
        {
            foreach (var q in qs)
            {
                Console.WriteLine($"\n{q.Text}");
                for (int j = 0; j < q.Options.Length; j++) Console.WriteLine($"{j + 1}. {q.Options[j]}");
                int choice = GetValidInput(q.Options.Length);
                if (choice - 1 == q.CorrectAnswerIndex) Console.WriteLine("Принято.");
                else Console.WriteLine("Ну ладно.");
            }
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
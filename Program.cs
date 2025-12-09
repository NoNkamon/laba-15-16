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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "МЯСНОЙ МАРАФОН: ULTIMATE EDITION";
            Console.WriteLine("==========================================");
            Console.WriteLine("  ВИКТОРИНА: МЯСО!!!! ");
            Console.WriteLine("==========================================");
            Console.WriteLine("Выполнили курсанты: Нестеренко, Шелепов, Тареев, Соловьев!!");
            Console.WriteLine("Гр 3832.9");
            var warmup = new List<Question> {
                new Question("Проверка на дурака. Ты готов?", new[] {"Да", "Нет", "Я камень", "Мяу"}, 0)
            };
            RunRound(warmup);
            Console.WriteLine("\nЗагрузка огромной базы вопросов...");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nВНИМАНИЕ! ВОПРОСОВ ОЧЕНЬ МНОГО.");
            Console.WriteLine("Условия обновлены:  У ТЕБЯ 7 ЖИЗНЕЙ.");
            Console.ResetColor();

            Console.WriteLine("\nНажми любую кнопку, чтобы начать АД...");
            Console.ReadKey();
            Console.Clear();
            RunSurvivalMode();

            Console.WriteLine("\nНажмите Enter, чтобы закрыть программу.");
            Console.ReadLine();
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
        static List<Question> GetHugeMixedPool()
        {
            return new List<Question>
            {
                new Question("Полное настоящее имя Мэг Гриффин (так записал Питер в свидетельстве)?", new[] { "Маргарет", "Меган", "Мегатрон", "Мегги" }, 2),
                new Question("Какой марки пиво постоянно пьют герои Гриффинов?", new[] { "Duff", "Pawtucket Patriot", "Heineken", "Bud Light" }, 1),
                new Question("Как зовут сына Кливленда?", new[] { "Кливленд Младший", "Ролло", "Джей-Джей", "Малик" }, 0),
                new Question("Стьюи создал машину времени, чтобы...", new[] { "Убить Лоис", "Избежать брокколи", "Никто не знает", "Попадать в приключения с Брайаном" }, 3),
                new Question("Какое слово Питера стало вирусным (про птицу)?", new[] { "Bird is the word", "Giggity", "Shut up Meg", "Roadhouse" }, 0),
                new Question("Что обычно разрушается у Джо Суонсона в доме?", new[] { "Телевизор", "Крыльцо (пандус)", "Окна", "Бассейн" }, 1),
                new Question("Кто родной отец Питера (спойлер)?", new[] { "Фрэнсис Гриффин", "Ирландец Микки МакФинниган", "Санта Клаус", "Президент США" }, 1),
                new Question("Как Брайан написал свой роман 'Faster Than the Speed of Love'?", new[] { "Гениально", "Он его украл", "Это полная чушь и провал", "Стал бестселлером" }, 2),
                new Question("Злейший враг Стьюи (помимо Лоис) из детства?", new[] { "Бертрам", "Злая Обезьяна", "Крис", "Оливия" }, 0),
                new Question("Любимая порноактриса Куагмайра?", new[] { "Саша Грей", "Сонни Леоне", "Брэнди Лав", "Чери" }, 3),
                new Question("Где работает Питер после фабрики игрушек?", new[] { "В НАСА", "На Пивоварне", "В полиции", "Учителем" }, 1),
                new Question("Сардинский сыр 'Касу Марцу' запрещен во многих странах, потому что в нем...", new[] { "Плесень", "Живые личинки мух", "Яд рыбы Фугу", "Кокаин" }, 1),
                new Question("Из чего делают самый дорогой кофе в мире 'Копи Лувак'?", new[] { "Из зерен, которые выкакал зверек мусанг", "Из золота", "Выращивают на Марсе", "Из желудков китов" }, 0),
                new Question("Национальное шотландское блюдо 'Хаггис' это...", new[] { "Пирог с яблоками", "Бараний желудок, набитый потрохами", "Жареная селедка", "Сладость" }, 1),
                new Question("В каком виде мяса больше всего железа?", new[] { "Куриная грудка", "Говяжья печень", "Свинина", "Рыба" }, 1),
                new Question("Что добавляют в колбасу, чтобы она была розовой (а не серой)?", new[] { "Нитрит натрия", "Марганцовку", "Кетчуп", "Кровь" }, 0),
                new Question("Какой перец считался самым острым в мире до появления Pepper X?", new[] { "Халапеньо", "Чили", "Каролинский Жнец (Carolina Reaper)", "Табаско" }, 2),
                new Question("Хамон (Jamón) - это...", new[] { "Сыровяленая свиная нога", "Вид сыра", "Итальянская паста", "Рыбный соус" }, 0),
                new Question("Что такое 'Су вид' (Sous-vide)?", new[] { "Жарка в масле", "Готовка в вакууме при низкой температуре", "Заморозка азотом", "Сыроедение" }, 1),
                new Question("Какой плод с точки зрения ботаники является ОРЕХОМ?", new[] { "Грецкий орех", "Арахис", "Миндаль", "Фундук" }, 3),
                new Question("Вегетарианцы не едят желе, потому что...", new[] { "Там сахар", "Желатин делают из костей и шкур животных", "Оно невкусное", "Это химия" }, 1),
                new Question("Порода собаки Снупи (из комиксов)?", new[] { "Далматин", "Бигль", "Такса", "Дворняга" }, 1),
                new Question("Какое животное ежегодно убивает больше всего людей в Африке?", new[] { "Лев", "Бегемот", "Крокодил", "Акула" }, 1),
                new Question("Что будет с собакой, если она съест шоколад?", new[] { "Ничего страшного", "Будет счастлива", "Сильное отравление (теобромин)", "Станет фиолетовой" }, 2),
                new Question("Самая тяжелая порода собак (до 100 кг+)?", new[] { "Английский Мастиф", "Доберман", "Немецкая овчарка", "Лабрадор" }, 0),
                new Question("Сколько сердец у осьминога?", new[] { "Одно", "Два", "Три", "Четыре" }, 2),
                new Question("У какого животного пенис имеет ЧЕТЫРЕ головки?", new[] { "Ехидна", "Кенгуру", "Кролик", "Тапир" }, 0),
                new Question("Кто единственное бессмертное существо на Земле (биологически)?", new[] { "Черепаха", "Медуза Turritopsis dohrnii", "Лобстер", "Тихоходка" }, 1),
                new Question("Как спит Кашалот?", new[] { "На боку", "Вертикально (стоя в воде)", "На берегу", "Он не спит" }, 1),
                new Question("В Средневековье считали, что лучший способ вылечить чуму — это...", new[] { "Пить много воды", "Нюхать пуканье в банке", "Молиться", "Есть землю" }, 1),
                new Question("Какое изобретение убивает больше людей в год, чем акулы за 100 лет?", new[] { "Торговые автоматы (падают на людей)", "Смартфоны", "Пылесосы", "Вилки" }, 0),
                new Question("На чем состоялась последняя казнь гильотиной во Франции (1977 год)?", new[] { "На Красной площади", "В тюрьме (уже вышли Звездные Войны)", "Во время революции", "В 18 веке" }, 1),
                new Question("Какого цвета кровь у мечехвоста?", new[] { "Красная", "Голубая", "Зеленая", "Черная" }, 1),
                new Question("Что используют для создания запаха ванили в дешевых продуктах?", new[] { "Ванилин из нефти", "Струя бобра (Кастореум)", "Цветы", "Сахар" }, 1),
                new Question("Где находится сердце у креветки?", new[] { "В груди", "В голове", "В хвосте", "В лапках" }, 1),
                new Question("Правда ли, что если таракану оторвать голову, он умрет от голода?", new[] { "Да, через неделю", "Нет, отрастет новая", "Он умрет от потери крови", "Мгновенно" }, 0),
                new Question("Что означает аббревиатура в названии пушки 'Taser' (Тайзер)?", new[] { "Tom Swift and His Electric Rifle", "Tactical Electric System", "Target Air Shocker", "Это фамилия изобретателя" }, 0),
                new Question("Сколько будет 0,1 + 0,2 в программировании (точно)?", new[] { "0.3", "0.30000000000000004", "0", "Ошибка" }, 1),
                new Question("Самая популярная фамилия в мире?", new[] { "Смит", "Иванов", "Ван (китайская)", "Ли" }, 3),
                new Question("На логотипе какой машины изображена змея, поедающая человека?", new[] { "Alfa Romeo", "Ferrari", "Peugeot", "Skoda" }, 0),
                new Question("Из чего делают текилу?", new[] { "Кактус", "Голубая Агава", "Тростник", "Кукуруза" }, 1),
                new Question("Что меньше?", new[] { "Атом", "Электрон", "Клетка", "Молекула" }, 1),
                new Question("Кто такая Консуэла в Гриффинах?", new[] { "Уборщица ('No, no, no...')", "Учительница", "Жена Джо", "Продавец" }, 0),
                new Question("Что ненавидит Питер больше всего (в старых сезонах)?", new[] { "Свою жену", "Фильм 'Крестный отец'", "Англичан", "Капусту" }, 1),
            };
        }
    }
}
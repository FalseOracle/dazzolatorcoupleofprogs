//подвязываем небольшое количество системных библиотек
using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

//объявляем главный класс, который позволит нам запускать программы по очереди или выборочно
class Program
{
    static void Main()
    {
        //создаём список доступных задач для выбора пользаком
        List<Action> applications = new List<Action>
        {
            GCD_LCM.Run,
            SumOfDigits.Run,
            ReverseNumber.Run,
            OddDigitsOnly.Run,
            Factorial.Run,
            GuessTheNumber.Run,
            NumberStatistics.Run,
            CurrencyConverter.Run,
            EventCalendar.Run
        };

        //создаём список с названиями задач
        List<string> taskNames = new List<string>
        {
            "1. НОД и НОК двух чисел",
            "2. Сумма цифр числа",
            "3. Переворот числа",
            "4. Число из нечётных цифр",
            "5. Факториал числа",
            "6. Угадай число",
            "7. Статистика чисел",
            "8. Конвертер валют",
            "9. Календарь событий"
        };

        while (true) //бесконечный цикл для повторного выбора задач при неудачном вводе
        {
            //выводим список задач пользователю
            Console.WriteLine("Доступные задачи:");
            //"перебираем" в массиве имён функций переменную, которая считает ввод с клавиатуры польхователем и выводит задачу
            foreach (var task in taskNames)
            {
                Console.WriteLine(task);
            }
            Console.WriteLine("Выберите номер задачи или введите 0 для выхода:");

            //считываем выбор пользователя 
            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice >= 0 && choice <= applications.Count)
            {
                if (choice == 0) break; //если пользователь ввёл 0, выходим из программы, так как задачи под 0 нету

                //запускаем выбранную задачу, вычитаем один ибо индексы массива идут с нуля
                applications[choice - 1]();
            }
            else
            //если номер не совпадает с индексом, то дропаем проста да лалалал
            {
                Console.WriteLine("Ошибка ввода. Введите корректный номер задачи.");
            }
        }
    }
}

//первая задача по поиску НОДа и НОКа двух чисел, которые вводит пользователь
class GCD_LCM
{
    public static void Run()
    {
        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: НОД и НОК двух чисел");
            Console.Write("Введите два числа через пробел или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню" я добавлю во все задачи чтобы была быстрая и удобная возможность выйти 
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }

            string[] inputs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (inputs.Length == 2 && int.TryParse(inputs[0], out int number1) && int.TryParse(inputs[1], out int number2))
            {
                if (number1 == 0 || number2 == 0) // проверка деления на ноль
                {
                    Console.WriteLine("Ошибка: одно из чисел равно нулю. Введите два ненулевых целых числа.");
                    continue; // запрашиваем ввод заново
                }

                int gcd = CalculateGCD(number1, number2);
                int lcm = number1 * number2 / gcd;
                Console.WriteLine($"НОД: {gcd}, НОК: {lcm}");
                break; //завершаем задачу после её выполнения, после чего мы будем говорить о завершении задачи ниже
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Убедитесь, что вы ввели два целых числа через пробел.");
            }
        }

        //говорим что задача окончена, заканчиваем программу и переносил в меню после нажатия на любую кнопку
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
    //некие подсчёты для работы задачи и подсчёта
    static int CalculateGCD(int a, int b) => b == 0 ? a : CalculateGCD(b, a % b);
}




//вторая задача, где числа, введённые пользователем, считают (к примеру, при вводе 64, программа выведет в результат сумму 10)
class SumOfDigits
{
    public static void Run()
    {
        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Сумма цифр числа");
            Console.Write("Введите целое число или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню"
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }
            //добавляем проверку на ошибки
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int number))
            {
                //складываем все числа в введёном "числе"
                int sum = 0;
                while (number != 0)
                {
                    sum += number % 10;
                    number /= 10;
                }
                Console.WriteLine($"Сумма цифр: {sum}");
                break; //завершаем задачу
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите корректное целое число.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}


//третья задача, где ожидается ввод числа, которое просто переворачивают (ну вы поняли, рекурсия, реверсия и всё такое)
class ReverseNumber
{
    public static void Run()
    {
        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Переворот числа");
            Console.Write("Введите целое число или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню"
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }
            //делаем проверку на ошибки
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int number))
            {
                //таким хитрым образом реверсируем число!
                int reversed = 0;
                while (number != 0)
                {
                    reversed = reversed * 10 + number % 10;
                    number /= 10;
                }
                Console.WriteLine($"Перевернутое число: {reversed}");
                break; //завершаем задачу
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите корректное целое число.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}




//задача, где из введённого числа выводятся повторно только нечётные (к примеру при вводе 65, выведется 5)
class OddDigitsOnly
{
    public static void Run()
    {
        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Число из нечётных цифр");
            Console.Write("Введите число или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню"
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }

            if (!string.IsNullOrWhiteSpace(input) && input.All(char.IsDigit))
            {
                //проверяем, можно ли разделить число на 2, тем самым и проверяем ёмаё)
                string result = string.Concat(input.Where(digit => (digit - '0') % 2 != 0));
                Console.WriteLine(result.Length > 0 ? $"Число из нечётных цифр: {result}" : "Нет нечётных цифр.");
                break; //завершаем задачу
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите корректное число, содержащее только цифры.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}


//пятая задача, где пользователь вводит число, а программа вычисляет его факториал
class Factorial
{
    public static void Run()
    {
        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Факториал числа");
            Console.Write("Введите неотрицательное целое число или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню"
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }

            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int number) && number >= 0)
            {
                //подсчёт факториала................
                long factorial = 1;
                for (int i = 2; i <= number; i++)
                {
                    factorial *= i;
                }
                Console.WriteLine($"Факториал: {factorial}");
                break; //завершаем задачу
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите неотрицательное целое число.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}


//шестая задача, где пользователю предлагается угадать случайное число от 1 до 100, интересная и простая задача как по мне
class GuessTheNumber
{
    public static void Run()
    {
        //генерируем случайное число, которое будем угадывать, а также обьявляем переменную, в которую будет записываться ввод пользователля
        Random random = new Random();
        int targetNumber = random.Next(1, 101);
        int userGuess;

        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Угадай число");
            Console.WriteLine("Угадайте число от 1 до 100");
            Console.Write("Ваш вариант или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню"
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }

            if (int.TryParse(input, out userGuess))
            {
                //даём пользователю угадать число простым способом
                if (userGuess < targetNumber) Console.WriteLine("Больше");
                else if (userGuess > targetNumber) Console.WriteLine("Меньше");
                else
                {
                    Console.WriteLine("Поздравляю, вы угадали!");
                    break; //завершаем задачу
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите корректное целое число.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}

//седьмая задача, где сначала мы выбираем количество чисел, после чего поочерёдно вводим их, в конце нам считают среднее, медиану и стандартное отклонение
class NumberStatistics
{
    public static void Run()
    {
        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Статистика чисел");
            Console.Write("Введите количество чисел или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню"
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }

            if (int.TryParse(input, out int count) && count > 0)
            {
                //обьявляем массив чисел в дабл (где после запятой числа есть)
                List<double> numbers = new List<double>();
                for (int i = 0; i < count; i++)
                {
                    //выводим для ввода пользователю столько раз эту строку, сколько чисел он выбрал
                    Console.Write($"Число {i + 1}: ");
                    //записываем число в массив, беря ввод пользователя
                    if (double.TryParse(Console.ReadLine(), out double number))
                    {
                        numbers.Add(number);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода. Введите корректное число.");
                        i--; //повторить ввод для текущего числа
                    }
                }

                double average = numbers.Average();
                double median = numbers.OrderBy(x => x).Skip(count / 2).First();
                double standardDeviation = Math.Sqrt(numbers.Select(x => Math.Pow(x - average, 2)).Average());

                Console.WriteLine($"Среднее: {average}, Медиана: {median}, Стандартное отклонение: {standardDeviation}");
                break; //завершаем задачу
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите положительное целое число.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}

//восьмая задача по переводу из грязных зелённых долларов и загнивающие евро и православные рубли
class CurrencyConverter
{
    public static void Run()
    {
        //задаём словарь, в который добавляем значения (string - тип данных для ключа (usd eur rub), а double - тип данных для значения которое ему пренадлежит)
        Dictionary<string, double> exchangeRates = new Dictionary<string, double>
        {
            {"USD", 1.0}, {"EUR", 0.85}, {"RUB", 75.0}
        };

        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Конвертер валют");
            Console.Write("Введите сумму в USD или напишите 'меню' для возврата в меню: ");
            string input = Console.ReadLine();

            //обработка команды "меню"
            if (input.ToLower() == "меню")
            {
                return; //переход в главное меню
            }

            //переводим валюту ожидая ввод пользователя, и если ввод был больше 0, то мы выводим 
            if (double.TryParse(input, out double amountInUSD) && amountInUSD >= 0)
            {
                //результат с посчитанными данными
                Console.WriteLine("Конвертированные суммы:");
                foreach (var rate in exchangeRates)
                {
                    Console.WriteLine($"{rate.Key}: {amountInUSD * rate.Value:F2}");
                }
                break; //завершаем задачу
            }
            else
            {
                Console.WriteLine("Ошибка ввода. Введите корректную неотрицательную сумму.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}

//последняя задача, где есть каллендарь событий, который мы можем просмотреть, добавить задачу или выйти
class EventCalendar
{
    public static void Run()
    {
        //снова создаём словарь, где будет ожидаться дата и ввод строки
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();
        string command;

        while (true) //цикл для повторного ввода задачи при неудачах или неправильных вводов
        {
            Console.WriteLine("\nЗадача: Календарь событий");
            Console.WriteLine("Команды: добавить, показать, выход, или напишите 'меню' для возврата в меню");
            command = Console.ReadLine().ToLower();

            //обработка команды "меню"
            if (command == "меню")
            {
                return; //переход в главное меню
            }

            //событие, если мы вводим добавить
            if (command == "добавить")
            {
                //происходит вывод с просьбой ввода даты, после чего 
                Console.Write("Введите дату (гггг-мм-дд): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime eventDate))
                {
                    //просят ввести событие, которое записывается в словарь и храниться там
                    Console.Write("Введите событие: ");
                    string eventInfo = Console.ReadLine();
                    events[eventDate] = eventInfo;
                }
                //проверка на ошибки
                else
                {
                    Console.WriteLine("Ошибка ввода. Введите корректную дату в формате гггг-мм-дд.");
                }
            }

            //если пользователь пишет "показать"
            else if (command == "показать")
            {
                //нам выводим все события из словаря 
                if (events.Any())
                {
                    foreach (var evt in events.OrderBy(e => e.Key))
                    {
                        Console.WriteLine($"{evt.Key:yyyy-MM-dd}: {evt.Value}");
                    }
                }
                //а если их нету, об этом и сообщается
                else
                {
                    Console.WriteLine("Нет запланированных событий.");
                }
            }
            else if (command == "выход")
            {
                break; //завершаем задачу
            }
            else
            {
                Console.WriteLine("Неизвестная команда. Введите 'добавить', 'показать', 'выход' или 'меню'.");
            }
        }

        //возврат в главное меню
        Console.WriteLine("\nЗадача завершена. Нажмите любую клавишу для возврата в меню...");
        Console.ReadKey();
    }
}


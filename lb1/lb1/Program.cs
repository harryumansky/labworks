using PersonLib;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lb1
{
    /// <summary>
    /// Класс основной части программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Создание списков
        /// </summary>
        /// <param name="args">аргумент</param>
        public static void Main(string[] args)
        {

            PersonList list1 = new PersonList();
            PersonList list2 = new PersonList();

            // Включение в каждый из списков по три человека
            list1.Add(new Person("Дмитрий", "Лоцман", 46, Gender.Man));
            list1.Add(new Person("Никита", "Вагапов", 36, Gender.Man));
            list1.Add(new Person("Bill", "Gates", 71, Gender.Man));

            list2.Add(new Person("Анастасия", "Лескова", 25, Gender.Woman));
            list2.Add(new Person("Андрей", "Любимов", 30, Gender.Man));
            list2.Add(new Person("Jessica", "Alba", 45, Gender.Woman));
            WaitForKey();

            // Вывод содержимого каждого списка
            PrintList(list1, "Список 1");
            PrintList(list2, "Список 2");
            WaitForKey();

            // Включение нового человека в первый список
            Person newPerson = new Person("Юлия", "Андреева", 
                                           41, Gender.Woman);
            list1.Add(newPerson);
            Console.WriteLine("\nДобавили нового человека в первый список:");
            PrintList(list1, "Список 1");
            WaitForKey();

            // Копирование второго человека из первого списка во второй список
            Person personCopy = list1.Get(1);
            list2.Add(personCopy);
            Console.WriteLine($"Скопирован человек из первого списка: " +
                $"{personCopy.Name} {personCopy.Surname}");
            PrintList(list1, "Список 1 после копирования: ");
            PrintList(list2, "Список 2 после копирования: ");
            WaitForKey();

            // Удаление второго человека из первого списка
            list1.RemoveAt(1);
            Console.WriteLine("Удален второй человек из первого списка");
            PrintList(list1, "Список 1 после удаления второго человека: ");
            PrintList(list2, "Список 2 после удаления из первого списка: ");
            WaitForKey();

            // Форматирование второго списка
            list2.Clear();
            Console.WriteLine("Второй список форматирован");
            PrintList(list2, "Список 2 после форматирования: ");
            WaitForKey();

            // Включение человека в список
            Console.WriteLine("\nДобавим человека вручную в список 1");
            Person personFromConsole = ReadFromConsole();
            list1.Add(personFromConsole);
            Console.WriteLine("\nВ список 1 добавлен человек");
            PrintPerson(personFromConsole);
            PrintList(list1, "Список 1 после добавления: ");
            WaitForKey();

            // Создание случайного человека
            Person randomPerson = RandomPerson.GetRandomPerson();
            Console.WriteLine("\nСоздан случайный человек:");
            PrintPerson(randomPerson);
            WaitForKey();
        }

        /// <summary>
        /// Метод для вывода списка людей на консоль с указанным заголовком
        /// </summary>
        /// <param name="list">Список для вывода</param>
        /// <param name="listName">Заголовок списка</param>
        private static void PrintList(PersonList list, string listName)
        {
            Console.WriteLine($"\n{listName}:");
            for (int i = 0; i < list.Count; i++)
            {
                PrintPerson(list.Get(i));
            }
        }

        /// <summary>
        /// Метод для вывода информации об одном человеке
        /// </summary>
        /// <param name="person">Объект Person для вывода</param>
        private static void PrintPerson(Person person)
        {
            string genderWritable;
            switch (person.Gender)
            {
                case Gender.Man:
                { 
                    genderWritable = "Мужской";
                    break;
                }

                case Gender.Woman:
                {
                    genderWritable = "Женский";
                    break;
                }

                default:
                {
                    throw new ArgumentOutOfRangeException(
                            nameof(person.Gender),
                            "Неизвестное значение пола");
                }
            }
            Console.WriteLine(
                $"{person.Name} {person.Surname}, " +
                $"возраст: {person.Age}, пол: {genderWritable}");
        }

        /// <summary>
        /// Метод для паузы между пунктами программы
        /// </summary>
        private static void WaitForKey()
        {
            Console.WriteLine("\nНажмите любую клавишу, " +
                                "чтобы продолжить...");
            Console.ReadKey();
        }

        /// <summary>
        /// Ввод данных о человеке с консоли
        /// </summary>
        /// <returns>Возвращает объект класса Person</returns>
        public static Person ReadFromConsole()   
        {
            Person person = new Person();

            var actionList = new List<Tuple<string, Action<string>>>()
            {
                new("Введите имя: ",
                    (input) =>
                    {
                        person.Name = input;
                    }),
                new("Введите фамилию: ",
                    (input) =>
                    {
                        person.Surname = input;
                    }),
                new("Введите возраст от 1 до 123: ",
                    (input) =>
                    {
                        int parsedAge = int.Parse(input);
                        person.Age = parsedAge;
                    }),
                new("Введите пол (1 — Мужчина, 2 — Женщина): ",
                    (input) =>
                    {
                        string innerInput = input ?? string.Empty;
                        switch (innerInput)
                        {
                            case "1":
                            {
                                person.Gender = Gender.Man;
                                break;
                            }
                            case "2":
                            {
                                person.Gender = Gender.Woman ;
                                break;
                            }
                            default:
                            {
                                throw new ArgumentException("Ошибка! Введите 1 или 2");
                            }
                        }
                    }),
            };

            foreach(var action in actionList)
            {
                ActionHandler(action.Item1, action.Item2);
            }

            return person;
        }

        //TODO: XML+
        /// <summary>
        /// Метод проверки вводимых данных
        /// </summary>
        /// <param name="message">
        /// Аргумент для передачи строки в метод при его вызове
        /// </param>
        /// <param name="action">
        /// Сохраняет и передает введеное в консоли значение
        /// </param>
        private static void ActionHandler(string message, Action<string> action)
        {
            while (true)
            {
                Console.Write(message);
                
                try
                {
                    string input = Console.ReadLine();
                    action(input);
                    return;
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(ArgumentException)
                        || ex.GetType() == typeof(FormatException)
                        || ex.GetType() == typeof(ArgumentOutOfRangeException))
                    { 
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }
            }
        }        
    }
}
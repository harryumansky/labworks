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
            Person newPerson = new Person("Юлия", "Андреева", 41, Gender.Woman);
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
            //TODO: RSDN+
            string genderStr;
            switch (person.Gender)
            {
                case Gender.Man:
                { 
                    //TODO: {}+
                    genderStr = "Мужской";
                    break;
                }

                case Gender.Woman:
                {
                    //TODO: {}+
                    genderStr = "Женский";
                    break;
                }

                default:
                    //TODO: {}+
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(person.Gender),
                        "Неизвестное значение пола");
                }
            }
            Console.WriteLine(
                $"{person.Name} {person.Surname}, " +
                $"возраст: {person.Age}, пол: {genderStr}");
        }

        /// <summary>
        /// Метод для паузы между пунктами программы
        /// </summary>
        private static void WaitForKey()
        {
            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        /// <summary>
        /// Ввод данных о человеке с консоли
        /// </summary>
        /// <returns>Возвращает объект класса Person</returns>
        /// <exception cref="Exception">Создание при неверном вводе</exception>
        public static Person ReadFromConsole()   
        {
            while (true)
            {
                //TODO: refactor+
                string name;
                while (true)
                {
                    Console.Write("Введите имя: ");
                    string input = Console.ReadLine();
                    try
                    {                        
                        name = Person.Verificate(input, "Имя");
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }

                //TODO: refactor+
                string surname;
                while (true)
                {
                    Console.Write("Введите фамилию: ");
                    string input = Console.ReadLine();
                    try
                    {
                        surname = Person.Verificate(input, "Фамилия");
                        Person.EnsureLanguage(name, surname);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }

                int age;
                while (true)
                {
                    Console.Write("Введите возраст: ");
                    if (int.TryParse(Console.ReadLine(), out age) &&
                        age >= Person.MinAge &&
                        age <= Person.MaxAge)
                    {
                        break;
                    }
                    Console.WriteLine(
                        $"Введите целое число от {Person.MinAge} " +
                        $"до {Person.MaxAge}.");
                }

                Gender gender;
                while (true)
                {
                    Console.Write("Введите пол (1 — Мужчина, 2 — Женщина): ");
                    string input = Console.ReadLine();
                    //TODO: switch-case+
                    switch (input)
                    {
                        case "1":
                        {
                            gender = Gender.Man;
                            break;
                        }
                        case "2":
                        {
                            gender = Gender.Woman;
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Ошибка! Введите 1 или 2");
                            continue;
                        }
                    }
                    break;
                }

                try
                {
                    return new Person(name, surname, age, gender);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine($"Ошибка: {exception.Message}");
                    Console.WriteLine("Повторите ввод данных о человеке\n");
                }
            }
        }
    }
}
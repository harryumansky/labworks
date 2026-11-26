using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLib
{
    /// <summary>
    /// Класс создания случайного человека
    /// </summary>
    public class RandomPerson
    {
        /// <summary>
        /// Создание и возврат нового экземпляра класса Person со случайными данными
        /// </summary>
        /// <returns>Новый объект класса Person</returns>
        public static Person GetRandomPerson()
        {
            Random random = new Random();

            string[] manNames = { "Александр", "Алексей", "Андрей", "Борис",
                                    "Бронислав", "Виктор", "Валерий", "Дмитрий",
                                    "Владимир", "Иван", "Олег", "Геннадий" };
            string[] womanNames = { "Ангелина", "Алина", "Виктория", "Ольга",
                                      "Наталья", "Мария", "Дарья", "Любовь", 
                                      "Евгения", "Галина", "Лидия", "Светлана" };

            string[] manSurname = { "Смирнов", "Иванов", "Кузнецов", "Морозов",
                                      "Попов", "Соколов", "Леберев", "Петров",
                                      "Козлов", "Новиков", "Волков", "Орлов" };
            string[] womanSurname= { "Семенова", "Зайцева", "Фролова", "Гусева",
                                       "Попова", "Белова", "Жукова", "Комарова",
                                       "Титова", "Крылова", "Фомина", "Исаева" };

            var gender = random.Next(2) == 0
                ? Gender.Man
                : Gender.Woman;

            int age = random.Next(Person.MinAge,Person.MaxAge + 1);

            string name = gender == Gender.Man
                ? manNames[random.Next(manNames.Length)]
                : womanNames[random.Next(womanNames.Length)];

            string surname = gender == Gender.Man
                ? manSurname[random.Next(manSurname.Length)]
                : womanSurname[random.Next(womanSurname.Length)];

            return new Person(name, surname, age, gender);
        }
    }
}

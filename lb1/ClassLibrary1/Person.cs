using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    /// <summary>
    /// Класс хранения и обработки данных о человеке
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        private string _name;

        /// <summary>
        /// Фамилия
        /// </summary>
        private string _surname;

        /// <summary>
        /// Возраст
        /// </summary>
        private int _age;

        /// <summary>
        /// Пол
        /// </summary>
        private Gender _gender;

        /// <summary>
        /// Создание информации о человеке
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="gender">Пол</param>
        public Person(string name, string surname, int age, Gender gender)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Gender = gender;
        }

        /// <summary>
        /// Получение и проверка имени
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                string validatedName = Validate(value, "Имя");
                EnsureLanguage(validatedName, _surname);
                _name = validatedName;
            }
        }

        /// <summary>
        /// Получение и проверка фамилии
        /// </summary>
        public string Surname
        {
            get { return _surname; }
            set
            {
                string validatedSurname = Validate(value, "Фамилия");
                EnsureLanguage(_name, validatedSurname);
                _surname = validatedSurname;
            }
        }

        /// <summary>
        /// Минимальный возраст человека
        /// </summary>
        public const int MinAge = 1;

        /// <summary>
        /// Максимальный возраст человека
        /// </summary>
        public const int MaxAge = 123;

        /// <summary>
        /// Проверка корректности введенного возраста
        /// </summary>
        public int Age
        {
            get { return _age; }
            set
            {
                //TODO: remove
                if (string.IsNullOrEmpty(Convert.ToString(value)))
                {
                    throw new Exception("Введите возраст!");
                }

                if (value < MinAge || value > MaxAge)
                {
                    throw new Exception($"{nameof(Age)} должен быть в дипазоне" +
                        $" от {MinAge} до {MaxAge}");
                }
                _age = value;
            }
        }

        /// <summary>
        /// Свойство Gender позволяет установить пол человека
        /// </summary>
        public Gender Gender
        {
            get { return _gender; }
            set
            {
                //TODO: remove
                if (!Enum.IsDefined(typeof(Gender), value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "Указано недопустимое значение пола.");
                }
                _gender = value;
            }
        }

        /// <summary>
        /// Проверка соответствия алфавиту - кириллица
        /// </summary>
        private const string Russian = @"^[а-яА-ЯёЁ]+(?:-[а-яА-ЯёЁ]+)?$";

        /// <summary>
        /// Проверка соответствия алфавиту - латиница
        /// </summary>
        private const string English = @"^[a-zA-Z]+(?:-[a-zA-Z]+)?$";

        /// <summary>
        /// Проверка соответсвия входной строки заданным правилам
        /// </summary>
        /// <param name="value">Проверяемая строка</param>
        /// <param name="fieldName">Название поля</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">при неверном вводе</exception>
        private static string Validate(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    $"{fieldName} не может быть пустым " +
                    $"или состоять только из пробелов.");
            }

            bool isRussian = Regex.IsMatch(value, Russian);
            bool isEnglish = Regex.IsMatch(value, English);

            if (!isRussian && !isEnglish)
            {
                throw new ArgumentException(
                    $"{fieldName} может содержать только русские буквы" +
                    //TODO: caps
                    $" ИЛИ только английские буквы. " +
                    $"Двойное имя/фамилия допускается через дефис.");
            }

            var textInfo =
                System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(value.ToLowerInvariant());
        }

        /// <summary>
        /// Проверка совпадения языка имени и фамилии
        /// </summary>
        /// <param name="name">Имя для проверки</param>
        /// <param name="surname">Фамилия для проверки</param>
        /// <exception cref="ArgumentException">
        /// Если язык имени и фамилии не совпадает
        /// </exception>
        private static void EnsureLanguage(
            string name,
            string surname)
        {
            if (!string.IsNullOrEmpty(name) &&
                !string.IsNullOrEmpty(surname))
            {
                bool nameIsRussian =
                    Regex.IsMatch(name, Russian);

                bool surnameIsRussian =
                    Regex.IsMatch(surname, Russian);

                if (nameIsRussian != surnameIsRussian)
                {
                    throw new ArgumentException(
                        "Язык имени и фамилии не совпадает. " +
                        "Имя и фамилия должны быть на одном языке.");
                }
            }
        }       
    }
}


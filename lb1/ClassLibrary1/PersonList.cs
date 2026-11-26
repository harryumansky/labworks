using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Класс списка людей
    /// </summary>
    public class PersonList
    {
        /// <summary>
        /// Инициализация нового экземпляра класса с пустым списком
        /// </summary>
        private readonly List<Person> _persons;

        /// <summary>
        /// Конструктор класса по умолчанию
        /// </summary>
        public PersonList()
        {
            _persons = new List<Person>();
        }

        /// <summary>
        /// Дополнение списка новым человеком
        /// </summary>
        public void Add(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(
                    nameof(person),
                    "Нельзя добавить пустую строку в список");
            }
            _persons.Add(person);
        }

        /// <summary>
        /// Удаление указанного человека из списка
        /// </summary>
        public void Remove(Person person)
        {
            _persons.Remove(person);
        }

        /// <summary>
        /// Удаление человека из списка по его индексу
        /// </summary>
        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            _persons.RemoveAt(index);
        }

        /// <summary>
        /// Возврат человека из списка по его индексу
        /// </summary>
        public Person Get(int index)
        {
            ValidateIndex(index);
            return _persons[index];
        }

        /// <summary>
        /// Возврат индекса указанного человека в списке
        /// Если человек не найден, возвращается -1
        /// </summary>
        public int IndexOf(Person person)
        {
            return _persons.IndexOf(person);
        }

        /// <summary>
        /// Форматирование списка удалением всех людей
        /// </summary>
        public void Clear()
        {
            _persons.Clear();
        }

        /// <summary>
        /// Возврат количества людей в списке
        /// </summary>
        public int Count
        {
            get { return _persons.Count; }
        }

        /// <summary>
        /// Проверка корректности индекса
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, когда 
        /// индекс вне диапазона</exception>
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= _persons.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
            }
        }
    }
}

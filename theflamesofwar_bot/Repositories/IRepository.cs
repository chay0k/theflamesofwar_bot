using System;
using System.Collections.Generic;
using System.Text;

namespace theflamesofwar_bot.Repositories
{
    public interface IRepository<T> : IDisposable
            where T : class
    {
        IEnumerable<T> GetList(); // получение всех объектов
        T Get(Guid id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(T item); // удаление объекта по id
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}

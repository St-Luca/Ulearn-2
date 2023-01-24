using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        /// <summary>
        /// Возвращает элемент по индексу или null, если такого элемента нет.
        /// При присвоении удаляет все элементы после.
        /// Если индекс в точности равен размеру коллекции, работает как Append.
        /// </summary>
        private readonly List<T> items = new List<T>();

        public T this[int index]
        {
            get
            {
                lock (items)
                {
                    if (index >= items.Count)
                    {
                        return null;
                    }
                    return items[index];
                }
            }
            set
            {
                lock (items)
                {
                    if (index == items.Count) //После ничего нет для удаления
                    {
                        items.Add(value);
                    }
                    else if (Count > index)
                    {
                        items[index] = value;
                        for (int j = items.Count - 1; j > index; j--)
                        {
                            items.RemoveAt(j);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает последний элемент или null, если такого элемента нет
        /// </summary>
        public T LastItem()
        {
            lock (items)
            {
                if (items.Count == 0)
                {
                    return null;
                }
                return items[items.Count - 1];
            }
        }

        /// <summary>
        /// Добавляет item в конец только если lastItem является последним элементом
        /// </summary>
        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (items)
            {
                if (items.Count == 0)  //Первый тоже последний
                {
                    items.Add(item);
                }
                else if (LastItem() == knownLastItem)
                {
                    items.Add(item);
                }
            }
        }

        /// <summary>
        /// Возвращает количество элементов в коллекции
        /// </summary>
        public int Count
        {
            get
            {
                return items.Count;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		/// <summary>
		/// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
		/// Медиана списка из четного количества элементов — это среднее арифметическое 
		/// двух серединных элементов списка после сортировки.
		/// </summary>
		/// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
		public static double Median(this IEnumerable<double> items)
		{
			List<double> itemList = items.ToList();
			itemList.Sort();
			int count = itemList.Count;

			if (count == 0)
			{
				throw new InvalidOperationException();
			}

			double median;

			if (count % 2 != 0)
			{
				median = itemList[count / 2];
			}
			else
			{
				median = (itemList[count / 2] + itemList[count / 2 - 1]) / 2;
			}

			return median;
		}

		/// <returns>
		/// Возвращает последовательность, состоящую из пар соседних элементов.
		/// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			T temp = default(T);
			int count = 0;
			foreach (var item in items)
			{
				if (count == 0)
				{
					count++;
					temp = item;
					continue;
				}
				yield return new Tuple<T, T>(temp, item);
				temp = item;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
		/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
		/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines
				.Skip(1)//Пропускаем заголовки
				.Select(s => s.Split(';'))//Cоздается массив из 3 слов строки
				.Select(lineArray =>
				{
					int slideId;

					if (lineArray.Length != 3 || !Enum.TryParse(lineArray[1], true, out SlideType slide))
					{
						return null;
					}
					else
					{
						if (Int32.TryParse(lineArray[0], out slideId)) slideId = Int32.Parse(lineArray[0]);
						else return null;
					}
					return new SlideRecord(slideId, slide, lineArray[2]);
				})
				.Where(slide => slide != null)
				.ToDictionary(slide => slide.SlideId);//Словарь: ключ — идентификатор слайда
		}

		/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
		/// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
		/// Такой словарь можно получить методом ParseSlideRecords</param>
		/// <returns>Список информации о посещениях</returns>
		/// <exception cref="FormatException">Если среди строк есть некорректные</exception>
		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
				.Skip(1) //Пропускаем заголовки
				.Aggregate(new List<VisitRecord>(), (curr, next) =>
				{
					string[] array = next.Split(';');//Cоздается массив из 4 слов строки

					if (array.Length != 4)  //Не хватает элементов строки
					{
						throw new FormatException("Wrong line [" + next + "]");
					}

					if (!int.TryParse(array[0], out int userId) ||
						!int.TryParse(array[1], out int slideId) ||
						!DateTime.TryParse(array[2], out DateTime date) ||
						!DateTime.TryParse(array[3], out DateTime dateTime)) //Неправильный формат/значения
					{
						throw new FormatException($"Wrong line [" + next + "]");
					}

					if (!slides.TryGetValue(slideId, out SlideRecord slide)) //Нет слайда
					{
						throw new FormatException($"Wrong line [" + next + "]");
					}

					curr.Add(new VisitRecord(userId, slideId, date.Add(dateTime.TimeOfDay), slide.SlideType));
					return curr;
				});
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<double> windowPoints = new Queue<double>();
			LinkedList<double> maxPoints = new LinkedList<double>();
			foreach (DataPoint dataItem in data)
			{
				windowPoints.Enqueue(dataItem.OriginalY);
				if (windowPoints.Count > windowWidth)  //Превышение лимита окна
				{
					if (windowPoints.Dequeue() == maxPoints.First.Value)
					{
						maxPoints.RemoveFirst();
					}
				}

				while (maxPoints.Count > 0 && dataItem.OriginalY > maxPoints.Last.Value)
				{
					maxPoints.RemoveLast();  //Удаляем максимумы меньше текущего
				}
				maxPoints.AddLast(dataItem.OriginalY);
				yield return dataItem.WithMaxY(maxPoints.First.Value);
			}
		}
	}
}
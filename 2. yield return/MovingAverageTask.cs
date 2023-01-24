using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<double> windowPoints = new Queue<double>();
			double result = 0.0;
			foreach (DataPoint dataItem in data)
			{
				if (windowPoints.Count == windowWidth)
				{
					result = result - windowPoints.Peek();
					windowPoints.Dequeue();
				}
				windowPoints.Enqueue(dataItem.OriginalY);
				result += dataItem.OriginalY;
				yield return dataItem.WithAvgSmoothedY(result / windowPoints.Count);
			}
		}
	}
}
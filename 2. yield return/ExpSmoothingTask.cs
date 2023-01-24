using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			double? previousItem = null;
			foreach (DataPoint dataItem in data)
			{
				if (previousItem == null)
				{
					previousItem = dataItem.OriginalY;
				}
				double expSmooth = (double)(alpha * dataItem.OriginalY + (1 - alpha) * previousItem);
				previousItem = expSmooth;
				yield return dataItem.WithExpSmoothedY(expSmooth);
			}
		}
	}
}
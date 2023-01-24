using System.Text;
using System.Drawing;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon
{
	public class BfsTask
	{
		public static bool CanSkipPoint(Map map, Point point, HashSet<Point> visitedPoints)
		{
			if (!map.InBounds(point) || visitedPoints.Contains(point) || map.Dungeon[point.X, point.Y] != MapCell.Empty)
			{
				return true;
			}
			else return false;
		}

		public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
		{   //Путь от сундука до точки start 
			Queue<SinglyLinkedList<Point>> pathQueue = new Queue<SinglyLinkedList<Point>>();
			pathQueue.Enqueue(new SinglyLinkedList<Point>(start));
			HashSet<Point> visitedPoints = new HashSet<Point>();
			visitedPoints.Add(start);
			HashSet<Point> chestsHS = chests.Select(i => i).ToHashSet();

			while (pathQueue.Count > 0)
			{
				SinglyLinkedList<Point> point = pathQueue.Dequeue();

				if (chestsHS.Contains(point.Value))
				{
					yield return point;
				}


				foreach (Size possiblePoint in Walker.PossibleDirections)
				{
					Point nextP = new Point();
					nextP.X += point.Value.X + possiblePoint.Width;
					nextP.Y += point.Value.Y + possiblePoint.Height;

					if (CanSkipPoint(map, nextP, visitedPoints))
					{
						continue;
					}

					pathQueue.Enqueue(new SinglyLinkedList<Point>(nextP, point));
					visitedPoints.Add(nextP);
				}
			}
		}
	}
}
using System.Text;
using System.Drawing;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Rivals
{
	public class RivalsTask
	{
		public static bool CanSkipPoint(Map map, Point point, HashSet<Point> visitedPoints)
		{
			if (!map.InBounds(point) || visitedPoints.Contains(point) || map.Maze[point.X, point.Y] != MapCell.Empty)
			{
				return true;
			}
			else return false;
		}

		public static IEnumerable<OwnedLocation> AssignOwners(Map map)
		{
			HashSet<Point> visitedPoints = new HashSet<Point>();
			Queue<OwnedLocation> queue = new Queue<OwnedLocation>();

			for (int n = 0; n < map.Players.Length; n++)
			{
				queue.Enqueue(new OwnedLocation(n,    //int Owner                  
							  new Point(map.Players[n].X, map.Players[n].Y), //point Location
							  0));                    //int Distance
				visitedPoints.Add(map.Players[n]);
			}

			while (queue.Count > 0)
			{
				OwnedLocation ownedLocation = queue.Dequeue();
				Point currLocationPoint = ownedLocation.Location;

				yield return new OwnedLocation(ownedLocation.Owner,
											new Point(currLocationPoint.X, currLocationPoint.Y),
											ownedLocation.Distance);

				for (int x = 1; x >= -1; x--)
				{
					for (int y = 1; y >= -1; y--)
					{
						if ((x == 0 && y != 0) || (x != 0 && y == 0))
						{
							Point currPoint = new Point();
							currPoint.X = currLocationPoint.X + x;
							currPoint.Y = currLocationPoint.Y + y;

							if (CanSkipPoint(map, currPoint, visitedPoints))
							{
								continue;
							}

							queue.Enqueue(new OwnedLocation(ownedLocation.Owner,
										new Point(currLocationPoint.X + x, currLocationPoint.Y + y),
										ownedLocation.Distance + 1));
							visitedPoints.Add(currPoint);
						}
						else
						{
							continue;
						}
					}
				}
			}
		}
	}
}

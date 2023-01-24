using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Drawing;
using Greedy.Architecture;
using System.Text;
using System.Linq;

namespace Greedy
{
	public class DijkstraData
	{
		public Point Previous { get; set; }
		public int Price { get; set; }
	}

	public class DijkstraPathFinder
	{   //»дем в взвешенном графе по самым дешевым ребрам и обновл€ем вес тех точек, 
		//у которых предыдущий суммарный путь был больше текущего. “аким образом, точки, 
		//как правило, инициализируютс€ минимальным или близким к минимальному значени€ми, 
		//что впоследствии помогает не обрабатывать длинные пути, а просто прерывать их, если 
		//сумма пути уже больше предыдущего.
		public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
			IEnumerable<Point> targets)
		{
			List<Point> visited = new List<Point>();
			Dictionary<Point, DijkstraData> track = new Dictionary<Point, DijkstraData>();
			track[start] = new DijkstraData { Price = 0, Previous = new Point(-1, -1) };
			List<Point> chestsPoints = targets.ToList();

			while (chestsPoints.Count != 0)
			{
				Point toOpen = GetMinPoint(track, visited);
				if (toOpen == new Point(-1, -1))
				{
					break;
				}

				if (chestsPoints.Contains(toOpen))
				{
					yield return GetResPath(track, toOpen);
					chestsPoints.Remove(toOpen);
				}

				VisitPoint(state, toOpen, track, visited);
				visited.Add(toOpen);
			}
		}

		public static PathWithCost GetResPath(Dictionary<Point, DijkstraData> track, Point end)
		{
			List<Point> res = new List<Point>();
			int resCost = track[end].Price;

			while (end != new Point(-1, -1))
			{
				res.Add(end);
				end = track[end].Previous;
			}

			res.Reverse();
			return new PathWithCost(resCost, res.ToArray());
		}

		private static Point GetMinPoint(Dictionary<Point, DijkstraData> track, List<Point> visited)
		{
			Point toOpen = new Point(-1, -1);
			int bestPrice = int.MaxValue;
			foreach (Point e in track.Keys)
			{
				if (!visited.Contains(e) && track[e].Price < bestPrice)
				{
					bestPrice = track[e].Price;
					toOpen = e;
				}
			}
			return toOpen;
		}

		private static void VisitPoint(State state, Point toOpen, Dictionary<Point, DijkstraData> track, List<Point> visited)
		{
			foreach (Point e in GetNeighborPoints(toOpen, state))
			{
				if (visited.Contains(e))
				{
					continue;
				}

				int currentPrice = track[toOpen].Price + state.CellCost[e.X, e.Y];

				if (!track.ContainsKey(e) || track[e].Price > currentPrice)
				{
					track[e] = new DijkstraData { Previous = toOpen, Price = currentPrice };
				}
			}
		}

		public static List<Point> GetNeighborPoints(Point point, State state)
		{
			List<Point> res = new List<Point>();

			for (int x = -1; x <= 1; x++)   //÷иклы дл€ соседних точек
			{
				for (int y = -1; y <= 1; y++)
				{
					if ((x != 0 && y != 0) || (x == 0 && y == 0)) //
					{
						continue;
					}

					Point neighborPoint = new Point(point.X + x, point.Y + y); //					
					res.Add(neighborPoint);
				}
			}
			return res.Where(p => state.InsideMap(p) && !state.IsWallAt(p)).ToList();
		}
	}
}

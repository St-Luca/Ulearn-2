using System;
using Greedy.Architecture.Drawing;
using Greedy.Architecture;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Greedy
{
	public class NotGreedyPathFinder : IPathFinder
	{
		public int BestChestsCount;
		public List<Point> ResPath;

		public void FindPath(State state, int energySpent, int countFoundChests,
 				  Point startPosition, IEnumerable<Point> chestsPositions, List<Point> currPath)
		{
			if (countFoundChests > BestChestsCount)
			{
				ResPath = currPath;
				BestChestsCount = countFoundChests;
			}

			if (BestChestsCount == state.Chests.Count)
			{
				return;
			}

			DijkstraPathFinder dijkstra = new DijkstraPathFinder();
			var paths = dijkstra.GetPathsByDijkstra(state, startPosition, chestsPositions);

			foreach (PathWithCost pathWithCost in paths)
			{
				if (energySpent + pathWithCost.Cost <= state.Energy)
				{
					IEnumerable<Point> newChests = chestsPositions.Except(new[] { pathWithCost.End });
					List<Point> newCurrPath = currPath.Concat(pathWithCost.Path.Skip(1)).ToList();

					FindPath(state, energySpent + pathWithCost.Cost, countFoundChests + 1, pathWithCost.End, newChests, newCurrPath);
				}
			}
		}

		public List<Point> FindPathToCompleteGoal(State state)
		{
			FindPath(state, 0, 0, state.Position, state.Chests, new List<Point>());

			return ResPath;
		}
	}
}
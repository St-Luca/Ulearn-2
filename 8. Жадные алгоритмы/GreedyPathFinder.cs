using System;
using Greedy.Architecture.Drawing;
using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
	public class GreedyPathFinder : IPathFinder
	{
		public List<Point> FindPathToCompleteGoal(State state)
		{
			List<Point> resList = new List<Point>();
			DijkstraPathFinder dijkstraPathFinder = new DijkstraPathFinder();
			int energy = 0;
			Point startPosition = state.Position;
			HashSet<Point> chestsPoints = state.Chests.ToHashSet();

			for (int n = 0; n < state.Goal; n++)
			{
				PathWithCost pathCost = dijkstraPathFinder.GetPathsByDijkstra(state, startPosition, chestsPoints).FirstOrDefault();

				if (pathCost != null)
				{
					energy += pathCost.Cost;

					if (energy <= state.Energy)
					{
						chestsPoints.Remove(pathCost.End);
						List<Point> preList = (pathCost.Path.Skip(1)).ToList();
						resList.AddRange(preList);
						startPosition = pathCost.End;
					}
					else
					{
						return new List<Point>();
					}
				}
				else
				{
					return new List<Point>();
				}
			}
			return resList;
		}
	}
}
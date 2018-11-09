using graph_coloring;

using System.Collections.Generic;

namespace graph_coloring.solutions
{
  public class LocalSearchSolution : Solution
  {
    public LocalSearchSolution(Graph g, List<int> colors) : base(g, colors)
    {
    }

    public override List<Solution> get_neighbors()
    {
      return new List<Solution>();
    }
  }
}

using System;
using System.Collections.Generic;

using graph_coloring;

namespace graph_coloring.solutions
{
  public class TabooSearchSolution : LocalSearchSolution
  {
    public TabooSearchSolution(Graph g, List<int> c, List<List<Node>> cc = null) : base(g, c, cc)
    {
    }
  }
}

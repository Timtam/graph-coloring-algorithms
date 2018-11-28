using System;
using System.Collections.Generic;

using graph_coloring;

namespace graph_coloring.algorithms
{
  public class TabooSearchAlgorithm : LocalSearchAlgorithm
  {
    public TabooSearchAlgorithm(Graph g) : base(g, "taboo search")
    {
    }
  }
}

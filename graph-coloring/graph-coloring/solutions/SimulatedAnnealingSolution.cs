using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using graph_coloring;

namespace graph_coloring.solutions
{
  public class SimulatedAnnealingSolution : LocalSearchSolution
  {
    public SimulatedAnnealingSolution(Graph g, List<int> c) : base(g, c)
    {
    }
  }
}

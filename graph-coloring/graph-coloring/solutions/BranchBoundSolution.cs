using graph_coloring;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace graph_coloring.solutions
{
  public class BranchBoundSolution : Solution
  {
    public BranchBoundSolution(Graph g, int[] colors) : base(g, colors)
    {
    }

    public override IEnumerable<object> GetNextNeighbor()
    {
      yield return null;
    }
  }
}

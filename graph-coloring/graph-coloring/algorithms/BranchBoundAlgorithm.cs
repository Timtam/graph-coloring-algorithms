using graph_coloring;
using graph_coloring.solutions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace graph_coloring.algorithms
{
  public class BranchBoundAlgorithm : Algorithm
  {
    public BranchBoundAlgorithm(Graph g) : base(g, "branch-bound")
    {
    }

    public override Solution Run()
    {
      return null;
    }

    public override void SetTimeout(int t)
    {
      return;
    }
  }
}

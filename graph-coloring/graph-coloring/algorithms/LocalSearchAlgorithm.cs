// local search implementation
// derives Algorithm class

using graph_coloring;
using graph_coloring.solutions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace graph_coloring.algorithms
{
  public class LocalSearchAlgorithm : Algorithm
  {
    public LocalSearchAlgorithm(Graph g) : base(g, "local-search")
    {
    }

    // runs the local search algorithm on the given graph
    public override Solution Run()
    {
      Solution s;
      double w, tmp_w;
      bool found = true;

      this.RunBefore();

      s = this.GetGreedySolution<LocalSearchSolution>();
      w = s.GetWorth();

      while(found)
      {
        found = false;
        foreach(Solution t in s.GetNextNeighbor())
        {
          tmp_w = t.GetWorth();
          if(tmp_w < w)
          {
            s = t;
            w = tmp_w;
            found = true;
            break;
          }
        }
      }
      this.RunAfter();

      return s;
    }

    public override void SetTimeout(int t)
    {
      return;
    }
  }
}

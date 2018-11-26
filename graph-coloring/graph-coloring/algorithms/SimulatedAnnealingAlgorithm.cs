using System;
using System.Collections.Generic;

using graph_coloring;
using graph_coloring.solutions;

namespace graph_coloring.algorithms
{
  public class SimulatedAnnealingAlgorithm : LocalSearchAlgorithm
  {
    public SimulatedAnnealingAlgorithm(Graph g) : base(g, "simulated annealing")
    {
    }

    public Solution Run(int start_temp)
    {
      bool found = true;
      int k = 0;
      Solution s = this.GetGreedySolution();
      double w = s.GetWorth();
      double tmp_w;
      double p;
      
      this.RunBefore();

      while(found)
      {
        found = false;
        k++;

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
          else if(tmp_w > w)
          {
            p = Math.Exp((w - tmp_w) * Math.Log(k, 2) / start_temp);

            if(Randomizer.NextDouble() < p)
            {
              s = t;
              w = tmp_w;
              found = true;
              break;
            }
          }
        }
      }

      this.RunAfter();
      return s;
    }
  }
}

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
      BranchBoundSolution s = new BranchBoundSolution(this.graph, null);
      BranchBoundSolution tmp_s;
      BranchBoundSolution valid_s = s.Copy<BranchBoundSolution>();
      int lim;
      Stack<BranchBoundSolution> ss = new Stack<BranchBoundSolution>();

      ss.Push(s);

      valid_s.ApplyGreedyColoring();

      lim = valid_s.ColorCount;

      this.RunBefore();
      
      while(ss.Count > 0)
      {
        s = ss.Pop();

        if(s.IsValid())
        {
          if(s.ColorCount < lim)
          {
            valid_s = s;
            lim = s.ColorCount;
          }
          continue;
        }

        tmp_s = s.Copy<BranchBoundSolution>();
        
        tmp_s.ApplyGreedyColoring();
        
        if(tmp_s.IsValid() == false
          || tmp_s.ColorCount > lim
        )
          continue;

        foreach(BranchBoundSolution neighbor in s.GetNextNeighbor())
          ss.Push(neighbor);
      }

      this.RunAfter();
      
      return valid_s;
    }

    public override void SetTimeout(int t)
    {
      return;
    }
  }
}

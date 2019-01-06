using graph_coloring;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace graph_coloring.solutions
{
  public class Solution
  {
    protected Graph graph;
    public int[] colors;
    private double worth;

    public Solution(Graph g, int[] colors)
    {
      this.graph = g;
      this.colors = colors;
      this.worth = double.MaxValue;
    }

    public virtual IEnumerable<object> GetNextNeighbor()
    {
      yield return new Solution(null, null);
    }
    
    public double GetWorth()
    {
      double w = 0;
      int[] invalid_edges;
      int[] ccl;
      Object sync = new Object();

      if(this.worth != double.MaxValue)
        return this.worth;

      invalid_edges = this.GetInvalidEdges();
      
      ccl = new int[invalid_edges.Length];

      Parallel.For(0, this.colors.Length, (i) =>
      {
        Interlocked.Increment(ref ccl[this.colors[i] - 1]);
      });

      Parallel.For<double>(0, invalid_edges.Length,
        () => 0.0,
        (int i, ParallelLoopState pls, double state) =>
        {
          state += (2.0*invalid_edges[i] - ccl[i]) * ccl[i];
          return state;
        },
        (double state) => {
          lock (sync)
          {
            w += state;
          }
        }
      );
        
      this.worth = w;

      return w;
    }

    public int ColorCount
    {
      get
      {
        return this.colors.Distinct().Count();
      }
    }

    public bool IsValid()
    {
      int[] invalid = this.GetInvalidEdges();

      Array.Sort(invalid);

      return invalid[invalid.Length - 1] == 0;
    }

    protected int GetUnusedColor()
    {
      int i;
      int[] colors;
      int clr = 1;

      colors = this.colors.Distinct().ToArray();
      
      Array.Sort(colors);

      for(i=0; i < colors.Length; i++)
      {
        if(colors[i] > clr)
          return clr;
        clr++;
      }

      return clr;
    }

    protected int[] GetInvalidEdges()
    {
      int[] edges = new int[this.colors.Max()];
      
      Parallel.For(0, this.graph.EdgeCount, i =>
      {
        if(this.colors[this.graph.Edges[i].A.ID] == this.colors[this.graph.Edges[i].B.ID])
          Interlocked.Increment(ref edges[this.colors[this.graph.Edges[i].A.ID] - 1]);
      });

      return edges;
    }
  }
}

using graph_coloring;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace graph_coloring.solutions
{
  public class LocalSearchSolution : Solution
  {
    public LocalSearchSolution(Graph g, int[] colors) : base(g, colors)
    {
    }

    public override IEnumerable<object> GetNextNeighbor()
    {
      List<int> c;
      int[] lc;
      int i,j;
      Node n;

      for(i=0; i < this.graph.NodeCount; i++)
      {

        n = this.graph.GetNode(i);

        c = new List<int>(this.colors.Distinct());
        c.Remove(this.colors[n.ID]);
        c.Add(this.GetUnusedColor());

        for(j=0; j < c.Count; j++)
        {
          lc = new int[this.colors.Length];
          Array.Copy(this.colors, lc, lc.Length);
            
          lc[n.ID] = c[j];
          yield return new LocalSearchSolution(this.graph, lc);
        }

      }
    }

    public override double GetWorth()
    {
      double w = 0;
      int[] invalid_edges;
      int[] ccl;
      Object sync = new Object();

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
        
      return w;
    }
  }
}

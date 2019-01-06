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
  }
}

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

      int[] c;
      int i;
      int node_offset = Array.IndexOf(this.colors, 0);

      if(node_offset == -1)
        yield return null;

      c = new int[this.colors.Length];

      Array.Copy(this.colors, c, c.Length);

      for(i=0; i < this.graph.NodeCount; i++)
      {
        c[node_offset] = i + 1;

        yield return new BranchBoundSolution(this.graph, c);
      }
    }
  }
}

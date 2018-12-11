using System;
using System.Collections.Generic;
using System.Linq;

using graph_coloring;

namespace graph_coloring.solutions
{
  public class SimulatedAnnealingSolution : LocalSearchSolution
  {
    public SimulatedAnnealingSolution(Graph g, int[] c) : base(g, c)
    {
    }

    public override IEnumerable<object> GetNextNeighbor()
    {
      List<int> c;
      int[] lc;
      int i,j;
      Node n;
      Dictionary<int, List<int>> processed;

      processed = new Dictionary<int, List<int>>(this.graph.NodeCount);

      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        c = new List<int>(this.colors.Distinct());
        c.Remove(this.colors[n.ID]);
        c.Add(this.GetUnusedColor());

        processed.Add(i, c);
      }

      while(processed.Count > 0)
      {

        i = processed.Keys.ToList()[Randomizer.Next(processed.Keys.Count)];

        n = this.graph.GetNode(i);

        j = processed[i][Randomizer.Next(processed[i].Count)];

        processed[i].Remove(j);
        if(processed[i].Count == 0)
          processed.Remove(i);

        lc = new int[this.colors.Length];
        Array.Copy(this.colors, lc, lc.Length);
        lc[n.ID] = j;
        yield return new SimulatedAnnealingSolution(this.graph, lc);
      }
    }
  }
}

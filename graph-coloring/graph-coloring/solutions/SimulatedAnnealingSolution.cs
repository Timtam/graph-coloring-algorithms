using System;
using System.Collections.Generic;
using System.Linq;

using graph_coloring;

namespace graph_coloring.solutions
{
  public class SimulatedAnnealingSolution : LocalSearchSolution
  {
    public SimulatedAnnealingSolution(Graph g, int[] c, List<List<Node>> cc = null) : base(g, c, cc)
    {
    }

    public override IEnumerable<object> GetNextNeighbor()
    {
      List<List<Node>> ccl;
      List<int> c;
      int[] lc;
      int i,j;
      Node n;
      Dictionary<int, List<int>> processed;

      ccl = new List<List<Node>>(this.color_classes.Count + 1);
        
      for(j=0; j < this.color_classes.Count; j++)
        ccl.Add(new List<Node>(this.color_classes[j]));
      ccl.Add(new List<Node>(this.color_classes[0].Capacity));

      processed = new Dictionary<int, List<int>>(this.graph.NodeCount);

      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        c = new List<int>(this.color_classes.Count);

        for(j=0; j < this.color_classes.Count; j++)
        {
          if(this.color_classes[j].Count > 0 && (this.colors[n.ID] - 1) != j)
          {
            c.Add(j + 1);
          }
        }
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
        ccl[lc[n.ID] - 1].Remove(n);
        lc[n.ID] = j;
        ccl[lc[n.ID] - 1].Add(n);
        yield return new SimulatedAnnealingSolution(this.graph, lc, ccl);
        ccl[lc[n.ID] - 1].Remove(n);
        ccl[this.colors[n.ID] - 1].Add(n);
      }
    }
  }
}

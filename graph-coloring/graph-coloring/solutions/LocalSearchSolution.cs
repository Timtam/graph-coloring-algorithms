using graph_coloring;

using System;
using System.Collections.Generic;

namespace graph_coloring.solutions
{
  public class LocalSearchSolution : Solution
  {
    public LocalSearchSolution(Graph g, List<int> colors) : base(g, colors)
    {
    }

    public override List<Solution> GetNeighbors()
    {
      List<int> c, lc;
      List<Solution> neighbors;
      int i;
      int n = new Random().Next(this.graph.NodeCount);
      Node nn = this.graph.GetNode(n);

      c = new List<int>(this.color_classes.Count);

      for(i=0; i < this.color_classes.Count; i++)
      {
        if(this.color_classes[i].Count > 0 && (this.colors[nn.ID] + 1) != i)
          c.Add(i + 1);
      }
      c.Add(this.GetUnusedColor());

      neighbors = new List<Solution>(c.Count);

      for(i=0; i < c.Count; i++)
      {
        lc = new List<int>(this.colors);
        lc[nn.ID] = c[i];
        neighbors.Add(new LocalSearchSolution(this.graph, lc));
      }

      return neighbors;
    }

    public override double GetWorth()
    {
      double w = 0;
      int invalid_edges = 0;
      int i,j;
      Node n,p;

      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        for(j=0; j < n.NeighborCount; j++)
        {
          p = n.GetNeighbor(j);
          if(this.colors[n.ID] == this.colors[p.ID])
            invalid_edges++;
        }
      }
      
      w = (invalid_edges*this.ColorCount*2) - Math.Pow(this.ColorCount, 2);

      return w;
    }
  }
}

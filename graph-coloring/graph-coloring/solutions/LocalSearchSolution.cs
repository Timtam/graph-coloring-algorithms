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

    public override IEnumerable<Solution> GetNextNeighbor()
    {
      List<int> c, lc;
      int i,j;
      Node n;

      for(i=0; i < this.graph.NodeCount; i++)
      {

        n = this.graph.GetNode(i);

        c = new List<int>(this.color_classes.Count);

        for(j=0; j < this.color_classes.Count; j++)
        {
          if(this.color_classes[j].Count > 0 && (this.colors[n.ID] + 1) != i)
            c.Add(j + 1);
        }
        c.Add(this.GetUnusedColor());

        for(j=0; j < c.Count; j++)
        {
          lc = new List<int>(this.colors);
          lc[n.ID] = c[j];
          yield return new LocalSearchSolution(this.graph, lc);
        }

      }
    }

    public override double GetWorth()
    {
      double w = 0;
      int i,j;
      List<int> invalid_edges = new List<int>(this.color_classes.Count);
      Node n,p;

      for(i=0; i < this.color_classes.Count; i++)
        invalid_edges.Add(0);

      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        for(j=0; j < n.NeighborCount; j++)
        {
          p = n.GetNeighbor(j);
          if(this.colors[n.ID] == this.colors[p.ID])
            invalid_edges[this.colors[n.ID] - 1]++;
        }
      }

      // each edge counts double
      for(i=0; i < invalid_edges.Count; i++)
        invalid_edges[i] /= 2;
      
      for(i=0; i < invalid_edges.Count; i++)
      {
        w += (2*invalid_edges[i] - this.color_classes[i].Count) * this.color_classes[i].Count;
      }

      return w;
    }
  }
}

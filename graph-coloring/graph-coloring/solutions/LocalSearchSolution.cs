using graph_coloring;

using System;
using System.Collections.Generic;

namespace graph_coloring.solutions
{
  public class LocalSearchSolution : Solution
  {
    public LocalSearchSolution(Graph g, List<int> colors, List<List<Node>> color_classes = null) : base(g, colors, color_classes)
    {
    }

    public override IEnumerable<Solution> GetNextNeighbor()
    {
      List<List<Node>> ccl;
      List<int> c, lc;
      int i,j;
      Node n;

      ccl = new List<List<Node>>(this.color_classes.Count + 1);
        
      for(j=0; j < this.color_classes.Count; j++)
        ccl.Add(new List<Node>(this.color_classes[j]));
      ccl.Add(new List<Node>(this.color_classes[0].Capacity));

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

        for(j=0; j < c.Count; j++)
        {
          lc = new List<int>(this.colors);
          ccl[lc[n.ID] - 1].Remove(n);
          lc[n.ID] = c[j];
          ccl[lc[n.ID] - 1].Add(n);
          yield return new LocalSearchSolution(this.graph, lc, ccl);
          ccl[lc[n.ID] - 1].Remove(n);
          ccl[this.colors[n.ID] - 1].Add(n);
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

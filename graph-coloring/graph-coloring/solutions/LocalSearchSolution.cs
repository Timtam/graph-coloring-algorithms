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
      List<int> c;
      List<Solution> neighbors = new List<Solution>(this.graph.NodeCount);
      int i;
      Random r = new Random();

      for(i=0; i < this.graph.NodeCount; i++)
      {
        c = new List<int>(this.colors);
        c[this.graph.GetNode(i).ID] = r.Next(1, this.ColorCount + 1);
        neighbors.Add(new LocalSearchSolution(this.graph, c));
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

using graph_coloring;

using System;
using System.Collections.Generic;
using System.Linq;

namespace graph_coloring.algorithms
{
  public class LocalSearch : Algorithm
  {
    public LocalSearch(Graph g) : base(g)
    {
    }

    public void Run()
    {
      int i;
      Node n;

      // bleaching all previous colorizations
     
      this.RunBefore();

      // looping over each node in the graph
      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);

        do
        {
          // if it isn't colored yet, add its color
          if(this.colors[n.ID] == 0)
            this.colors[n.ID] = this.GetPossibleColor(n);
        }
        while((n = this.GetBleachedNeighbor(n)) != null);
      }
      this.RunAfter();
    }

    private Node GetBleachedNeighbor(Node n)
    {
      int i;
      Node m;
      
      for(i=0; i < n.NeighborCount; i++)
      {
        m = n.GetNeighbor(i);
        if(this.colors[m.ID] == 0)
          return m;
      }

      return null;
    }

    private int GetPossibleColor(Node n)
    {
      List<int> colors = new List<int>(n.NeighborCount);
      int color = 1;
      int i;
      Node m;
      
      for(i=0; i<n.NeighborCount; i++)
      {
        m = n.GetNeighbor(i);
        if(this.colors[m.ID] > 0)
          colors.Add(this.colors[m.ID]);
      }

      colors = colors.Select(x => x).Distinct().ToList();

      colors.Sort();

      for(i=0; i<colors.Count; i++)
      {
        if(colors[i] != color)
          break;
        color++;
      }

      return color;
    }
  }
}

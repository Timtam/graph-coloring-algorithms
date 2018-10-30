// local search implementation
// derives Algorithm class

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

    // runs the local search algorithm on the given graph
    public void Run()
    {
      int i;
      Node n;

      this.RunBefore();

      // looping over each node in the graph
      // secures against non-connected graphs
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

    // retrieves the next bleached neighbor from all of n's neighbors
    // doesn't follow a specific logic, simply select the first one found
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

    // checks all neighbors of n and returns the best color to use
    // the best color is either the smallest color already used for this graph, 
    // but not yet used for any neighbor, or a totally new color (if needed)
    // we extract all colors used by neighbors
    // distinct the list, sort it and loop until we find a valid color
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

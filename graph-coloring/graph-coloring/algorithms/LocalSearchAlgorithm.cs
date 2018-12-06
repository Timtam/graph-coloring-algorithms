// local search implementation
// derives Algorithm class

using graph_coloring;
using graph_coloring.solutions;

using System;
using System.Collections.Generic;
using System.Linq;

namespace graph_coloring.algorithms
{
  public class LocalSearchAlgorithm : Algorithm
  {
    public LocalSearchAlgorithm(Graph g, string name = "local search") : base(g, name)
    {
    }

    protected T GetGreedySolution<T>()
    {
      int i;
      Node n;

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

      // we finished with that solution
      return (T)Activator.CreateInstance(typeof(T), this.graph, this.colors.ToArray(), null);
    }

    protected T GetSingleColoredSolution<T>()
    {
      int i;
      
      for(i=0; i < this.graph.NodeCount; i++)
        this.colors[i] = 1;

      return (T)Activator.CreateInstance(typeof(T), this.graph, this.colors.ToArray(), null);
    }

    // runs the local search algorithm on the given graph
    public override Solution Run()
    {
      Solution s;
      double w, tmp_w;
      bool found = true;

      this.RunBefore();

      s = this.GetGreedySolution<LocalSearchSolution>();
      w = s.GetWorth();

      while(found)
      {
        found = false;
        foreach(Solution t in s.GetNextNeighbor())
        {
          tmp_w = t.GetWorth();
          if(tmp_w < w)
          {
            s = t;
            w = tmp_w;
            found = true;
            break;
          }
        }
      }
      this.RunAfter();

      return s;
    }

    // retrieves the next bleached neighbor from all of n's neighbors
    // doesn't follow a specific logic, simply select the first one found
    private Node GetBleachedNeighbor(Node n)
    {
      int i;
      Node m;
      List<Node> neighbors = this.graph.GetNeighbors(n);
      
      for(i=0; i < n.NeighborCount; i++)
      {
        m = neighbors[i];
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
      List<Node> neighbors = this.graph.GetNeighbors(n);
      
      for(i=0; i<n.NeighborCount; i++)
      {
        m = neighbors[i];
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

    public override void SetTimeout(int t)
    {
      return;
    }
  }
}

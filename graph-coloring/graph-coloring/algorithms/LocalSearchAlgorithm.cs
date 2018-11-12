﻿// local search implementation
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
    public LocalSearchAlgorithm(Graph g) : base(g)
    {
    }

    protected LocalSearchSolution GetGreedySolution()
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
      return new LocalSearchSolution(this.graph, this.colors);
    }

    // runs the local search algorithm on the given graph
    public Solution Run()
    {
      int i;
      List<Solution> neighbors;
      Solution s,t;
      double w;
      bool found = true;

      this.RunBefore();

      s = this.GetGreedySolution();
      w = s.GetWorth();
      neighbors = s.GetNeighbors();
      s = neighbors[new Random().Next(neighbors.Count)];

      while(found)
      {
        found = false;
        neighbors = s.GetNeighbors();
        for(i=0; i < neighbors.Count; i++)
        {
          t = neighbors[i];
          if(t.GetWorth() < w)
          {
            s = t;
            w = s.GetWorth();
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
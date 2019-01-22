using graph_coloring;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace graph_coloring.solutions
{
  public class Solution : IColorable
  {
    protected Graph graph;
    protected int[] colors;
    private double worth;

    public Solution(Graph g, int[] colors = null)
    {
      this.graph = g;
      this.colors = colors;
      this.worth = double.MaxValue;

      if(this.colors == null)
        this.colors = new int[this.graph.NodeCount];
    }

    public virtual IEnumerable<object> GetNextNeighbor()
    {
      yield return new Solution(null, null);
    }
    
    public double GetWorth()
    {
      double w = 0;
      int[] invalid_edges;
      int[] ccl;
      int k;
      Object sync = new Object();

      if(this.worth != double.MaxValue)
        return this.worth;

      invalid_edges = this.GetInvalidEdges();
      
      ccl = new int[invalid_edges.Length];

      for(k=0; k < this.colors.Length; k++)
        ccl[this.colors[k] - 1]++;

      Parallel.For<double>(0, invalid_edges.Length,
        () => 0.0,
        (int i, ParallelLoopState pls, double state) =>
        {
          state += (2.0*invalid_edges[i] - ccl[i]) * ccl[i];
          return state;
        },
        (double state) => {
          lock (sync)
          {
            w += state;
          }
        }
      );
        
      this.worth = w;

      return w;
    }

    public int ColorCount
    {
      get
      {
        return this.colors.Distinct().Count();
      }
    }

    public bool IsValid()
    {
      int[] invalid = this.GetInvalidEdges();

      Array.Sort(invalid);

      return invalid[invalid.Length - 1] == 0;
    }

    protected int GetUnusedColor()
    {
      int i;
      int[] colors;
      int clr = 1;

      colors = this.colors.Distinct().ToArray();
      
      Array.Sort(colors);

      for(i=0; i < colors.Length; i++)
      {
        if(colors[i] > clr)
          return clr;
        clr++;
      }

      return clr;
    }

    protected int[] GetInvalidEdges()
    {
      int[] edges = new int[this.colors.Max()];
      
      Parallel.For(0, this.graph.EdgeCount, i =>
      {
        if(this.colors[this.graph.Edges[i].A.ID] == this.colors[this.graph.Edges[i].B.ID])
          Interlocked.Increment(ref edges[this.colors[this.graph.Edges[i].A.ID] - 1]);
      });

      return edges;
    }

    public T Copy<T>()
    {
      return (T)Activator.CreateInstance(typeof(T), this.graph, this.colors);
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

      colors = colors.Distinct().ToList();

      colors.Sort();

      for(i=0; i<colors.Count; i++)
      {
        if(colors[i] != color)
          break;
        color++;
      }

      return color;
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

    public void ApplyGreedyColoring()
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
    }

    public void ApplySingleColoring()
    {
      int i;
      
      for(i=0; i < this.graph.NodeCount; i++)
        if(this.colors[i] == 0)
          this.colors[i] = 1;
    }
  }
}

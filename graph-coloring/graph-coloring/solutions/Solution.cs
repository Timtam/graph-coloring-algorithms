using graph_coloring;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace graph_coloring.solutions
{
  public class Solution
  {
    protected Graph graph;
    protected int[] colors;

    public Solution(Graph g, int[] colors)
    {
      this.graph = g;
      this.colors = colors;
    }

    public virtual IEnumerable<object> GetNextNeighbor()
    {
      yield return new Solution(null, null);
    }
    
    public virtual double GetWorth()
    {
      return 0;
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
  }
}

using graph_coloring;

using System;
using System.Collections.Generic;

namespace graph_coloring.solutions
{
  public class Solution
  {
    protected Graph graph;
    protected List<int> colors;
    protected List<List<Node>> color_classes;

    public Solution(Graph g, List<int> colors, List<List<Node>> color_classes = null)
    {
      List<int> c;
      int i,sum;
      Node n;

      this.graph = g;
      this.colors = colors;
      this.color_classes = color_classes;

      if(this.color_classes != null)
        return;

      // calculating amount of different colors
      c = new List<int>(this.colors.Count);

      for(i=0; i < this.colors.Count; i++)
        c.Add(0);
        
      for(i=0; i<this.colors.Count; i++)
        if(this.colors[i] > 0)
          c[this.colors[i] - 1]++;
        
      sum = 1;
      for(i=0; i<this.colors.Count; i++)
        if(c[this.colors[i] - 1] > 0 && this.colors[i] > sum)
          sum = this.colors[i];

      this.color_classes = new List<List<Node>>(sum);
      for(i=0; i < sum; i++)
        this.color_classes.Add(new List<Node>(this.graph.NodeCount / sum));

      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        this.color_classes[this.colors[n.ID] - 1].Add(n);
      }
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
        int i;
        int sum = this.color_classes.Count;
        
        for(i=0; i < this.color_classes.Count; i++)
          if(this.color_classes[i].Count == 0)
            sum--;

        return sum;
      }
    }

    public bool IsValid()
    {
      List<int> invalid = this.GetInvalidEdges();

      invalid.Sort();

      return invalid[invalid.Count - 1] == 0;
    }

    protected int GetUnusedColor()
    {
      int i;
      
      for(i=0; i < this.colors.Count; i++)
      {
        if(i < this.color_classes.Count && this.color_classes[i].Count == 0)
          return i + 1;
        else if(i >= this.color_classes.Count)
          break;
      }
      return i + 1;
    }

    protected List<int> GetInvalidEdges()
    {
      List<int> edges = new List<int>(this.color_classes.Count);
      int i;
      
      for(i=0; i < this.color_classes.Count; i++)
        edges.Add(0);

      for(i=0; i < this.graph.EdgeCount; i++)
      {
        if(this.colors[this.graph.Edges[i].A.ID] == this.colors[this.graph.Edges[i].B.ID])
          edges[this.colors[this.graph.Edges[i].A.ID] - 1]++;
      }

      return edges;
    }
  }
}

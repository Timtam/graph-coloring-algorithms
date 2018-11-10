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
    public Solution(Graph g, List<int> colors)
    {
      List<int> c;
      int i,sum;
      Node n;

      this.graph = g;
      this.colors = colors;

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

    public virtual List<Solution> get_neighbors()
    {
      return new List<Solution>();
    }
    
    public virtual double get_worth()
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

    public bool is_valid()
    {
      int i,j;
      Node n,n2;
      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        for(j=0; j < n.NeighborCount; j++)
        {
          n2 = n.GetNeighbor(j);
          if(this.colors[n.ID] == this.colors[n2.ID])
            return false;
        }
      }
      return true;
    }
  }
}

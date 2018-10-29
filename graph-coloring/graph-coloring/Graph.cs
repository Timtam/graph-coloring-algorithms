using System;
using System.Collections.Generic;

namespace graph_coloring
{
  public class Graph
  {
    private List<Node> graph;
    public Graph(int nodes)
    {
      int i;
      this.graph = new List<Node>(nodes);
      for(i=0; i < nodes; i++)
        this.graph.Add(new Node(i));
    }

    public void AddEdge(int from, int to)
    {
      this.graph[from].AddNeighbor(this.graph[to]);
      this.graph[to].AddNeighbor(this.graph[from]);
    }

    public int NodeCount
    {
      get
      {
        return this.graph.Count;
      }
    }

    public int EdgeCount
    {
      get
      {
        int i,j;
        List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
        Tuple<int, int> edge,redge;

        for(i=0; i < this.graph.Count; i++)
        {
          for(j=0; j<this.graph[i].NeighborCount; j++)
          {
            edge = new Tuple<int, int>(this.graph[i].ID, this.graph[i].GetNeighbor(j).ID);
            redge = new Tuple<int, int>(edge.Item2, edge.Item1);
            if(edges.IndexOf(redge) == -1)
              edges.Add(edge);
          }
        }
        return edges.Count;
      }
    }

    public Node GetNode(int idx)
    {
      if(idx >= this.graph.Count)
        throw new System.ArgumentOutOfRangeException("idx out of range", "idx");
      return this.graph[idx];
    }
  }
}

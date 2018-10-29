using System;
using System.Collections.Generic;

namespace graph_coloring
{
  public class Graph
  {
    private int _edges;
    private List<Node> graph;
    public Graph(int nodes)
    {
      int i;
      this._edges = 0;
      this.graph = new List<Node>(nodes);
      for(i=0; i < nodes; i++)
        this.graph.Add(new Node(i));
    }

    public void AddEdge(int from, int to)
    {
      this.graph[from].AddNeighbor(this.graph[to]);
      this.graph[to].AddNeighbor(this.graph[from]);
      this._edges++;
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
        return this._edges;
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

// a graph class
// has a certain amount of nodes (Node objects)
// it can also add edges between nodes

using System;
using System.Collections.Generic;
using System.Linq;

namespace graph_coloring
{
  public class Graph
  {
    private List<List<Node>> neighbors;
    private List<Node> graph;
    public readonly List<Edge> Edges;
    public Graph(int nodes)
    {
      int i;
      this.Edges = new List<Edge>();
      this.graph = new List<Node>(nodes);
      for(i=0; i < nodes; i++)
        this.graph.Add(new Node(i));
      this.neighbors = new List<List<Node>>(this.NodeCount);

      for(i=0; i < this.NodeCount; i++)
        this.neighbors.Add(new List<Node>());
    }

    public void AddEdge(int from, int to)
    {

      if(from == to) // we ignore circular edges
        return;

      this.Edges.Add(new Edge(this.graph[from], this.graph[to]));
      this.graph[from].IncrementNeighborCount();
      this.graph[to].IncrementNeighborCount();
      this.neighbors[from].Add(this.graph[to]);
      this.neighbors[to].Add(this.graph[from]);
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
        return this.Edges.Count;
      }
    }

    public Node GetNode(int idx)
    {
      if(idx >= this.graph.Count)
        throw new System.ArgumentOutOfRangeException("idx out of range", "idx");
      return this.graph[idx];
    }

    public List<Node> GetNeighbors(Node n)
    {
      return this.neighbors[n.ID];
    }
  }
}

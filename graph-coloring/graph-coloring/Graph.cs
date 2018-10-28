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
        this.graph.Add(new Node());
    }

    public void AddEdge(int from, int to)
    {
      this.graph[from].AddNeighbor(this.graph[to]);
      this.graph[to].AddNeighbor(this.graph[from]);
    }
  }
}

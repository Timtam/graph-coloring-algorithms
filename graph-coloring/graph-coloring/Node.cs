using System.Collections.Generic;

namespace graph_coloring
{
  public class Node
  {
    readonly List<Node> neighbors;

    public Node()
    {
      this.neighbors = new List<Node>();
    }

    public void AddNeighbor(Node n)
    {
      this.neighbors.Add(n);
    }
  }
}

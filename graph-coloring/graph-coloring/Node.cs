using System.Collections.Generic;

namespace graph_coloring
{
  public class Node
  {
    private List<Node> neighbors;
    public readonly int ID;

    public Node()
    {
      this.neighbors = new List<Node>();
    }

    public Node(int id) : this()
    {
      this.ID = id;
    }

    public void AddNeighbor(Node n)
    {
      this.neighbors.Add(n);
    }

    public Node GetNeighbor(int idx)
    {
      if(idx >= this.NeighborCount)
        throw new System.ArgumentOutOfRangeException("index out of range", "idx");
      return this.neighbors[idx];
    }

    public int NeighborCount
    {
      get
      {
        return this.neighbors.Count;
      }
    }
  }
}

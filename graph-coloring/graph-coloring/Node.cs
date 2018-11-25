// a node class
// has an ID and knows its neighbors

using System.Collections.Generic;

namespace graph_coloring
{
  public class Node
  {
    public readonly int ID;
    public int _neighbor_count;

    public Node()
    {
      this._neighbor_count = 0;
    }

    public Node(int id) : this()
    {
      this.ID = id;
    }

    public override string ToString()
    {
      return "Node " + this.ID;
    }

    public void IncrementNeighborCount()
    {
      this._neighbor_count++;
    }

    public int NeighborCount
    {
      get
      {
        return this._neighbor_count;
      }
    }
  }
}

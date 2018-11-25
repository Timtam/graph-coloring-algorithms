namespace graph_coloring
{
  public class Edge
  {
    public readonly Node A;
    public readonly Node B;

    public Edge(Node a, Node b)
    {
      this.A = a;
      this.B = b;
    }
  }
}

using graph_coloring;

namespace graph_coloring.features
{
  public class Feature
  {
    public readonly Node Node;
    public readonly int Color;
    public double Cost;

    public Feature(Node n, int c)
    {
      this.Node = n;
      this.Color = c;
      this.Cost = 0.0;
    }
  }
}
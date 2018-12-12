using graph_coloring;

namespace graph_coloring.features
{
  public struct Feature
  {
    public Node Node;
    public int Color;
    public double Cost;

    public override string ToString()
    {
      return "Feature for node (" + this.Node + ") with color " + this.Color + " and cost " + this.Cost;
    }
  }
}
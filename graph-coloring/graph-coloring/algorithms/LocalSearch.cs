using graph_coloring;

namespace graph_coloring.algorithms
{
  public class LocalSearch : Algorithm
  {
    public LocalSearch(Graph g) : base(g)
    {
    }

    public void Run()
    {
      int i;
      Node n;

      // bleaching all previous colorizations
      this.Bleach();
      
      // looping over each node in the graph
      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);

        do
        {
          // if it isn't colored yet, add its color
          if(this.colors[n.ID] == 0)
            this.colors[n.ID] = this.GetPossibleColor(n);
        }
        while((n = this.GetBleachedNeighbor(n)) != null);
      }
    }

    private Node GetBleachedNeighbor(Node n)
    {
      int i;
      Node m;
      
      for(i=0; i < n.NeighborCount; i++)
      {
        m = n.GetNeighbor(i);
        if(this.colors[m.ID] == 0)
          return m;
      }

      return null;
    }

    private int GetPossibleColor(Node n)
    {
      bool found = false;
      int color = 0;
      int i;
      int tmp_color;
      
      while(!found)
      {
        found = true;
        color++;
        for(i=0; i < n.NeighborCount; i++)
        {
          tmp_color = this.colors[n.GetNeighbor(i).ID];
          if(tmp_color > 0 && tmp_color == color)
          {
            found = false;
            break;
          }
        }
      }
      
      return color;
    }
  }
}

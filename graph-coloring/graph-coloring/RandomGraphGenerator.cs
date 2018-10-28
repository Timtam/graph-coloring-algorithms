using System;

namespace graph_coloring
{
  public static class RandomGraphGenerator
  {
    public static Graph Generate(int nodes)
    {
      Graph g = new Graph(nodes);
      int i,j,r;
      Random random = new Random();
      
      for(i=0; i<nodes; i++)
      {
        for(j=0; j<i; j++)
        {
          r = random.Next(100);
          if(r >= 30)
            g.AddEdge(i, j);
        }
      }
      return g;
    }
  }
}

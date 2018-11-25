// this class can generate a random graph with a given amount of nodes
// it doesn't take care about connected graphs (yet)
// it simply creates x nodes and randomly connects them

using System;

namespace graph_coloring
{
  public static class RandomGraphGenerator
  {
    public static Graph Generate(int nodes)
    {
      Graph g = new Graph(nodes);
      int i,j,r;
      
      for(i=0; i<nodes; i++)
      {
        for(j=0; j<i; j++)
        {
          r = Randomizer.Next(100);
          if(r >= 30)
            g.AddEdge(i, j);
        }
      }

      return g;
    }
  }
}

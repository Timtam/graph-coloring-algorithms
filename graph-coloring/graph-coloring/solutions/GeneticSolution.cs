using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using graph_coloring;

namespace graph_coloring.solutions
{
  public class GeneticSolution : LocalSearchSolution
  {

    public GeneticSolution(Graph g, int[] c) : base(g, c)
    {
    }

    public static Tuple<GeneticSolution, GeneticSolution> OnePointCrossover(GeneticSolution s1, GeneticSolution s2)
    {
      int[] c;
      int p = Randomizer.Next(s1.graph.NodeCount-1)+1;
      GeneticSolution o1, o2;

      c = s1.colors.Take(p).Concat(s2.colors.Skip(p)).ToArray();
      
      o1 = new GeneticSolution(s1.graph, c);

      c = s2.colors.Take(p).Concat(s1.colors.Skip(p)).ToArray();

      o2 = new GeneticSolution(s1.graph, c);

      return new Tuple<GeneticSolution, GeneticSolution>(o1, o2);
    }

    public static Tuple<GeneticSolution, GeneticSolution> TwoPointCrossover(GeneticSolution s1, GeneticSolution s2)
    {
      int[] c;
      int q = Randomizer.Next(s1.graph.NodeCount-1);
      int r = q;
      int tmp;
      GeneticSolution o1, o2;

      while(r == q)
      {
        r = Randomizer.Next(s1.graph.NodeCount-1);
      }

      if(r < q)
      {
        tmp = r;
        r = q;
        q = tmp;
      }

      c = s1.colors.Take(q).Concat(s2.colors.Skip(q).Take(r-q)).Concat(s1.colors.Skip(r)).ToArray();

      o1 = new GeneticSolution(s1.graph, c);

      c = s2.colors.Take(q).Concat(s1.colors.Skip(q).Take(r-q)).Concat(s2.colors.Skip(r)).ToArray();

      o2 = new GeneticSolution(s1.graph, c);
      
      return new Tuple<GeneticSolution, GeneticSolution>(o1, o2);
    }

    public void MutateRandomly(double p)
    {
      double rd;
      int ri;
      int i;
      
      for(i=0; i < this.colors.Length; i++)
      {
        rd = Randomizer.NextDouble();
        if(rd < p)
        {
          ri = Randomizer.Next(this.colors.Length) + 1;
          this.colors[i] = ri;
        }
      }
    }
  }
}

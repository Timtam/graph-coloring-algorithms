using System;
using System.Collections.Generic;
using System.Linq;

using graph_coloring;
using graph_coloring.solutions;

namespace graph_coloring.algorithms
{
  public class GeneticAlgorithm : Algorithm
  {

    private const int Amount_Start_Solutions = 100;

    public GeneticAlgorithm(Graph g) : base(g, "genetic")
    {
      this.SetTimeout(120000);
    }

    public override Solution Run()
    {
      List<GeneticSolution> solutions = new List<GeneticSolution>(Amount_Start_Solutions);
      List<GeneticSolution> siblings = new List<GeneticSolution>((Amount_Start_Solutions/2)*(Amount_Start_Solutions/2));
      int i,j,r;
      int[] c;
      double limit_w = 2.0*this.graph.EdgeCount*this.graph.NodeCount;
      double w, tmp_w;
      List<GeneticSolution> neighbors;
      Tuple<GeneticSolution, GeneticSolution> t;
      GeneticSolution s;

      for(i=0; i < Amount_Start_Solutions; i++)
      {
        c = new int[this.graph.NodeCount];
        for(j=0; j < this.graph.NodeCount; j++)
        {
          r = Randomizer.Next(this.graph.NodeCount) + 1;
          c[j] = r;
        }
        solutions.Add(new GeneticSolution(this.graph, c));
      }

      solutions = solutions.OrderBy(o => o.GetWorth() / limit_w).ToList();

      s = solutions[0];
      w = s.GetWorth();

      this.RunBefore();

      while(this.measurement.ElapsedMilliseconds < this.timeout)
      {
        neighbors = new List<GeneticSolution>(Amount_Start_Solutions);
        siblings.Clear();
        for(i=0; i < Amount_Start_Solutions; i++)
        {
          var enumerator = solutions[i].GetNextNeighbor().GetEnumerator();
          enumerator.MoveNext();
          neighbors.Add(((Solution)enumerator.Current).Copy<GeneticSolution>());
        }

        // only the best survive
        neighbors = neighbors.OrderBy(o => o.GetWorth() / limit_w).Take(Amount_Start_Solutions/2).ToList();

        // and those will generate siblings

        for(i=0; i < Amount_Start_Solutions / 2; i++)
        {
          for(j=i + 1; j < Amount_Start_Solutions / 2; j++)
          {
            t = GeneticSolution.OnePointCrossover(neighbors[i], neighbors[j]);
            siblings.Add(t.Item1);
            siblings.Add(t.Item2);
          }
        }

        // only Amount_Start_Solutions siblings may survive to prevent a neverending population
        solutions = siblings.OrderBy(o => o.GetWorth() / limit_w).Take(Amount_Start_Solutions).ToList();

        // loop over all solutions and mutate random genes
        for(i=0; i < Amount_Start_Solutions; i++)
          solutions[i].MutateRandomly();

        // we order the solutions to find the best
        solutions = solutions.OrderBy(o => o.GetWorth() / limit_w).ToList();
        tmp_w = solutions[0].GetWorth();
        if(tmp_w < w)
        {
          s = solutions[0];
          w = tmp_w;
        }
      }

      this.RunAfter();

      return s;
    }

    public override void SetTimeout(int t)
    {
      this.timeout = t;
    }
  }
}

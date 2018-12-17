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
    }

    public override Solution Run()
    {
      List<GeneticSolution> start_solutions = new List<GeneticSolution>(Amount_Start_Solutions);
      int i,j,r;
      int[] c;
      double limit_w = 2.0*this.graph.EdgeCount*this.graph.NodeCount;
      List<GeneticSolution> neighbors = new List<GeneticSolution>(Amount_Start_Solutions);

      for(i=0; i < Amount_Start_Solutions; i++)
      {
        c = new int[this.graph.NodeCount];
        for(j=0; j < this.graph.NodeCount; j++)
        {
          r = Randomizer.Next(this.graph.NodeCount) + 1;
          c[j] = r;
        }
        start_solutions.Add(new GeneticSolution(this.graph, c));
      }

      while(true)
      {
        neighbors.Clear();
        for(i=0; i < Amount_Start_Solutions; i++)
        {
          var enumerator = start_solutions[i].GetNextNeighbor().GetEnumerator();
          enumerator.MoveNext();
          neighbors.Add((GeneticSolution)enumerator.Current);
        }
      }

      return null;
    }
  }
}

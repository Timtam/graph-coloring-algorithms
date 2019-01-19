using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using graph_coloring;
using graph_coloring.solutions;

namespace graph_coloring.algorithms
{
  public class GeneticAlgorithm : Algorithm
  {

    private int amount_start_solutions;
    private int crossover_strategy = 1; // 1 = onepoint, 2 = twopoint
    private double mutation_probability;

    public GeneticAlgorithm(Graph g) : base(g)
    {
      this.SetTimeout(120000);
      this.SetAmountStartSolutions(100);
      this.SetMutationProbability(0.05);
    }

    public override Solution Run()
    {
      List<GeneticSolution> solutions = new List<GeneticSolution>(this.GetAmountStartSolutions());
      List<GeneticSolution> siblings = new List<GeneticSolution>((this.GetAmountStartSolutions()/2)*(this.GetAmountStartSolutions()/2));
      int i,j,r;
      int[] c;
      double limit_w = 2.0*this.graph.EdgeCount*this.graph.NodeCount;
      double w, tmp_w;
      List<GeneticSolution> neighbors;
      Tuple<GeneticSolution, GeneticSolution> t;
      GeneticSolution s;

      for(i=0; i < this.GetAmountStartSolutions(); i++)
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

      Console.WriteLine("amount of start solutions set to " + this.GetAmountStartSolutions());
      Console.WriteLine("probability of random mutations set to " + this.GetMutationProbability());

      this.RunBefore();

      while(this.measurement.ElapsedMilliseconds < this.timeout)
      {
        neighbors = new List<GeneticSolution>(this.GetAmountStartSolutions());
        siblings.Clear();
        for(i=0; i < this.GetAmountStartSolutions(); i++)
        {
          var enumerator = solutions[i].GetNextNeighbor().GetEnumerator();
          enumerator.MoveNext();
          neighbors.Add(((Solution)enumerator.Current).Copy<GeneticSolution>());
        }

        // only the best survive
        neighbors = neighbors.OrderBy(o => o.GetWorth() / limit_w).Take(this.GetAmountStartSolutions()/2).ToList();

        // and those will generate siblings

        for(i=0; i < this.GetAmountStartSolutions() / 2; i++)
        {
          for(j=i + 1; j < this.GetAmountStartSolutions() / 2; j++)
          {
            if(this.GetCrossoverStrategy() == 1)
              t = GeneticSolution.OnePointCrossover(neighbors[i], neighbors[j]);
            else
              t = GeneticSolution.TwoPointCrossover(neighbors[i], neighbors[j]);
            siblings.Add(t.Item1);
            siblings.Add(t.Item2);
          }
        }

        // only amount of start solutionssiblings may survive to prevent a neverending population
        solutions = siblings.OrderBy(o => o.GetWorth() / limit_w).Take(this.GetAmountStartSolutions()).ToList();

        // loop over all solutions and mutate random genes
        for(i=0; i < this.GetAmountStartSolutions(); i++)
          solutions[i].MutateRandomly(this.GetMutationProbability());

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

    public override string GetName()
    {
      switch(this.GetCrossoverStrategy())
      {
        case 1:
          return "genetic-onepoint";
        case 2:
          return "genetic-twopoint";
        default:
          return "genetic-unknown";
      }
    }

    public void SetCrossoverStrategy(int s)
    {
      if(s != 1 && s != 2)
        throw new ArgumentOutOfRangeException("only 1 or 2 for onepoint or twopoint crossover allowed");

      this.crossover_strategy = s;
    }

    public int GetCrossoverStrategy()
    {
      return this.crossover_strategy;
    }

    public void SetAmountStartSolutions(int a)
    {
      if(a <= 0)
        throw new ArgumentException("there must be at least one start solution");
      this.amount_start_solutions = a;
    }

    public int GetAmountStartSolutions()
    {
      return this.amount_start_solutions;
    }
    
    public override void SetParameters(string[] param)
    {
      if(param.Length == 0)
        return;
      if(param.Length >= 1)
      {
        try
        {
          this.SetAmountStartSolutions(int.Parse(param[0]));
        }
        catch(FormatException)
        {
          throw new ArgumentException("amount of start solutions must be numeric");
        }
      }

      if(param.Length >= 2)
      {
        try
        {
          this.SetMutationProbability(Double.Parse(param[1], CultureInfo.InvariantCulture));
        }
        catch(FormatException)
        {
          throw new ArgumentException("mutation probability must be a floating-point number");
        }
      }
    }

    public void SetMutationProbability(double p)
    {
      if(p < 0 || p > 1.0)
        throw new ArgumentException("mutation probability may not be less than 0 or greater than 1.0");
      this.mutation_probability = p;
    }

    public double GetMutationProbability()
    {
      return this.mutation_probability;
    }
  }
}

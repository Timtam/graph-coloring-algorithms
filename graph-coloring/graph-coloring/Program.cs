// program entry point
// program accepts the following parameters:
//         file: either a file name to a DIMACS graph file
//               or a number, in which case a random graph with that amount of
//               nodes will be generated
// local search will be applied to the given graph afterwards
// and the result, as well as the running time will be printed

using graph_coloring.algorithms;
using graph_coloring.solutions;

using System;
using System.IO;
using System.Linq;

namespace graph_coloring
{
  class Program
  {
    static int Main(string[] args)
    {
      Algorithm s;
      Solution ss;
      Graph graph;
      int timeout = -1;;
      string algorithm;
      string file;
      if(args.Length == 0)
      {
        Console.WriteLine("graph-coloring FILE ALGORITHM [TIMEOUT [OPTIONS]]");
        Console.WriteLine("Parameters:");
        Console.WriteLine("\tFILE - filename of a DIMACS graph file to process");
        Console.WriteLine("\t       specify a number for a random graph");
        Console.WriteLine("\t       with that amount of nodes");
        Console.WriteLine("\tALGORITHM - algorithm to use during the search");
        Console.WriteLine("\tTIMEOUT - milliseconds after which the program will finish");
        Console.WriteLine("\t          processing some algorithms");
        Console.WriteLine("\tOPTIONS - algorithm-specific options, like start ");
        Console.WriteLine("\t          temperature for simulated annealing (see below)");
        Console.WriteLine("");
        Console.WriteLine("Currently supported algorithms:");
        Console.WriteLine("\tgenetic-onepoint");
        Console.WriteLine("\tgenetic-twopoint");
        Console.WriteLine("\tlocal-search");
        Console.WriteLine("\tsimulated-annealing");
        Console.WriteLine("\t\tOptions:");
        Console.WriteLine("\t\t\tstart temperature to use (default 30)");
        Console.WriteLine("\ttaboo-search");
        return 0;
      }
      else if(args.Length == 1)
      {
        Console.WriteLine("You must at least specify an algorithm to use");
        return 1;
      }

      if(args.Length >= 3)
      {
        try
        {
          timeout = int.Parse(args[2]);
          if(timeout <= 0)
          {
            Console.WriteLine("You cannot disable timeouts.");
            return 1;
          }
        }
        catch (FormatException)
        {
          Console.WriteLine("timeout must be set with a numeric value.");
          return 1;
        }
      }
      file = args[0];
      try
      {
        graph = RandomGraphGenerator.Generate(int.Parse(file));
      }
      catch(FormatException)
      {
        if(!File.Exists(file))
        {
          Console.WriteLine("This file doesn't exist.");
          return 1;
        }
        graph = DIMACSParser.Read(file);
      }
      Console.WriteLine("Graph with " + graph.NodeCount + " nodes and " + graph.EdgeCount + " edges loaded successfully.");

      algorithm = args[1];

      switch(algorithm)
      {
        case "local-search":
          s = new LocalSearchAlgorithm(graph);
          break;
        case "simulated-annealing":
          s = new SimulatedAnnealingAlgorithm(graph);
          break;
        case "taboo-search":
          s = new TabooSearchAlgorithm(graph);
          break;
        case "genetic-onepoint":
          s = new GeneticAlgorithm(graph);
          ((GeneticAlgorithm)s).SetCrossoverStrategy(1);
          break;
        case "genetic-twopoint":
          s = new GeneticAlgorithm(graph);
          ((GeneticAlgorithm)s).SetCrossoverStrategy(2);
          break;
        default:
          Console.WriteLine("no algorithm with name " + algorithm + " found");
          return 1;
      }

      Console.WriteLine("Running " + s.GetName() + " on graph");
      if(timeout >= 0)
        s.SetTimeout(timeout);

      timeout = s.GetTimeout();
      if(timeout == -1)
        Console.WriteLine("No timeout applied.");
      else
        Console.WriteLine("Timeout set to " + timeout + " milliseconds.");

      try
      {
        s.SetParameters(args.Skip(3).ToArray());
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine("Error: " + ex.Message);
        return 1;
      }

      ss = s.Run();
      Console.WriteLine("Coloring through " + s.GetName() + " finished successfully.");
      Console.WriteLine("Finished with " + ss.ColorCount + " colors needed");
      Console.WriteLine("Algorithm took " + s.Duration + " to run.");
      if(ss.IsValid() == true)
        Console.WriteLine("Algorithm result is a feasable solution.");
      else
      {
        Console.WriteLine("The algorithm result isn't a feasable solution and therefore");
        Console.WriteLine("still requires tweaking.");
      }
      return 0;
    }
  }
}

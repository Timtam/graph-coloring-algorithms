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

namespace graph_coloring
{
  class Program
  {
    static void Main(string[] args)
    {
      Algorithm s;
      Solution ss;
      Graph graph;
      int timeout = -1;;
      string algorithm;
      string file;
      if(args.Length == 0)
      {
        Console.WriteLine("graph-coloring FILE ALGORITHM [TIMEOUT]");
        Console.WriteLine("Parameters:");
        Console.WriteLine("\tFILE - filename of a DIMACS graph file to process");
        Console.WriteLine("\t       specify a number for a random graph");
        Console.WriteLine("\t       with that amount of nodes");
        Console.WriteLine("\tALGORITHM - algorithm to use during the search");
        Console.WriteLine("\tTIMEOUT - milliseconds after which the program will finish");
        Console.WriteLine("\t          processing some algorithms");
        Console.WriteLine("");
        Console.WriteLine("Currently supported algorithms:");
        Console.WriteLine("\tlocal-search");
        Console.WriteLine("\tsimulated-annealing");
        Console.WriteLine("\ttaboo-search");
        return;
      }
      else if(args.Length == 1)
      {
        Console.WriteLine("You must at least specify an algorithm to use");
        return;
      }

      if(args.Length == 3)
      {
        try
        {
          timeout = int.Parse(args[2]);
          if(timeout <= 0)
          {
            Console.WriteLine("You cannot disable timeouts.");
            return;
          }
        }
        catch (FormatException)
        {
          Console.WriteLine("timeout must be set with a numeric value.");
          return;
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
          return;
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
        default:
          Console.WriteLine("no algorithm with name " + algorithm + " found");
          return;
      }

      Console.WriteLine("Running " + s.Name + " on graph");
      if(timeout >= 0)
        s.SetTimeout(timeout);

      timeout = s.GetTimeout();
      if(timeout == -1)
        Console.WriteLine("No timeout applied.");
      else
        Console.WriteLine("Timeout set to " + timeout + " milliseconds.");
      ss = s.Run();
      Console.WriteLine("Coloring through " + s.Name + " finished successfully.");
      Console.WriteLine("Finished with " + ss.ColorCount + " colors needed");
      Console.WriteLine("Algorithm took " + s.Duration + " to run.");
      if(ss.IsValid() == true)
        Console.WriteLine("Algorithm result is a feasable solution.");
      else
      {
        Console.WriteLine("The algorithm result isn't a feasable solution and therefore");
        Console.WriteLine("still requires tweaking.");
      }
    }
  }
}

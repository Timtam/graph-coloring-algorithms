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
      Graph graph;
      string file;
      if(args.Length != 1)
      {
        Console.WriteLine("The only parameter to this program may be the file");
        Console.WriteLine("defining the graph which needs to be processed.");
        return;
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

      Console.WriteLine("Running local search on graph");
      LocalSearchAlgorithm s = new LocalSearchAlgorithm(graph);
      Solution ss = s.Run();
      Console.WriteLine("Coloring through local search finished successfully.");
      Console.WriteLine("Finished with " + ss.ColorCount + " colors needed");
      Console.WriteLine("Algorithm took " + s.Duration + " to run.");
      if(ss.IsValid() == true)
        Console.WriteLine("Algorithm result is a feasable solution as well.");
      else
      {
        Console.WriteLine("The algorithm result isn't a feasable solution and therefore");
        Console.WriteLine("still requires tweaking.");
      }
    }
  }
}

﻿using System;
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
      if(!File.Exists(file))
      {
        Console.WriteLine("This file doesn't exist.");
        return;
      }
      graph = DIMACSParser.Read(file);
    }
  }
}

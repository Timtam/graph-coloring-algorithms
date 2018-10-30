// dimacsparser class
// understands dimacs graph files and parses them
// takes care of basic dimacs file errors
// only edge problem files are supported right now
// Read() method returns a fully loaded Graph object
// may also raise exceptions if an error occurred

using System.IO;

namespace graph_coloring
{
  public static class DIMACSParser
  {
    public static Graph Read(string file)
    {
      Graph g = null;
      StreamReader sr;
      string line;
      string[] parts;
      
      sr = File.OpenText(file);
      while((line = sr.ReadLine()) != null)
      {
        if(string.IsNullOrWhiteSpace(line))
          continue;
        // comments
        else if(line.StartsWith("c"))
          continue;
        else if(line.StartsWith("p"))
        {
          parts = line.Split(' ');
          if(parts.Length != 4)
            throw new System.ArgumentException("problem line doesn't have the required format");
          if(parts[1] != "edge")
            throw new System.ArgumentOutOfRangeException("only edge supported as problem format yet");
          g = new Graph(int.Parse(parts[2]));
        }
        else if(line.StartsWith("e"))
        {
          if(g == null)
            throw new System.InvalidOperationException("cannot add edges to not properly initialized graphs");
          parts = line.Split(' ');
          if(parts.Length != 3)
            throw new System.ArgumentException("wrong number of arguments to edge specification");
          g.AddEdge(int.Parse(parts[1]) - 1, int.Parse(parts[2]) - 1);
        }
      }
      sr.Close();
      return g;
    }
  }
}

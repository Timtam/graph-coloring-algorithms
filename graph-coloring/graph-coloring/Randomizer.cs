using System;

namespace graph_coloring
{
  static class Randomizer
  {
    private static Random random;
    
    static Randomizer()
    {
      random = new Random();
    }

    public static int Next(int max)
    {
      return random.Next(max);
    }

    public static int Next(int min, int max)
    {
      return random.Next(min, max);
    }
  }
}

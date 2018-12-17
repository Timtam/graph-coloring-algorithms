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

    public override IEnumerable<object> GetNextNeighbor()
    {
      yield return null;
    }
  }
}

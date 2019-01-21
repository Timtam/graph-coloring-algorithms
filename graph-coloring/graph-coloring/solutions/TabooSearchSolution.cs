using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using graph_coloring;

namespace graph_coloring.solutions
{
  public class TabooSearchSolution : LocalSearchSolution
  {

    private Feature[] features;
    public Feature Feature;

    public TabooSearchSolution(Graph g, int[] c) : base(g, c)
    {
    }

    public override IEnumerable<object> GetNextNeighbor()
    {
      int i;
      int[] c;
      Feature min_f;
      TabooSearchSolution s;

      min_f = this.features[0];

      for(i=1; i < this.features.Length; i++)
        if(this.features[i].Cost < min_f.Cost)
          min_f = features[i];

      c = new int[this.colors.Length];
      Array.Copy(this.colors, c, c.Length);

      c[min_f.Node.ID] = min_f.Color;
      s = new TabooSearchSolution(this.graph, c);
      s.Feature = min_f;
      yield return s;
    } 

    public void SetFeatures(Feature[] features)
    {
      int[] cl = this.colors.Distinct().ToArray();
      int uc = this.GetUnusedColor();

      this.features = features.Where(f => this.colors[f.Node.ID] != f.Color && (cl.Contains(f.Color) || uc == f.Color)).ToArray();

      Parallel.For(0, this.features.Length, (i) =>
      {
        int[] c = new int[this.colors.Length];
        TabooSearchSolution s;

        Array.Copy(this.colors, c, c.Length);
        // changing the color within the copied color array
        c[this.features[i].Node.ID] = this.features[i].Color;
        s = new TabooSearchSolution(this.graph, c);
        this.features[i].Cost = s.GetWorth();
      });
    }
  }
}

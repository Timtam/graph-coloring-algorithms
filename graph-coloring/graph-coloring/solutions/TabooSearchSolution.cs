using System;
using System.Collections.Generic;
using System.Linq;

using graph_coloring;
using graph_coloring.features;

namespace graph_coloring.solutions
{
  public class TabooSearchSolution : LocalSearchSolution
  {

    private List<Feature> features;
    public Feature Feature;

    public TabooSearchSolution(Graph g, int[] c) : base(g, c)
    {
      this.Feature = null;
    }

    public override IEnumerable<object> GetNextNeighbor()
    {
      int i;
      int[] c;
      Feature min_f;
      TabooSearchSolution s;

      min_f = this.features[0];

      for(i=1; i < this.features.Count; i++)
        if(this.features[i].Cost < min_f.Cost)
          min_f = features[i];

      c = new int[this.colors.Length];
      Array.Copy(this.colors, c, c.Length);

      c[min_f.Node.ID] = min_f.Color;
      s = new TabooSearchSolution(this.graph, c);
      s.Feature = min_f;
      yield return s;
    } 

    public void SetFeatures(List<Feature> features)
    {
      // copying the current state
      int i;
      int[] c = new int[this.colors.Length];
      TabooSearchSolution s;

      Array.Copy(this.colors, c, c.Length);

      this.features = features.Where(f => this.colors[f.Node.ID] != f.Color).ToList();

      for(i=0; i < features.Count; i++)
      {
        // changing the color within the copied color array
        c[features[i].Node.ID] = features[i].Color;
        s = new TabooSearchSolution(this.graph, c);
        features[i].Cost = s.GetWorth();
        // reverting changes
        c[features[i].Node.ID] = this.colors[features[i].Node.ID];
      }
    }
  }
}

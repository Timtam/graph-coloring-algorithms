using System;
using System.Collections.Generic;

using graph_coloring;
using graph_coloring.features;

namespace graph_coloring.solutions
{
  public class TabooSearchSolution : LocalSearchSolution
  {
    public TabooSearchSolution(Graph g, List<int> c, List<List<Node>> cc = null) : base(g, c, cc)
    {
    }

    public void CalculateFeatureCosts(List<Feature> features)
    {
      // copying the current state
      int i;
      List<int> c = new List<int>(this.colors);
      List<List<Node>> ccl = new List<List<Node>>(this.color_classes);
      TabooSearchSolution s;

      for(i=0; i < features.Count; i++)
      {
        // changing the color within the copied color array
        c[features[i].Node.ID] = features[i].Color;
        // change color classes
        ccl[this.colors[features[i].Node.ID] - 1].Remove(features[i].Node);
        ccl[features[i].Color - 1].Add(features[i].Node);
        s = new TabooSearchSolution(this.graph, c, ccl);
        features[i].Cost = s.GetWorth();
        // reverting changes
        ccl[features[i].Color - 1].Remove(features[i].Node);
        ccl[this.colors[features[i].Node.ID] - 1].Add(features[i].Node);
        c[features[i].Node.ID] = this.colors[features[i].Node.ID];
      }
    }
  }
}

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

    public TabooSearchSolution(Graph g, List<int> c, List<List<Node>> cc = null) : base(g, c, cc)
    {
      this.Feature = null;
    }

    public override IEnumerable<object> GetNextNeighbor()
    {
      int i;
      List<int> c;
      List<List<Node>> ccl;
      Feature min_f;
      TabooSearchSolution s;

      min_f = this.features[0];

      for(i=1; i < this.features.Count; i++)
        if(this.features[i].Cost < min_f.Cost)
          min_f = features[i];

      c = new List<int>(this.colors);
      ccl = new List<List<Node>>(Math.Max(this.color_classes.Count, min_f.Color));

      for(i=0; i < Math.Max(this.color_classes.Count, min_f.Color); i++)
      {
        if(i < this.color_classes.Count)
          ccl.Add(new List<Node>(this.color_classes[i]));
        else
          ccl.Add(new List<Node>(this.graph.NodeCount));
      }

      c[min_f.Node.ID] = min_f.Color;
      ccl[this.colors[min_f.Node.ID] - 1].Remove(min_f.Node);
      ccl[min_f.Color - 1].Add(min_f.Node);
      s = new TabooSearchSolution(this.graph, c, ccl);
      s.Feature = min_f;
      yield return s;
    } 

    public void SetFeatures(List<Feature> features)
    {
      // copying the current state
      int i;
      List<int> c = new List<int>(this.colors);
      List<List<Node>> ccl = new List<List<Node>>(this.graph.NodeCount);
      TabooSearchSolution s;

      this.features = features.Where(f => this.colors[f.Node.ID] != f.Color).ToList();

      for(i=0; i < this.graph.NodeCount; i++)
      {
        if(i < this.color_classes.Count)
          ccl.Add(new List<Node>(this.color_classes[i]));
        else
          ccl.Add(new List<Node>(this.graph.NodeCount));
      }

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

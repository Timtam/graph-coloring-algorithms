using System;
using System.Collections.Generic;

using graph_coloring;
using graph_coloring.features;
using graph_coloring.solutions;

namespace graph_coloring.algorithms
{
  public class TabooSearchAlgorithm : LocalSearchAlgorithm
  {
    public TabooSearchAlgorithm(Graph g) : base(g, "taboo search")
    {
    }

    public override Solution Run()
    {
      int i,j;
      List<Feature> features = new List<Feature>(this.graph.NodeCount * this.graph.NodeCount);
      Node n;
      TabooSearchSolution s;
      TabooSearchSolution global_s;
      double w;
      double global_w;
      // create list of features first

      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        for(j=0; j < this.graph.NodeCount; j++)
        {
          features.Add(new Feature(n, j + 1));
        }
      }

      // get the initial solution
      s = this.GetGreedySolution<TabooSearchSolution>();
      w = s.GetWorth();
      global_s = s;
      global_w = w;

      // calculating feature cost for first solution
      s.SetFeatures(features);

      return s;
    }
  }
}

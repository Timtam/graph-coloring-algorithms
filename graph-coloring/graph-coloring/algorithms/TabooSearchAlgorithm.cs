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
      this.SetTimeout(120000);
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

      this.RunBefore();

      // calculating feature cost for first solution
      s.SetFeatures(features);

      while(this.measurement.ElapsedMilliseconds < this.timeout)
      {
        var enumerator = s.GetNextNeighbor().GetEnumerator();
        enumerator.MoveNext();
        s = (TabooSearchSolution)enumerator.Current;
        s.SetFeatures(features);
        if(s.IsValid() && (w = s.GetWorth()) < global_w)
        {
          global_s = s;
          global_w = w;
        }
      }

      this.RunAfter();

      return global_s;
    }

    public override void SetTimeout(int t)
    {
      this.timeout = t;
    }
  }
}

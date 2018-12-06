using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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
      ConcurrentQueue<Feature> taboo;
      Feature drop;
      int i,j;
      int useless_steps = 0;
      int stored_features;
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

      // calculate amount of stored features
      stored_features = this.graph.NodeCount;

      taboo = new ConcurrentQueue<Feature>();

      // get the initial solution
      s = this.GetSingleColoredSolution<TabooSearchSolution>();
      w = s.GetWorth();
      global_s = s;
      global_w = w;

      this.RunBefore();

      // calculating feature cost for first solution
      s.SetFeatures(features);

      while(this.measurement.ElapsedMilliseconds < this.timeout && useless_steps <= 100)
      {
        var enumerator = s.GetNextNeighbor().GetEnumerator();
        enumerator.MoveNext();
        useless_steps++;
        s = (TabooSearchSolution)enumerator.Current;
        taboo.Enqueue(s.Feature);
        if(taboo.Count > stored_features)
          taboo.TryDequeue(out drop);
        s.SetFeatures(features.Where(f => !taboo.Contains(f)).ToList());
        if(s.IsValid() && (w = s.GetWorth()) < global_w)
        {
          useless_steps = 0;
          global_s = s;
          global_w = w;
          while(taboo.Count < 0)
            taboo.TryDequeue(out drop);
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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using graph_coloring;
using graph_coloring.solutions;

namespace graph_coloring.algorithms
{
  public class TabooSearchAlgorithm : Algorithm
  {
    private int taboo_list_length;

    public TabooSearchAlgorithm(Graph g) : base(g, "taboo search")
    {
      this.SetTimeout(120000);
      this.SetTabooListLength(g.NodeCount);
    }

    public override Solution Run()
    {
      ConcurrentQueue<Feature> taboo;
      Feature drop;
      int i,j,k;
      int useless_steps = 0;
      int stored_features;
      Feature[] features = new Feature[(this.graph.NodeCount * this.graph.NodeCount)];
      Node n;
      TabooSearchSolution s;
      TabooSearchSolution global_s;
      double w;
      double global_w;
      // create list of features first

      k = 0;
      for(i=0; i < this.graph.NodeCount; i++)
      {
        n = this.graph.GetNode(i);
        for(j=0; j < this.graph.NodeCount; j++)
        {
          features[k].Node = n;
          features[k].Color = j + 1;
          features[k].Cost = 0.0;
          k++;
        }
      }

      // calculate amount of stored features
      stored_features = this.taboo_list_length;

      taboo = new ConcurrentQueue<Feature>();

      // get the initial solution
      s = this.GetSingleColoredSolution<TabooSearchSolution>();
      w = s.GetWorth();
      global_s = s;
      global_w = w;

      Console.WriteLine("taboo list length set to " + stored_features);

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
        s.SetFeatures(features.Where(f => !taboo.Contains(f)).ToArray());
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

    public void SetTabooListLength(int t)
    {
      if(t <= 0)
        throw new ArgumentException("taboo list cannot be equal or less than 0");
      this.taboo_list_length = t;
    }

    public int GetTabooListLength()
    {
      return this.taboo_list_length;
    }

    public override void SetParameters(string[] param)
    {
      if(param.Length == 0)
        return;
      
      if(param.Length >= 1)
      {
        try
        {
          this.SetTabooListLength(int.Parse(param[0]));
        }
        catch(FormatException)
        {
          throw new ArgumentException("taboo list length must be numeric");
        }
      }
    }
  }
}

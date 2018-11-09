// a base class for all future algorithms
// provides general support for some tasks all algorithms need to perform the same way
// thats storing the node colors and stopping the runtime for now
// it also provides access to the required amount of colors (result)
// all coloring algorithms need to derive this class

using graph_coloring;
using graph_coloring.solutions;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace graph_coloring.algorithms
{
  public class Algorithm
  {
    protected Graph graph;
    protected List<int> colors;
    protected Stopwatch measurement;

    public Algorithm(Graph g)
    {
      int i;

      this.graph = g;
      this.colors = new List<int>(graph.NodeCount);
      this.measurement = new Stopwatch();

      for(i=0; i<this.graph.NodeCount; i++)
        this.colors.Add(0);
    }

    public void Bleach()
    {
      int i;
      for(i=0; i < this.colors.Count; i++)
        this.colors[i] = 0;
    }

    protected void RunBefore()
    {
      this.Bleach();
      this.measurement.Reset();
      this.measurement.Start();
    }

    protected void RunAfter()
    {
      this.measurement.Stop();
    }

    public virtual Solution run()
    {
      return new Solution(null, null);
    }

    public TimeSpan Duration
    {
      get
      {
        return this.measurement.Elapsed;
      }
    }
  }
}
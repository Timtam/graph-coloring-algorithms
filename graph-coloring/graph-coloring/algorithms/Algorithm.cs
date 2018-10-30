using graph_coloring;

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

    public int ColorCount
    {
      get
      {
        List<int> colors = new List<int>(this.colors.Count);
        int i,sum;
        
        for(i=0; i < this.colors.Count; i++)
          colors.Add(0);
        
        for(i=0; i<this.colors.Count; i++)
          if(this.colors[i] > 0)
            colors[this.colors[i] - 1]++;
        
        sum = 0;
        for(i=0; i<colors.Count; i++)
          if(colors[i] > 0)
            sum++;

        return sum;
      }
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

    public TimeSpan Duration
    {
      get
      {
        return this.measurement.Elapsed;
      }
    }
  }
}
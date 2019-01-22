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
    protected Stopwatch measurement;
    private string name;
    protected int timeout;

    public Algorithm(Graph g, string name = "unknown")
    {
      this.graph = g;
      this.measurement = new Stopwatch();
      this.name = name;
      this.timeout = -1;
    }

    protected void RunBefore()
    {
      this.measurement.Reset();
      this.measurement.Start();
    }

    protected void RunAfter()
    {
      this.measurement.Stop();
    }

    public virtual Solution Run()
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

    public int GetTimeout()
    {
      return this.timeout;
    }

    public virtual void SetTimeout(int t)
    {
      this.timeout = t;
    }

    public virtual string GetName()
    {
      return this.name;
    }

    public virtual void SetParameters(string[] param)
    {
    }

    protected T GetGreedySolution<T>() where T : IColorable
    {
      T s = (T)Activator.CreateInstance(typeof(T), this.graph, null);
      s.ApplyGreedyColoring();
      return s;
    }

    protected T GetSingleColoredSolution<T>() where T : IColorable
    {
      T s = (T)Activator.CreateInstance(typeof(T), this.graph, null);
      s.ApplySingleColoring();
      return s;
    }
  }
}
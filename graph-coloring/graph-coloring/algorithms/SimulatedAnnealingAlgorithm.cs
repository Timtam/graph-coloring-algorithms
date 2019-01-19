using System;
using System.Collections.Generic;

using graph_coloring;
using graph_coloring.solutions;

namespace graph_coloring.algorithms
{
  public class SimulatedAnnealingAlgorithm : LocalSearchAlgorithm
  {
    private int start_temperature;

    public SimulatedAnnealingAlgorithm(Graph g) : base(g, "simulated annealing")
    {
      this.SetStartTemperature(30);
      this.SetTimeout(120000);
    }

    public override Solution Run()
    {
      bool found = true;
      int k = 0;
      Solution s = this.GetGreedySolution<SimulatedAnnealingSolution>();
      Solution valid_s = s;
      double w = s.GetWorth();
      double tmp_w;
      double p;
      
      Console.WriteLine("start temperature set to " + this.GetStartTemperature());

      this.RunBefore();

      while(found && this.measurement.ElapsedMilliseconds < this.GetTimeout())
      {
        found = false;
        k++;

        foreach(Solution t in s.GetNextNeighbor())
        {
          tmp_w = t.GetWorth();
          if(tmp_w < w)
          {
            s = t;
            if(s.IsValid())
              valid_s = s;
            w = tmp_w;
            found = true;
            break;
          }
          else if(tmp_w > w)
          {
            p = Math.Exp((w - tmp_w) * Math.Log(k, 2) / this.start_temperature);

            if(Randomizer.NextDouble() < p)
            {
              s = t;
              if(s.IsValid())
                valid_s = s;
              w = tmp_w;
              found = true;
              break;
            }
          }
        }
      }

      this.RunAfter();
      return valid_s;
    }

    public override void SetTimeout(int t)
    {
      this.timeout = t;
    }

    public int GetStartTemperature()
    {
      return this.start_temperature;
    }

    public void SetStartTemperature(int t)
    {
      if(t <= 0)
        throw new ArgumentException("start temperature may not be equal or less than 0");
      this.start_temperature = t;
    }

    public override void SetParameters(string[] param)
    {

      if(param.Length == 1)
      {
        try
        {
          this.SetStartTemperature(int.Parse(param[0]));
        }
        catch(FormatException)
        {
          throw new System.ArgumentException("start temperature must be numeric");
        }
      }
      else if(param.Length > 1)
        throw new ArgumentException("too many arguments for simulated annealing");
    }
  }
}

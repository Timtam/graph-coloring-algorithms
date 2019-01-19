using System;
using System.Collections.Generic;
using System.Globalization;

using graph_coloring;
using graph_coloring.solutions;

namespace graph_coloring.algorithms
{
  public class SimulatedAnnealingAlgorithm : LocalSearchAlgorithm
  {
    private int start_temperature;
    private double annealing_factor;

    public SimulatedAnnealingAlgorithm(Graph g) : base(g, "simulated annealing")
    {
      this.SetStartTemperature(30);
      this.SetTimeout(120000);
      this.SetAnnealingFactor(1.0);
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
      Console.WriteLine("annealing factor set to " + this.GetAnnealingFactor());

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
            p = Math.Exp((w - tmp_w) * this.annealing_factor * Math.Log(k, 2) / this.start_temperature);

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

      if(param.Length >= 1)
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

      if(param.Length >= 2)
      {
        try
        {
          this.SetAnnealingFactor(Double.Parse(param[1], CultureInfo.InvariantCulture.NumberFormat));
        }
        catch(FormatException)
        {
          throw new ArgumentException("annealing factor must be a floating point number");
        }
      }
    }

    public void SetAnnealingFactor(double f)
    {
      if(f <= 0)
        throw new ArgumentException("annealing factor may not be equal or less than 0");
      this.annealing_factor = f;
    }

    public double GetAnnealingFactor()
    {
      return this.annealing_factor;
    }
  }
}

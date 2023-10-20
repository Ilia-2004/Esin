#nullable enable
namespace esin_first_task;

public class Edge
{
  public string To { get; set; }
  public int? Weight { get; set; }

  // constructor
  public Edge(string to, int? weight) => (To, Weight) = (to, weight);
}
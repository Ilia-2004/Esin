#nullable enable
namespace esin_first_task;

public class Edge
{
  public string? To { get; set; }
  public int? Weight { get; set; }

  // constructors
  public Edge(string? to, int? weight, bool directed) => (To, Weight) = (to, weight);
}
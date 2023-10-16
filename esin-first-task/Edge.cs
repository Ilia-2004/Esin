#nullable enable
namespace esin_first_task;

public class Edge
{
  public string? To { get; set; }
  public int? Weight { get; set; }

  // constructor
  public Edge(string? to, int? weight, bool directed) => (To, Weight, _) = (to, weight, directed);

  public Edge(string connectedVertexName, int weight)
  {
    throw new System.NotImplementedException();
  }
}
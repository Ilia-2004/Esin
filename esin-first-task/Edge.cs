namespace esin_first_task;

public class Edge
{
  public string? To { get; set; }
  public int? Weight { get; set; }
  public bool IsDirected { get; set; }

  // constructor
  public Edge(string? to, int? weight, bool directed) => (To, Weight, IsDirected) = (to, weight, directed);
}
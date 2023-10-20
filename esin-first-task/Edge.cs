namespace esin_first_task;

public class Edge
{
  public string To;
  public int? Weight;

  // constructor
  public Edge(string to, int? weight) => (To, Weight) = (to, weight);
}
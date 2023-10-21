namespace AdjacencyList;

// The class that contains the structure of the edges of the graph
public class Edge
{
  public string To;
  public int? Weight;

  // Constructor
  public Edge(string to, int? weight) => (To, Weight) = (to, weight);
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace esin_first_task;

public class Graph
{
  private Dictionary<string, List<Edge>> AdjacencyList { get; set; }
  
  // constructors
  public Graph() => AdjacencyList = new Dictionary<string, List<Edge>>();

  public Graph(Graph other) =>
    AdjacencyList = new Dictionary<string, List<Edge>>(other.AdjacencyList);

  public Graph(string path) => AddFromFile(path);

  // methods
  public void OutList()
  {
    foreach (var key in AdjacencyList)
    {
      foreach (var value in key.Value)
        Console.WriteLine($"{key.Key}: {value.To}, {value.Weight}");
    }
  }
  public bool AddVertex(string vertex)
  {
    if (!AdjacencyList.ContainsKey(vertex))
      AdjacencyList[vertex] = new List<Edge> { new Edge("", null) };
    else
    {
      Console.WriteLine(" this vertex already exists");
      return false; 
    }

    return true;
  }

  public bool AddEdge(string from, string to, int? weight, bool isDirected)
  {
    if (AdjacencyList.ContainsKey(from) && AdjacencyList.ContainsKey(to))
    {
      AdjacencyList[from].Add(new Edge(to, weight));
      if (!isDirected) 
        AdjacencyList[to].Add(new Edge(from, weight));
    }
    else
    {
      Console.WriteLine(" these vertices don't exist");
      return false;
    }

    return true;
  }

  public bool RemoveVertex(string vertex)
  {
    if (AdjacencyList.ContainsKey(vertex))
    {
      AdjacencyList.Remove(vertex);
      foreach (var value in from key in AdjacencyList from value in key.Value where value.To == vertex select value)
      {
        value.To = string.Empty; 
        value.Weight = null;
      }
    }
    else
    {
      Console.WriteLine(" these vertices don't exist");
      return false; 
    }

    return true;
  }

  public bool RemoveEdge(string from, string to, bool isDirected)
  {
    if (isDirected)
    {
      var edge = AdjacencyList[from].FirstOrDefault(e => e.To == to);
      if (edge != null) AdjacencyList[from].Remove(edge);
      else Console.WriteLine(" this edge don't exist");
    }
    else
    {
      var edgeOfFrom = AdjacencyList[from].FirstOrDefault(e => e.To == to);
      var edgeOfTo = AdjacencyList[to].FirstOrDefault(e => e.To == from);
      if (edgeOfFrom != null && edgeOfTo != null)
      {
        AdjacencyList[from].Remove(edgeOfFrom);
        AdjacencyList[to].Remove(edgeOfTo);
      }
      else
      {
        Console.WriteLine(" this edge don't exist");
        return false; 
      }
    }

    return true; 
  }

  public bool AddFromFile(string pathFile)
  {
    AdjacencyList = new Dictionary<string, List<Edge>>();
    var lines = File.ReadAllLines(pathFile);

    Console.WriteLine(" write numbert method: ");
    Console.Write(" > "); var command = Console.ReadLine();

    if (command == "1")
        {
            var vertexes = lines[1].Trim().Split(' ');
            var listAdjacency = lines.Skip(2).ToArray();

            foreach (var vertex in vertexes)
              AdjacencyList[vertex] = new List<Edge> { new Edge("", null) };
    
            var rowCount = listAdjacency.Length;
            var columnCount = listAdjacency[0].Split(' ').Length;
            var listAdjacencyArr = new string[rowCount, columnCount];

            for (var i = 0; i < rowCount; i++)
            {
              var values = listAdjacency[i].Split(' ');
              for (var j = 0; j < columnCount; j++)
                listAdjacencyArr[i, j] = values[j];
            }
    
            for (var i = 0; i < listAdjacencyArr.GetLength(0); i++)
            {
              for (var j = 1; j < listAdjacencyArr.GetLength(1); j++)
              { 
                if (listAdjacencyArr[i, j] != "0")
                  AdjacencyList[(i + 1).ToString()].Add(new Edge(j.ToString(), int.Parse(listAdjacencyArr[i, j].ToString())));
              }
            }
        }

    else if (command == "2")
        {
            Dictionary<string, List<Edge>> adjacencyList = new Dictionary<string, List<Edge>>();

            foreach (string line in lines)
            {
                string[] components = line.Split(' ');

                string vertex = components[0];

                List<Edge> edges = new List<Edge>();

                for (int i = 1; i < components.Length; i += 2)
                {
                    string toVertex = components[i];
                    int? weight = null;

                    if (i + 1 < components.Length)
                    {
                        int parsedWeight;
                        if (int.TryParse(components[i + 1], out parsedWeight))
                            weight = parsedWeight;
                    }

                    Edge edge = new Edge(toVertex, weight);
                    edges.Add(edge);
                }

                adjacencyList[vertex] = edges;
            }

            AdjacencyList = adjacencyList;
        }
    foreach (var vertex in AdjacencyList)
    {
      foreach (var value in vertex.Value)
        Console.WriteLine($"{vertex.Key}: {value.To}, {value.Weight}");
    }

    return true; 
  }

  public bool AddInFile(string pathFile)
  {
    using (StreamWriter writer = new StreamWriter(pathFile))
    {
       foreach (var vertex in AdjacencyList)
       {
          writer.Write(vertex.Key + " ");
          foreach (var edge in vertex.Value)
            writer.Write(edge.To + " " + edge.Weight + " ");

          writer.WriteLine();
       }
    }

    return true; 
  }

  public void PrintAdjacencyMatrix()
    {
        var vertices = AdjacencyList.Keys.ToList();
        var n = vertices.Count;
        var matrix = new int[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
                matrix[i, j] = 0;
        }

        for (var i = 0; i < n; i++)
        {
            var vertex = vertices[i];
            var edges = AdjacencyList[vertex];

            foreach (var edge in edges)
            {
              if (edge.To == "") continue;
              var j = vertices.IndexOf(edge.To);
              matrix[i, j] = edge.Weight ?? 1;
            }
        }

        Console.WriteLine(" adjacency matrix:");
        Console.Write("  ");

        for (var i = 0; i < n; i++)
            Console.Write(vertices[i] + " ");

        Console.WriteLine();
        for (var i = 0; i < n; i++)
        {
            Console.Write(vertices[i] + " ");

            for (var j = 0; j < n; j++)
                Console.Write(matrix[i, j] + " ");

            Console.WriteLine();
        }
    }
}
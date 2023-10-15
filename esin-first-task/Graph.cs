using System;
using System.Collections.Generic;
using System.Linq;

namespace esin_first_task;

public class Graph
{
  private Dictionary<string, List<Edge>> _adjacencyList { get; set; }
  
  // constructors
  public Graph() => _adjacencyList = new Dictionary<string, List<Edge>>();

  public Graph(Graph other) =>
    _adjacencyList = new Dictionary<string, List<Edge>>(other._adjacencyList);

  public Graph(string path) => WriteFromFile(path);

  // methods
  public void OutList()
  {
    foreach (var key in _adjacencyList)
    {
      foreach (var value in key.Value)
        Console.WriteLine($"{key.Key}: {value.To}, {value.Weight}");
    }
  }
  public bool AddVertex(string vertex)
  {
    if (!_adjacencyList.ContainsKey(vertex))
    {
      _adjacencyList[vertex] = new List<Edge>();
      _adjacencyList[vertex].Add(new Edge("", null, false));
    }
    else
    {
      Console.WriteLine(" this vertex already exists");
      return false; 
    }

    return true;
  }

  public bool AddEdge(string from, string to, int? weight, bool isDirected)
  {
    if (_adjacencyList.ContainsKey(from) && _adjacencyList.ContainsKey(to))
    {
      _adjacencyList[from].Add(new Edge(to, weight, isDirected));
      if (!isDirected) 
        _adjacencyList[to].Add(new Edge(from, weight, isDirected));
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
    if (_adjacencyList.ContainsKey(vertex))
      _adjacencyList.Remove(vertex);
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
      Edge edge = _adjacencyList[from].FirstOrDefault(e => e.To == to);
      if (edge != null) _adjacencyList[from].Remove(edge);
      else Console.WriteLine(" this edge don't exist");
    }
    else
    {
      Edge edgeOfFrom = _adjacencyList[from].FirstOrDefault(e => e.To == to);
      Edge edgeOfTo = _adjacencyList[to].FirstOrDefault(e => e.To == from);
      if (edgeOfFrom != null && edgeOfTo != null)
      {
        _adjacencyList[from].Remove(edgeOfFrom);
        _adjacencyList[to].Remove(edgeOfTo);
      }
      else
      {
        Console.WriteLine(" this edge don't exist");
        return false; 
      }
    }

    return true; 
  }

  public bool WriteFromFile(string path)
  {
    return true; 
  }

   public void PrintAdjacencyMatrix()
    {
        var vertices = _adjacencyList.Keys.ToList();
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
            var edges = _adjacencyList[vertex];

            foreach (var edge in edges)
            {
                if (edge.To != "")
                {
                    var j = vertices.IndexOf(edge.To);
                    matrix[i, j] = edge.Weight ?? 1;
                }
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
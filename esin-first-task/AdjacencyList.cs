using System;
using System.Collections.Generic;
using System.Linq;

namespace esin_first_task;

public class AdjacencyList
{
  private Dictionary<string, List<Edge>> _adjecencyList { get; set; }
  
  // constructors
  public AdjacencyList() => _adjecencyList = new Dictionary<string, List<Edge>>();

  public AdjacencyList(AdjacencyList other) =>
    _adjecencyList = new Dictionary<string, List<Edge>>(other._adjecencyList);

  public AdjacencyList(string path) => WriteFromFile(path);

  // methods
  public void OutList()
  {
    foreach (var key in _adjecencyList)
    {
      foreach (var value in key.Value)
        Console.WriteLine($"{key.Key}: {value.To}, {value.Weight}");
    }
  }
  public bool AddVertex(string vertex)
  {
    if (!_adjecencyList.ContainsKey(vertex))
    {
      _adjecencyList[vertex] = new List<Edge>();
      _adjecencyList[vertex].Add(new Edge("", null, false));
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
    if (_adjecencyList.ContainsKey(from) && _adjecencyList.ContainsKey(to))
    {
      _adjecencyList[from].Add(new Edge(to, weight, isDirected));
      if (!isDirected) 
        _adjecencyList[to].Add(new Edge(from, weight, isDirected));
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
    if (_adjecencyList.ContainsKey(vertex))
      _adjecencyList.Remove(vertex);
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
      Edge edge = _adjecencyList[from].FirstOrDefault(e => e.To == to);
      if (edge != null) _adjecencyList[from].Remove(edge);
      else Console.WriteLine(" this edge don't exist");
    }
    else
    {
      Edge edgeOfFrom = _adjecencyList[from].FirstOrDefault(e => e.To == to);
      Edge edgeOfTo = _adjecencyList[to].FirstOrDefault(e => e.To == from);
      if (edgeOfFrom != null && edgeOfTo != null)
      {
        _adjecencyList[from].Remove(edgeOfFrom);
        _adjecencyList[to].Remove(edgeOfTo);
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

     public void PrintAdjacencyMatrix(Dictionary<string, List<Edge>> adjacencyList)
    {
        var vertices = adjacencyList.Keys.ToList();
        var n = vertices.Count;
        var matrix = new int[n, n];

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                matrix[i, j] = 0;
            }
        }

        for (var i = 0; i < n; i++)
        {
            var vertex = vertices[i];
            var edges = adjacencyList[vertex];

            foreach (var edge in edges)
            {
                if (edge.To != null)
                {
                    var j = vertices.IndexOf(edge.To);
                    matrix[i, j] = edge.Weight ?? 1;
                }
            }
        }

        Console.WriteLine("Матрица смежности:");
        Console.Write("   ");

        for (var i = 0; i < n; i++)
        {
            Console.Write(vertices[i] + " ");
        }

        Console.WriteLine();

        for (var i = 0; i < n; i++)
        {
            Console.Write(vertices[i] + " ");

            for (var j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }

            Console.WriteLine();
        }
    }

}
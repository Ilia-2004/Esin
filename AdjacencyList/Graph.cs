using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdjacencyList;

// The class for storing data in a graph
public class Graph
{
  private Dictionary<string, List<Edge>> _adjacencyList;
  
  // Constructors
  public Graph() => _adjacencyList = new Dictionary<string, List<Edge>>();

  public Graph(Graph other) =>
    _adjacencyList = new Dictionary<string, List<Edge>>(other._adjacencyList);

  public Graph(string path) => AddFromFile(path);

  // Methods
  // this method outputs a dictionary
  public void OutList()
  {
    foreach (var key in _adjacencyList)
    {
      foreach (var value in key.Value)
        Console.WriteLine($"{key.Key}: {value.To}, {value.Weight}");
    }
  }
  
  // this method adds vertex in the dictionary  
  public bool AddVertex(string vertex)
  {
    if (!_adjacencyList.ContainsKey(vertex))
      _adjacencyList[vertex] = new List<Edge> { new Edge("", null) };
    else
    {
      Console.WriteLine(" this vertex already exists");
      return false; 
    }

    return true;
  }

  // this method adds edges in the dictionary
  public bool AddEdge(string from, string to, int? weight, bool isDirected)
  {
    if (_adjacencyList.ContainsKey(from) && _adjacencyList.ContainsKey(to))
    {
      _adjacencyList[from].Add(new Edge(to, weight));
      if (!isDirected)
        _adjacencyList[to].Add(new Edge(from, weight));
    }
    else
    {
      Console.WriteLine(" these vertices don't exist");
      return false;
    }

    return true;
  }

  // this method removes vertexes in the dictionary 
  public bool RemoveVertex(string vertex)
  {
    if (_adjacencyList.ContainsKey(vertex))
    {
      _adjacencyList.Remove(vertex);
      foreach (var value in from key in _adjacencyList from value in key.Value where value.To == vertex select value)
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

  // this method removes edges in the dictionary 
  public bool RemoveEdge(string from, string to, bool isDirected)
  {
    if (isDirected)
    {
      var edge = _adjacencyList[from].FirstOrDefault(e => e.To == to);
      if (edge != null) _adjacencyList[from].Remove(edge);
      else Console.WriteLine(" this edge don't exist");
    }
    else
    {
      var edgeOfFrom = _adjacencyList[from].FirstOrDefault(e => e.To == to);
      var edgeOfTo = _adjacencyList[to].FirstOrDefault(e => e.To == from);
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

  // this method adds data in the dictionary from a file
  public bool AddFromFile(string pathFile)
  {
    _adjacencyList = new Dictionary<string, List<Edge>>();
    var lines = File.ReadAllLines(pathFile);

    Console.WriteLine(" write number of method: ");
    Console.Write("  > "); var command = Console.ReadLine();

    switch (command)
    {
      case "1":
      {
        var vertexes = lines[1].Trim().Split(' ');
        var listAdjacency = lines.Skip(2).ToArray();

        foreach (var vertex in vertexes)
          _adjacencyList[vertex] = new List<Edge> { new Edge("", null) };
    
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
              _adjacencyList[(i + 1).ToString()].Add(new Edge(j.ToString(), int.Parse(listAdjacencyArr[i, j])));
          }
        }

        break;
      }
      case "2":
      {
        var adjacencyList = new Dictionary<string, List<Edge>>();

        foreach (var line in lines)
        {
          var components = line.Split(' ');
          var vertex = components[0];
          var edges = new List<Edge>();

          for (var i = 1; i < components.Length; i += 2)
          {
            var toVertex = components[i];
            int? weight = null;

            if (i + 1 < components.Length)
            {
              if (int.TryParse(components[i + 1], out var parsedWeight))
                weight = parsedWeight;
            }

            var edge = new Edge(toVertex, weight);
            edges.Add(edge);
          }

          adjacencyList[vertex] = edges;
        }

        _adjacencyList = adjacencyList;
        break;
      }
    }
    foreach (var vertex in _adjacencyList)
    {
      foreach (var value in vertex.Value)
        Console.WriteLine($"{vertex.Key}: {value.To}, {value.Weight}");
    }

    return true; 
  }

  // this method adds data to the file from the dictionary
  public bool AddInFile(string pathFile)
  {
    using var writer = new StreamWriter(pathFile);
    foreach (var vertex in _adjacencyList)
    {
      writer.Write(vertex.Key + " ");
      foreach (var edge in vertex.Value)
        writer.Write(edge.To + " " + edge.Weight + " ");

      writer.WriteLine();
    }

    return true; 
  }

  // this method outputs an adjacency matrix
  public void OutputsAdjacencyMatrix() 
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
  
  // this method builds a complete graph
  public static Graph BuildCompleteGraph(Graph g)
  {
    var completeGraph = new Graph();

    foreach (var vertex in g._adjacencyList.Keys)
      completeGraph.AddVertex(vertex);

    foreach (var from in g._adjacencyList.Keys)
    {
      foreach (var to in g._adjacencyList.Keys)
        completeGraph.AddEdge(from, to, 1, false);
    }
    
    return completeGraph; 
  }
}
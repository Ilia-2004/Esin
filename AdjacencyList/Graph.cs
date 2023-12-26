using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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
  public void OutputAdjacencyList()
  {
    foreach (var key in _adjacencyList)
    {
      Console.Write(key.Key);
      foreach (var value in key.Value)
        Console.WriteLine($": {value.To}, {value.Weight}");
      Console.WriteLine();
    }
  }

  // this method adds vertex in the dictionary  
  public bool AddVertex(string vertex)
  {
    if (string.IsNullOrEmpty(vertex)) return false;

    if (!_adjacencyList.ContainsKey(vertex))
      _adjacencyList[vertex] = new List<Edge>();
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
    if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(from)) return false;
    
    AddVertex(from);
    AddVertex(to);
    
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
      foreach (var value in from key in _adjacencyList
               from value in key.Value
               where value.To == vertex
               select value)
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
    Console.Write("  > ");
    var command = Console.ReadLine();

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
  public static Graph s_BuildCompleteGraph(Graph g)
  {
    var completeGraph = new Graph();

    foreach (var vertex in g._adjacencyList.Keys)
      completeGraph.AddVertex(vertex);

    foreach (var from in g._adjacencyList.Keys)
    {
      foreach (var to in g._adjacencyList.Keys)
        completeGraph.AddEdge(from, to, null, false);
    }

    return completeGraph;
  }

  // this method builds a complement graph
  public static Graph s_BuildComplementGraph(Graph g)
  {
    var complementGraph = new Graph();

    foreach (var vertex in g._adjacencyList.Keys)
      complementGraph.AddVertex(vertex);

    foreach (var vertex1 in g._adjacencyList.Keys)
    {
      foreach (var vertex2 in g._adjacencyList.Keys.Where(vertex2 =>
                 !HasEdgeToVertex(g._adjacencyList[vertex1], vertex2)))
        complementGraph.AddEdge(vertex1, vertex2, null, false);
    }

    return complementGraph;

    bool HasEdgeToVertex(IEnumerable<Edge> edges, string targetVertex) => edges.Any(edge => edge.To == targetVertex);
  }

  // this method builds a combined graph
  public static Graph s_BuildCombinedGraph(Graph g)
  {
    var combinedGraph = new Graph();
    var adjacencyList = new Graph();

    adjacencyList.AddFromFile(@"F:\Ilya\Programming\Esin\AdjacencyList\test-files\file3.txt");

    foreach (var vertex in g._adjacencyList)
    {
      combinedGraph.AddVertex(vertex.Key);
      foreach (var value in vertex.Value)
        combinedGraph.AddEdge(vertex.Key, value.To, value.Weight, false);
    }

    foreach (var vertex in adjacencyList._adjacencyList)
    {
      if (!combinedGraph._adjacencyList.Contains(vertex))
      {
        var addVertex = combinedGraph.AddVertex(vertex.Key);
        if (!addVertex) return combinedGraph;
        foreach (var value in vertex.Value)
          combinedGraph.AddEdge(vertex.Key, value.To, value.Weight, false);
      }
      else
      {
        Console.WriteLine("  this vertex already exists");
        break;
      }
    }

    foreach (var vertex1 in g._adjacencyList.Keys)
    {
      foreach (var vertex2 in adjacencyList._adjacencyList.Keys)
        combinedGraph.AddEdge(vertex1, vertex2, null, false);
    }

    combinedGraph.OutputsAdjacencyMatrix();

    return combinedGraph;
  }

  // this method builds a connected graph
  public static Graph s_uildConnectedGraph(Graph g)
  {
    var connectedGraph = new Graph();
    var adjacencyList = new Graph();

    adjacencyList.AddFromFile(@"D:\ISP-42\Krasnenkov-2\AdjacencyList\test-file\file3.txt");

    foreach (var vertex in g._adjacencyList)
    {
      connectedGraph.AddVertex(vertex.Key);
      foreach (var value in vertex.Value)
        connectedGraph.AddEdge(vertex.Key, value.To, value.Weight, false);
    }

    foreach (var vertex in adjacencyList._adjacencyList)
    {
      if (!connectedGraph._adjacencyList.Contains(vertex))
      {
        var addVertex = connectedGraph.AddVertex(vertex.Key);
        if (!addVertex) return connectedGraph;
        foreach (var value in vertex.Value)
          connectedGraph.AddEdge(vertex.Key, value.To, value.Weight, false);
      }
      else
      {
        Console.WriteLine("  this vertex already exists");
        break;
      }
    }

    connectedGraph.OutputsAdjacencyMatrix();

    return connectedGraph;
  }

  // this method is for checking for the digraph
  public int CheckingForDigraph()
  {
    var count = 0;
    var visited = new HashSet<string>();

    foreach (var vertex in _adjacencyList.Keys.Where(vertex =>
               _adjacencyList[vertex] != null && !visited.Contains(vertex)))
    {
      _dfs(vertex, visited);
      count++;
    }

    return count;
  }

  // the support method for the CheckingForDigraph method
  private void _dfs(string vertex, HashSet<string> visited)
  {
    visited.Add(vertex);

    if (!_adjacencyList.ContainsKey(vertex) || _adjacencyList[vertex] == null) return;
    foreach (var neighbor in _adjacencyList[vertex].Where(e => e is { To: not null }).Select(e => e.To))
    {
      if (!visited.Contains(neighbor))
        _dfs(neighbor, visited);
    }
  }

  // the method for checking the graph type
  public string CheckGraphType()
  {
    var visited = new HashSet<string>();
    var connectedComponents = CheckingForDigraph();

    switch (connectedComponents)
    {
      case 1:
        return "  the graph is a tree";

      case > 1:
      {
        foreach (var vertex in _adjacencyList.Keys)
        {
          visited.Clear();

          if (visited.Contains(vertex)) continue;
          if (_hasCycles(vertex, visited, null))
            return "  it is neither a forest nor a tree";
        }

        return "  is a forest (several connected trees)";
      }

      default:
        return "  it is neither a forest nor a tree";
    }
  }

  // the support method for the CheckGraphType method
  private bool _hasCycles(string vertex, HashSet<string> visited, string parent)
  {
    visited.Add(vertex);

    if (!_adjacencyList.ContainsKey(vertex) || _adjacencyList[vertex] == null) return false;
    foreach (var neighbor in _adjacencyList[vertex].Where(e => e is { To: not null }).Select(e => e.To))
    {
      if (!visited.Contains(neighbor))
      {
        if (_hasCycles(neighbor, visited, vertex))
          return true;
      }
      else if (neighbor != parent) return true;
    }

    return false;
  }
  public (Graph, int?) KruskalMst()
  {
    var mst = new Graph();
    var parent = new Dictionary<string, string>();
            
    foreach (var vertex in _adjacencyList.Keys)
      parent[vertex] = vertex;

    var sortedEdges = _adjacencyList
      .SelectMany(kvp => kvp.Value.Select(adj => new { Source = kvp.Key, Destination = adj.To, Weight = adj.Weight }))
      .OrderBy(edge => edge.Weight)
      .ToList();
            
    int? totalWeight = 0; 
            
    foreach (var edge in sortedEdges)
    {
      var root1 = s_Find(edge.Source, parent);
      var root2 = s_Find(edge.Destination, parent);
                
      if (root1 != root2)
      {
        mst.AddEdge(edge.Source, edge.Destination, edge.Weight, false);
        totalWeight += edge.Weight; 
        s_Union(root1, root2, parent);
      }
    }

    return (mst, totalWeight);
  }

  private static string s_Find(string vertex, Dictionary<string, string> parent)
  {
    if (parent[vertex] != vertex)
      parent[vertex] = s_Find(parent[vertex], parent);
            
    return parent[vertex];
  }

  private static void s_Union(string root1, string root2, Dictionary<string, string> parent) => parent[root1] = root2;
}
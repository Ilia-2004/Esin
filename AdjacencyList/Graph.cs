using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdjacencyList;

public class Graph
{
  public class Edge
  {
    public string To;
    public int? Weight;

    public Edge(string to, int? weight)
    {
       To = to;
       Weight = weight;
    }
  }

  private Dictionary<string, List<Edge>> AdjacencyList;

   public Graph()
   {
      AdjacencyList = new Dictionary<string, List<Edge>>();
   }

  public Graph(Graph other)
  {
      AdjacencyList = new Dictionary<string, List<Edge>>(other.AdjacencyList);
  }

    public Graph(string path)
    {
        AddFromFile(path);
    }

  // Методы
  
  // вывод списка
  public void OutputAdjacencyList()
  {
    foreach (var key in AdjacencyList)
    {
      Console.Write(key.Key);
      foreach (var value in key.Value)
        Console.WriteLine($": {value.To}, {value.Weight}");
      Console.WriteLine();
    }
  }
  
  // добавление вершины  
  public bool AddVertex(string vertex)
  {
    if (vertex == "") return false; 
    
    if (!AdjacencyList.ContainsKey(vertex))
      AdjacencyList[vertex] = new List<Edge>();
    else
    {
      Console.WriteLine("эта вершина уже существует");
      return false; 
    }

    return true;
  }

  // добавление ребра
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
      Console.WriteLine("этих вершин нет");
      return false;
    }

    return true;
  }

  // удаление вершины 
  public bool RemoveVertex(string vertex)
  {
    if (AdjacencyList.ContainsKey(vertex))
    {
      AdjacencyList.Remove(vertex);
      foreach (var value in from key in AdjacencyList from value in key.Value 
               where value.To == vertex select value)
      {
        value.To = string.Empty; 
        value.Weight = null;
      }
    }
    else
    {
      Console.WriteLine("этой вершины нет");
      return false; 
    }

    return true;
  }

  // удаление ребра 
  public bool RemoveEdge(string from, string to, bool isDirected)
  {
    if (isDirected)
    {
      var edge = AdjacencyList[from].FirstOrDefault(e => e.To == to);
      if (edge != null) AdjacencyList[from].Remove(edge);
      else Console.WriteLine("этой вершины нет");
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
        Console.WriteLine("этой вершины нет");
        return false; 
      }
    }

    return true; 
  }

  // добавление из файла
  public bool AddFromFile(string pathFile)
  {
    AdjacencyList = new Dictionary<string, List<Edge>>();
    var lines = File.ReadAllLines(pathFile);

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

        AdjacencyList = adjacencyList;
     
    
    foreach (var vertex in AdjacencyList)
    {
      foreach (var value in vertex.Value)
        Console.WriteLine($"{vertex.Key}: {value.To}, {value.Weight}");
    }

    return true; 
  }

  // добавление в файл
  public bool AddInFile(string pathFile)
  {
    using var writer = new StreamWriter(pathFile);
    foreach (var vertex in AdjacencyList)
    {
      writer.Write(vertex.Key + " ");
      foreach (var edge in vertex.Value)
        writer.Write(edge.To + " " + edge.Weight + " ");

      writer.WriteLine();
    }

    return true; 
  }

  // вывод матрицы
  public void OutputsAdjacencyMatrix() 
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

    Console.WriteLine(" Матрица:");
    Console.Write(" ");

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
  
  // построение полного графа
  public static Graph BuildFullGraph(Graph g)
  {
    var completeGraph = new Graph();

    foreach (var vertex in g.AdjacencyList.Keys) 
      completeGraph.AddVertex(vertex);

    foreach (var from in g.AdjacencyList.Keys)
    {
      foreach (var to in g.AdjacencyList.Keys)
       completeGraph.AddEdge(from, to, null, false);
    }
    
    return completeGraph; 
  }
  
  // построение дополнительного графа
  public static Graph BuildComplementGraph(Graph g)
  {
    var complementGraph = new Graph();
    
    foreach (var vertex in g.AdjacencyList.Keys) 
      complementGraph.AddVertex(vertex);

    foreach (var vertex1 in g.AdjacencyList.Keys)
    {
      foreach (var vertex2 in g.AdjacencyList.Keys.Where(vertex2 => !HasEdgeToVertex(g.AdjacencyList[vertex1], vertex2)))
        complementGraph.AddEdge(vertex1, vertex2, null, false);
    }
    
    return complementGraph;

    bool HasEdgeToVertex(IEnumerable<Edge> edges, string targetVertex) => edges.Any(edge => edge.To == targetVertex);
  }
  
  // построение объединённого графа
  public static Graph BuildCombinedGraph(Graph g, string pathFile)
  {
    var combinedGraph = new Graph();
    var adjacencyList = new Graph();
    
    adjacencyList.AddFromFile($@"D:\esin\AdjacencyList\test-files\{pathFile}");

    foreach (var vertex in g.AdjacencyList)
    {
      combinedGraph.AddVertex(vertex.Key);
      foreach (var value in vertex.Value)
        combinedGraph.AddEdge(vertex.Key, value.To, value.Weight, false); 
    }

    foreach (var vertex in adjacencyList.AdjacencyList)
    {
      if (combinedGraph.AdjacencyList.Contains(vertex))
      {
        Console.WriteLine("эта вершина уже 64646464существует");
        return combinedGraph; 
      }
        var proverka = combinedGraph.AddVertex(vertex.Key);
            if (!proverka) return combinedGraph;
        foreach (var value in vertex.Value)
          combinedGraph.AddEdge(vertex.Key, value.To, value.Weight, false);
    }

    foreach (var vertex1 in g.AdjacencyList.Keys)
    {
      foreach (var vertex2 in adjacencyList.AdjacencyList.Keys)
        combinedGraph.AddEdge(vertex1, vertex2, null, false);
    }

    combinedGraph.OutputsAdjacencyMatrix();
    
    return combinedGraph; 
  }

  // построение соединённого графа
  public static Graph BuildConnectedGraph(Graph g, string pathFile)
  {
    var connectedGraph = new Graph();
    var adjacencyList = new Graph();

    adjacencyList.AddFromFile($@"D:\esin\AdjacencyList\test-files\{pathFile}");

    foreach (var vertex in g.AdjacencyList)
    {
      connectedGraph.AddVertex(vertex.Key);
      foreach (var value in vertex.Value)
        connectedGraph.AddEdge(vertex.Key, value.To, value.Weight, false);
    }

    foreach (var vertex in adjacencyList.AdjacencyList)
    {
      if (!connectedGraph.AdjacencyList.Contains(vertex))
      {
        var proverka = connectedGraph.AddVertex(vertex.Key);
        if (!proverka) return connectedGraph;
        foreach (var value in vertex.Value)
          connectedGraph.AddEdge(vertex.Key, value.To, value.Weight, false);
      }
      else
      {
        Console.WriteLine("эта вершина уже существует");
        break;
      }
    }

        connectedGraph.OutputsAdjacencyMatrix();

    return connectedGraph;
  }

  // проверка на Орграф
  public int CheckingForDigraph()
  {
    var count = 0;
    var visited = new HashSet<string>();

    foreach (var vertex in AdjacencyList.Keys.Where(vertex => AdjacencyList[vertex] != null && !visited.Contains(vertex)))
    {
      dfs(vertex, visited);
      count++;
    }

    return count;
  }
  private void dfs(string vertex, HashSet<string> visited)
  {
    visited.Add(vertex);
    
    if (!AdjacencyList.ContainsKey(vertex) || AdjacencyList[vertex] == null) return;
    foreach (var neighbor in AdjacencyList[vertex].Where(e => e is { To: not null }).Select(e => e.To))
    {
      if (!visited.Contains(neighbor))
        dfs(neighbor, visited);
    }
  }

  // проверка типа графа
  public string CheckGraphType()
  {
    var visited = new HashSet<string>();
    var connectedComponents = CheckingForDigraph();
  
    switch (connectedComponents)
    {
      case 1:
        return "Дерево";
          
      case > 1:
      {
        foreach (var vertex in AdjacencyList.Keys)
        {
          visited.Clear();
              
          if (visited.Contains(vertex)) continue;
          if (hasCycles(vertex, visited, null))
            return "Ни лес, ни дерево";
        }
            
        return "Лес";
      }
          
      default:
        return "Ни лес, ни дерево";
    }
  }
  private bool hasCycles(string vertex, HashSet<string> visited, string parent)
  {
    visited.Add(vertex);

    if (!AdjacencyList.ContainsKey(vertex) || AdjacencyList[vertex] == null) return false;
    foreach (var neighbor in AdjacencyList[vertex].Where(e => e is { To: not null }).Select(e => e.To))
    {
      if (!visited.Contains(neighbor))
      {
        if (hasCycles(neighbor, visited, vertex)) 
          return true;
      }
      else if (neighbor != parent) return true;
    }

    return false;
  }
  
  // алгоритм Крусталя
  public static Graph Execute(Graph graph)
  {
    var mstGraph = new Graph();
    var edges = new List<(string From, Edge Edge)>();

    foreach (var kvp in graph.AdjacencyList)
    {
      foreach (var edge in kvp.Value)
      {
        if (edge.To != null && edge.Weight.HasValue)
          edges.Add((kvp.Key, edge));
      }
    }

    var sortedEdges = edges.OrderBy(e => e.Edge.Weight).ToList();
    Dictionary<string, string> parent = new();

    foreach (var vertex in graph.AdjacencyList.Keys.Where(vertex => vertex != null))
      parent[vertex] = vertex;

    foreach (var (from, edge) in sortedEdges)
    {
      if (from != null && edge.To != null)
      {
        var root1 = findRoot(from, parent);
        var root2 = findRoot(edge.To, parent);

        if (root1 != null && root2 != null && root1 != root2)
        {
          if (!mstGraph.AdjacencyList.ContainsKey(root1))
            mstGraph.AdjacencyList[root1] = new List<Edge>();
          mstGraph.AdjacencyList[root1].Add(new Edge(root2, edge.Weight));
          parent[root1] = root2;
        }
      }
    }

    return mstGraph;
  }
  private static string findRoot(string vertex, IDictionary<string, string> parent)
  {
    if (vertex == null || !parent.ContainsKey(vertex)) return null;

    if (parent[vertex] != vertex)
      parent[vertex] = findRoot(parent[vertex], parent);

    return parent[vertex];
  }
}
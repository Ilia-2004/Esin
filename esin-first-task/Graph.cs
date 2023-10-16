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

  public Graph(string path) => WriteFromFile(path);

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
      AdjacencyList[vertex] = new List<Edge> { new Edge("", null, false) };
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
      AdjacencyList[from].Add(new Edge(to, weight, isDirected));
      if (!isDirected) 
        AdjacencyList[to].Add(new Edge(from, weight, false));
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

  public void WriteFromFile(string path)
  {
    // Считываем все строки из файла
    var lines = File.ReadAllLines(path);
    
    // Проходим по каждой строке
    foreach (var t in lines)
    {
      // Разделяем строку на отдельные элементы
      var row = t.Split(' ');

      // Получаем имя вершины
      var vertexName = row[0];

      // Создаем список ребер для данной вершины
      var edges = new List<Edge>();

      // Проходим по каждому элементу в строке
      for (var j = 1; j < row.Length; j++)
      {
        // Получаем вес ребра
        var weight = int.Parse(row[j]);

        // Если вес ребра больше 0, добавляем его в список ребер
        if (weight <= 0) continue;
        var connectedVertexName = lines[j].Split(' ')[0];
        edges.Add(new Edge(connectedVertexName, weight));
      }

      // Добавляем список ребер для данной вершины в словарь смежности
      AdjacencyList.Add(vertexName, edges);
    }
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
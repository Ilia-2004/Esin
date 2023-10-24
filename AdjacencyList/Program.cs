using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Runtime.Intrinsics.Arm;

class Graph
{
    public class Edge
    {
        public string? To { get; set; }
        public double? Weight { get; set; }
        public bool IsDirected { get; set; }
        public Edge(string? to, double? weight) => (To, Weight) = (to, weight);
    }

    public Dictionary<string, List<Edge>> AdjacencyList { get; private set; } = new();


    // Конструкторы
    public Graph()
    {
    }
    public Graph(string filename) => LoadFromFile(filename);
    public Graph(Graph other) => AdjacencyList = new Dictionary<string, List<Edge>>(other.AdjacencyList);

    // Методы
    public void AddVertex(string vertex, bool isDirected)
    {
        AdjacencyList[vertex] = new List<Edge>();
        AdjacencyList[vertex].Add(new Edge("", null));
        foreach (var vertexs in AdjacencyList)
        {
            foreach (var edge in vertexs.Value)
            {
                Console.WriteLine($"{vertexs.Key} {edge.To} {edge.Weight} {edge.IsDirected}");
            }
        }
    }

    public void AddEdge(string from, string to, double? weight, bool isDirected = false)
    {
        if (!AdjacencyList.ContainsKey(from)) AddVertex(from, isDirected);
        AdjacencyList[from].Add(new Edge(to, weight));
        if (!isDirected)
        {
            if (!AdjacencyList.ContainsKey(to)) AddVertex(to, isDirected);
            AdjacencyList[to].Add(new Edge(from, weight));
        }
    }

    public void RemoveVertex(string vertex) => AdjacencyList.Remove(vertex);

    public void RemoveEdge(string from, string to)
    {
        Edge edge = AdjacencyList[from].FirstOrDefault(e => e.To == to);
        if (edge != null) AdjacencyList[from].Remove(edge);
        //SaveToFile(filename);
    }

    public void LoadFromFile(string filename)
    {
        using StreamReader reader = new StreamReader(filename);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(' ');
            if (parts.Length != 4) break;
            // else if(parts[1] == null && parts[2]==null) AddEdge(parts[0], null, null, bool.Parse(parts[3]));
            // else if (parts[1] == null) AddEdge(parts[0], null, double.Parse(parts[2]), bool.Parse(parts[3]));
            if (parts[2] == "") AddVertex(parts[0], bool.Parse(parts[3]));
            else AddEdge(parts[0], parts[1], double.Parse(parts[2]), bool.Parse(parts[3]));
        }
    }
    // public void AddVertexWithZeroEdges(string vertexName, bool isDirected = false )
    // {
    //     AddVertex(vertexName);
    //     foreach (var existingVertex in AdjacencyList.Keys)
    //     {
    //         if (existingVertex != vertexName)
    //         { 
    //             AddEdge(vertexName, existingVertex, 0, isDirected);
    //         }
    //     }
    // }

    public void SaveToFile(string filename)
    {
        using StreamWriter writer = new StreamWriter(filename, false);
        foreach (var vertex in AdjacencyList)
        {
            foreach (var edge in vertex.Value)
            {

                // Console.WriteLine($"{vertex.Key} {edge.To} {edge.Weight} {edge.IsDirected}");
                writer.WriteLine($"{vertex.Key} {edge.To} {edge.Weight} {edge.IsDirected}");
            }
        }

    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var v in AdjacencyList)
        {
            StringBuilder edges = new StringBuilder();
            foreach (var e in v.Value)
            {
                edges.Append(e.To);
                edges.Append(", ");
            }
            if (edges.Length > 0)
            {
                edges.Length -= 2; // Remove the last comma and space
            }
            sb.AppendLine($"{v.Key}: {edges}");
        }
        return sb.ToString();
    }

    public void PrintAdjacencyMatrix()
    {
        var vertices = AdjacencyList.Keys.ToList();
        int n = vertices.Count;
        double[,] matrix = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            foreach (var edge in AdjacencyList[vertices[i]])
            {
                int j = vertices.IndexOf(edge.To);
                if (j != -1)
                    if (edge.Weight != null)
                        matrix[i, j] = edge.Weight.Value;
            }
        }

        Console.Write("    ");
        foreach (var vertex in vertices)
            Console.Write($"{vertex} ");
        Console.WriteLine();

        for (int i = 0; i < n; i++)
        {
            Console.Write($"{vertices[i]}   ");
            for (int j = 0; j < n; j++)
                Console.Write($"{(matrix[i, j] == 0 ? "-" : matrix[i, j].ToString())} ");
            Console.WriteLine();
        }
    }



}

class Program
{
    static void Main()
    {
        // string D = "S    SS    SSS";
        // string[] d = D.Split(' ');
        // Console.WriteLine(D);
        // foreach (var VARIABLE in d)
        // {
        //     if (VARIABLE == "") 
        //         Console.WriteLine("-");
        //     else
        //         Console.WriteLine(VARIABLE);            
        //     
        // }
        Graph g = new();
        Console.WriteLine("Enter file name:");
        string filename = Console.ReadLine();
        g = new Graph(filename);
        try
        {
            g = new Graph(filename);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading file: {e.Message}");

        }

        Console.WriteLine("Your graph is directed(0 - true / 1 - false)");
        int dr = int.Parse(Console.ReadLine());
        bool dir = dr == 0 ? true : false;

        Console.WriteLine("Your graph is weighted(0 - true / 1 - false)");
        int we = int.Parse(Console.ReadLine());
        bool wi = we == 0 ? true : false;
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Add Vertex");
            Console.WriteLine("2. Add Edge");
            Console.WriteLine("3. Remove Vertex");
            Console.WriteLine("4. Remove Edge");
            Console.WriteLine("5. Display Graph");
            Console.WriteLine("6. Display Adjacency Matrix");
            Console.WriteLine("7: Load graph from file");
            Console.WriteLine("8: Build and print full graph");
            Console.WriteLine("9: Build and print complement graph");
            Console.WriteLine("10. Load Graph From File");
            Console.WriteLine("11. Exit");
            Console.Write("Select an option: ");

            string option = Console.ReadLine();
            bool isDirected = dir ? true : false;
            switch (option)
            {

                case "1":
                    Console.Write("Vertex name: ");
                    string n = Console.ReadLine();
                    g.AddVertex(n, isDirected);
                    // g.AddVertexWithZeroEdges(n, isDirected);
                    g.SaveToFile(filename);
                    break;

                case "2":
                    Console.Write("From: ");
                    string from = Console.ReadLine();
                    Console.Write("To: ");
                    string to = Console.ReadLine();
                    if (wi) Console.Write("Weight: ");
                    double weight = wi ? double.Parse(Console.ReadLine()) : 1;
                    //if (dir) Console.Write("Is directed (y/n): ");

                    g.AddEdge(from, to, weight, isDirected);
                    g.SaveToFile(filename);
                    break;
                case "3":
                    Console.Write("Vertex name: ");
                    g.RemoveVertex(Console.ReadLine());
                    g.SaveToFile(filename);
                    break;

                case "4":
                    Console.Write("From: ");
                    from = Console.ReadLine();
                    Console.Write("To: ");
                    to = Console.ReadLine();
                    g.RemoveEdge(from, to);
                    g.SaveToFile(filename);
                    break;

                case "5":
                    Console.WriteLine(g);
                    break;

                case "6":
                    g.PrintAdjacencyMatrix();
                    break;

                case "7":
                    Console.Write($"Filename: {filename}");
                    break;

                case "11":
                    return;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}

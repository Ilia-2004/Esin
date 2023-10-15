using System;

namespace esin_first_task
{
    internal class Program
    {
    public static void Main()
    {
      Graph objectGraph = new Graph();
      string command = string.Empty;
      
      while (true)
      { 
        Console.WriteLine("GRAPH ADJACENCY LIST");
        Console.WriteLine(" $ what do you want to do? | if you need help input '--h'");
        Console.Write("  > "); command = Console.ReadLine();
        
        if (command == "--h")
        {
          Console.WriteLine(" $ output an adjacency list,           '--out-list'");
          Console.WriteLine(" $ add a graph vertex,                 '--add-vert'");
          Console.WriteLine(" $ add a graph edge,                   '--add-edge'");
          Console.WriteLine(" $ remove a graph vertex,              '--rem-vert'");
          Console.WriteLine(" $ add a graph egde,                   '--rem-egde'");
          Console.WriteLine(" $ add a graph egde,                   '--rem-egde'");
          Console.WriteLine(" $ input a matrix of adjacency list,   '--write-mat'");
          Console.WriteLine(" $ if you need help,                   '--h'");
          Console.WriteLine(" $ exit programm,                      '--e'");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--out-list")
        {
           objectGraph.OutList();
           
           Console.ReadKey();
           Console.Clear();
        }
        else if (command == "--add-vert")
        {
          string vertex = "";
          
          Console.WriteLine(" $ input your vertex:");
          Console.Write("  > "); vertex = Console.ReadLine();
          
          bool addVertex = objectGraph.AddVertex(vertex);
          if (addVertex)
            Console.WriteLine(" $ your vertex has been successfully added");
          else 
            Console.WriteLine(" $ [ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--add-edge")
        {
          string to = string.Empty;
          string from = string.Empty;
          int? weight = null; 
          bool isDirected = false;

          try
          {
            Console.WriteLine(" $ input your edge 'from':");
            Console.Write("  > "); from = Console.ReadLine();
            
            Console.WriteLine(" $ input your edge 'to':");
            Console.Write("  > "); to = Console.ReadLine();
            
            Console.WriteLine(" $ input your edge weight:");
            Console.Write("  > "); string value = Console.ReadLine();
            if (value != "0") weight = Convert.ToInt32(value); 
            
            Console.WriteLine(" $ input directed:");
            Console.Write("  > "); isDirected = Convert.ToBoolean(Console.ReadLine());
          }
          catch
          {
            Console.WriteLine(" $ [ERROR]: you wrote an incorrect dataset!");
            Console.ReadKey();
            Console.Clear();
            
            continue; 
          }
          
          bool addEdge = objectGraph.AddEdge(from, to, weight, isDirected);
          if (addEdge) 
            Console.WriteLine(" $ your edge has been successfully added");
          else 
            Console.WriteLine(" $ [ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--rem-vert")
        {
          string vertex = "";
          
          Console.WriteLine(" $ input your vertex:");
          Console.Write("  > "); vertex = Console.ReadLine();
          
          bool remVert = objectGraph.RemoveVertex(vertex);
          if (remVert)
            Console.WriteLine(" $ your vertex has been successfully removed");
          else 
            Console.WriteLine(" $ [ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--rem-edge")
        {
          string to = "";
          string from = "";
          int? weight = null; 
          bool isDirected = false; 
          
          Console.WriteLine("$ input your vertex 'from':");
          Console.Write(" > "); from = Console.ReadLine();
          
          Console.WriteLine("$ input your vertex 'to':");
          Console.Write(" > "); to = Console.ReadLine();
          
          Console.WriteLine("$ input directed:");
          Console.Write(" > "); isDirected = Convert.ToBoolean(Console.ReadLine());
          
          bool remEdge = objectGraph.RemoveEdge(from, to, isDirected);
          if (remEdge)
            Console.WriteLine("$ your edge has been successfully removed");
          else 
            Console.WriteLine("$ [ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--write-mat")
        {
          objectGraph.PrintAdjacencyMatrix();

          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--e") break;
        else
        {
          Console.WriteLine("$ [ERROR]: you wrote an incorrect command!");
          Console.ReadKey();
          Console.Clear();
        }
      }
    }
  }
}
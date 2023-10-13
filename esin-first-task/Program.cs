using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;

namespace esin_first_task
{
  internal class Program
  {
    public static void Main()
    {
      Thread.Sleep(60);
      AdjacencyList objectGraph = new AdjacencyList();
      string command = "";
      
      while (true)
      { 
        Console.WriteLine("\u001b[1mGRAPH ADJACENCY LIST\u001b[0m");
        Console.WriteLine(" \u001b[1m$ \u001b[0m" + 
                          "what do you want to do? | if you need help input '--h'");
        Console.Write("  \u001b[1m> \u001b[0m"); command = Console.ReadLine();
        
        if (command == "--h")
        {
          Console.WriteLine(" \u001b[1m$ \u001b[0m" +
                            "output an adjacency list,     '--out-list'");
          Console.WriteLine(" \u001b[1m$ \u001b[0m" + 
                            "add a graph vertex,           '--add-vert'");
          Console.WriteLine(" \u001b[1m$ \u001b[0m" +
                            "add a graph edge,             '--add-edge'");
          Console.WriteLine(" \u001b[1m$ \u001b[0m" + 
                            "remove a graph vertex,        '--rem-vert'");
          Console.WriteLine(" \u001b[1m$ \u001b[0m" + 
                            "add a graph egde,             '--rem-egde'");
          Console.WriteLine(" \u001b[1m$ \u001b[0m" +
                            "add a graph egde,             '--rem-egde'");
          Console.WriteLine(" \u001b[1m$ \u001b[0m" +
                            "if you need help,             '--h'");
          Console.WriteLine(" \u001b[1m$ \u001b[0m" +
                            "exit programm,                '--e'");
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
          
          Console.WriteLine(" \u001b[1m$ \u001b[0minput your vertex:");
          Console.Write("  \u001b[1m> \u001b[0m"); vertex = Console.ReadLine();
          
          bool addVertex = objectGraph.AddVertex(vertex);
          if (addVertex)
            Console.WriteLine(" \u001b[1m$ \u001b[0myour vertex has been successfully added");
          else 
            Console.WriteLine(" \u001b[1m$ \u001b[0m[ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--add-edge")
        {
          string to = "";
          string from = "";
          int? weight = null; 
          bool isDirected = false; 
          
          Console.WriteLine(" \u001b[1m$ \u001b[0minput your edge 'from':");
          Console.Write("  \u001b[1m> \u001b[0m"); from = Console.ReadLine();
          Console.WriteLine(" \u001b[1m$ \u001b[0minput your edge 'to':");
          Console.Write("  \u001b[1m> \u001b[0m"); to = Console.ReadLine();
          Console.WriteLine(" \u001b[1m$ \u001b[0minput your edge weight:");
          Console.Write("  \u001b[1m> \u001b[0m"); weight = Convert.ToInt32(Console.ReadLine());
          Console.WriteLine(" \u001b[1m$ \u001b[0minput directed:");
          Console.Write("  \u001b[1m> \u001b[0m"); isDirected = Convert.ToBoolean(Console.ReadLine());
          
          bool addEdge = objectGraph.AddEdge(from, to, weight, isDirected);
          if (addEdge) 
            Console.WriteLine(" \u001b[1m$ \u001b[0myour edge has been successfully added");
          else 
            Console.WriteLine(" \u001b[1m$ \u001b[0m[ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--rem-vert")
        {
          string vertex = "";
          
          Console.WriteLine(" \u001b[1m$ \u001b[0minput your vertex:");
          Console.Write("  \u001b[1m> \u001b[0m"); vertex = Console.ReadLine();
          
          bool remVert = objectGraph.RemoveVertex(vertex);
          if (remVert)
            Console.WriteLine(" \u001b[1m$ \u001b[0myour vertex has been successfully removed");
          else 
            Console.WriteLine(" \u001b[1m$ \u001b[0m[ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--rem-edge")
        {
          string to = "";
          string from = "";
          int? weight = null; 
          bool isDirected = false; 
          
          Console.WriteLine(" \u001b[1m$ \u001b[0minput your vertex 'from':");
          Console.Write("  \u001b[1m> \u001b[0m"); from = Console.ReadLine();
          Console.WriteLine(" \u001b[1m$ \u001b[0minput your vertex 'to':");
          Console.Write("  \u001b[1m> \u001b[0m"); to = Console.ReadLine();
          Console.WriteLine(" \u001b[1m$ \u001b[0minput directed:");
          Console.Write("  \u001b[1m> \u001b[0m"); isDirected = Convert.ToBoolean(Console.ReadLine());
          
          bool remEdge = objectGraph.RemoveEdge(from, to, isDirected);
          if (remEdge)
            Console.WriteLine(" \u001b[1m$ \u001b[0myour edge has been successfully removed");
          else 
            Console.WriteLine(" \u001b[1m$ \u001b[0m[ERROR]: you wrote an incorrect command!");
          
          Console.ReadKey();
          Console.Clear();
        }
        else if (command == "--e") break;
        else
        {
          Console.WriteLine(" \u001b[1m$ \u001b[0m[ERROR]: you wrote an incorrect command!");
          Console.ReadKey();
          Console.Clear();
        }
      }
    }
  }
}
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
        Console.WriteLine("GRAPH ADJACENCY LIST");
        Console.WriteLine(" $ what do you want to do? | if you need help input '--h'");
        Console.Write("  > "); command = Console.ReadLine();
        
        if (command == "--h")
        {
          Console.WriteLine(" $ output an adjacency list,     '--out-list'");
          Console.WriteLine(" $ add a graph vertex,           '--add-vert'");
          Console.WriteLine(" $ add a graph edge,             '--add-edge'");
          Console.WriteLine(" $ remove a graph vertex,        '--rem-vert'");
          Console.WriteLine(" $ add a graph egde,             '--rem-egde'");
          Console.WriteLine(" $ add a graph egde,             '--rem-egde'");
          Console.WriteLine(" $ if you need help,             '--h'");
          Console.WriteLine(" $ exit programm,                '--e'");
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
          string to = "";
          string from = "";
          int? weight = null; 
          bool isDirected = false; 
          
          Console.WriteLine(" $ input your edge 'from':");
          Console.Write("  > "); from = Console.ReadLine();
          Console.WriteLine(" $ input your edge 'to':");
          Console.Write("  > "); to = Console.ReadLine();
          Console.WriteLine(" $ input your edge weight:");
          Console.Write("  > "); weight = Convert.ToInt32(Console.ReadLine());
          Console.WriteLine(" $ input directed:");
          Console.Write("  > "); isDirected = Convert.ToBoolean(Console.ReadLine());
          
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
            Console.WriteLine("$ [ERROR]: you wrote an incorrect command!");
          
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
using System;
using esin_first_task;


class Program
{
    static void Main()
    {
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

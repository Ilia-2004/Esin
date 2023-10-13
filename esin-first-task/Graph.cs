using System;
using System.IO;
using System.Collections.Generic;

namespace esin_first_task
{
    public struct Graph
    {
        //variables
        private Dictionary<string, Dictionary<string, int>> _weightedGraph;
        private Dictionary<string, List<int>> _unweightedGraph;
        // продумать момент того, в чём разница между двумя графами
        // понять, как сохранять направленные и не направленные графы
        
        //constructors
        public Graph()
        {
            _weightedGraph = new Dictionary<string, Dictionary<string, int>>();
            _unweightedGraph = new Dictionary<string, List<int>>();
        }

        public Graph(Graph objectGraph)
        {
            _weightedGraph = objectGraph._weightedGraph;
            _unweightedGraph = objectGraph._unweightedGraph;

            // weight graph
            foreach (string key in objectGraph._weightedGraph.Keys)
                _weightedGraph[key] = new Dictionary<string, int>(objectGraph._weightedGraph[key]);
            
            // unweight graph
            foreach (string key in objectGraph._unweightedGraph.Keys)
                _unweightedGraph[key] = new List<int>(objectGraph._unweightedGraph[key]);
        }

        public Graph(string path)
        {
            string[] arrOnFile = File.ReadAllLines(path);
        }
        
        //methods
        
        
        public bool AddVertex()
        {
            string status = _showStatus("add a vertex");
            if(status == "[ERROR]") return false;
            
            string newVertex = "";
            Console.WriteLine(" \u001b[1m$ \u001b[0minput your vertex:");
            Console.Write("  \u001b[1m> \u001b[0m"); newVertex = Console.ReadLine();

            if (status == "--weig")
            {
                if (!_weightedGraph.ContainsKey(newVertex))
                    _weightedGraph.Add(newVertex, new Dictionary<string, int>());
                else
                {
                    Console.WriteLine(" this peak already exists");
                    return false; 
                }
            }
            else
            {
                if (!_unweightedGraph.ContainsKey(newVertex)) 
                    _unweightedGraph.Add(newVertex, new List<int>());
                else
                {
                    Console.WriteLine(" this peak already exists");
                    return false;
                }
            }

            return true; 
        }

        public bool AddEdge()
        {
            string status = _showStatus("add an edge");
            if(status == "[ERROR]") return false;
            int value = 0;
            
            string vertex = "";
            Console.WriteLine(" \u001b[1m$ \u001b[0minput need vertex:");
            Console.Write("  \u001b[1m> \u001b[0m"); vertex = Console.ReadLine();

            string newEdge = "";
            Console.WriteLine(" \u001b[1m$ \u001b[0minput your egde:");
            Console.Write("  \u001b[1m> \u001b[0m"); newEdge = Console.ReadLine();
            
            if (status == "--weig")
            {
                Console.WriteLine(" \u001b[1m$ \u001b[0minput weight for your egde:");
                Console.Write("  \u001b[1m> \u001b[0m"); value = int.Parse(Console.ReadLine());
                try
                {
                    _weightedGraph[vertex].Add(newEdge, value);
                }
                catch
                {
                    Console.WriteLine(" \u001b[1m$ \u001b[0m[ERROR]: no such vertix exists!");
                    return false;  
                }
            }
            else _unweightedGraph[vertex].Add(value);

            return true;
        }
        
        public bool RemoveVertex()
        {
            string status = _showStatus("remove a vertex");
            if (status == "[ERROR]") return false;
            
            string vertex = "";
            Console.WriteLine(" \u001b[1m$ \u001b[0minput need vertex:");
            Console.Write("  \u001b[1m> \u001b[0m"); vertex = Console.ReadLine();
            
            if (status == "--weig")
                _weightedGraph.Remove(vertex);
            else 
                _unweightedGraph.Remove(vertex);

            return true;
        }

        public bool RemoveEdge()
        {
            string status = _showStatus("remove an edge");
            if (status == "[ERROR]") return false;
            
            string vertex = "";
            Console.WriteLine(" \u001b[1m$ \u001b[0minput need vertex:");
            Console.Write("  \u001b[1m> \u001b[0m"); vertex = Console.ReadLine();
            
            string edge = "";
            Console.WriteLine(" \u001b[1m$ \u001b[0minput your egde:");
            Console.Write("  \u001b[1m> \u001b[0m"); edge = Console.ReadLine();

            if (status == "--weig")
            {
                try
                {
                    _weightedGraph[vertex].Remove(edge);
                }
                catch
                {
                    Console.WriteLine(" \u001b[1m$ \u001b[0m[ERROR]: no such vertix exists!");
                    return false;  
                }
            }
            //else
                //_unweightedGraph[vertex].Remove(edge);

            return true; 
        }
        
        public void AddFile(string path) {}
        public bool OutList()
                {
                    string status = _showStatus("show");
                    if(status == "[ERROR]") return false;
                    
                    if(status == "--weig")
                    {
                        foreach (var key in _weightedGraph.Keys)
                        {
                            Console.WriteLine(key + ":");
                        }
                    }
                    else
                    {
                        foreach (var key in _unweightedGraph.Keys)
                        {
                            Console.WriteLine(key + ":");
                            foreach (var value in _unweightedGraph[key])
                                Console.WriteLine("\t" + value);
                        }
                    }
        
                    return true; 
                }
        
        // support methods
        private string _showStatus(string str)
        {
            string depth = "";
            Console.WriteLine($" \u001b[1m$ \u001b[0mwhich graph do you want to {str} to? \n " +
                              "  '--weig' - directed graph | '--unweig' - undirected praph");
            Console.Write("  \u001b[1m> \u001b[0m"); depth = Console.ReadLine();
                                                       
            if (depth != "--weig" && depth != "--unweig")
            {
                depth = "[ERROR]";
                return depth;  
            }
                                           
            return depth; 
        }
    }
}
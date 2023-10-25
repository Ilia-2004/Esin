using System;

namespace AdjacencyList;

class Program
{
  static void Main()
  {
    var objectGraph = new Graph();
    const string pathFile = @"D:\esin\AdjacencyList\test-files\";
    
    Console.WriteLine(" 0. выход");
    Console.WriteLine(" 1. вывод списка");
    Console.WriteLine(" 2. добавить вершину");
    Console.WriteLine(" 3. добавить ребро");
    Console.WriteLine(" 4. удалить вершину");
    Console.WriteLine(" 5. удалить ребро");
    Console.WriteLine(" 6. добавить из файла");
    Console.WriteLine(" 7. добавить в файл");
    Console.WriteLine(" 8. вывод матрицы");
    Console.WriteLine(" 9. построить полный граф");
    Console.WriteLine(" 10. построить дополнительный граф");
    Console.WriteLine(" 11. построить объединённый граф");
    Console.WriteLine(" 12. построить соединённый граф");
    Console.WriteLine(" 13. проверить Ограф");
    Console.WriteLine(" 14. проверить тип графа");
    Console.WriteLine(" 15. алгоритм Крусталя");
    Console.WriteLine();

    while (true)
    {   
      Console.WriteLine("Введите команду"); 
      string com = Console.ReadLine();

      if (com == "1")
      {
        objectGraph.OutputAdjacencyList();
           
        Console.ReadKey();
      }
      else if (com == "2")
      {
        Console.Write("Введите вершину:");
        string vertex = Console.ReadLine();
          
        var addVertex = objectGraph.AddVertex(vertex.ToUpper());
        Console.WriteLine(addVertex
          ? "Ваша вершина успешно добавлена"
          : "Ошибка: эта вершина уже существует");

        Console.ReadKey();
      }
      else if (com == "3")
      {
        string to;
        string from;
        int? weight = null; 
        bool isDirected;

        try
        {
          Console.WriteLine("Откуда?");
          from = Console.ReadLine();
            
          Console.WriteLine("Куда?");
          to = Console.ReadLine();
            
          Console.WriteLine("Вес:");
          string value = Console.ReadLine();
          if (value != "0") weight = Convert.ToInt32(value); 
            
          Console.WriteLine("Взвешенный или нет? (true || false)");
          isDirected = Convert.ToBoolean(Console.ReadLine());
        }
        catch
        {
          Console.WriteLine("Ну это косяк...: некоректный ввод команда...");
          Console.ReadKey();
            
          continue; 
        }
          
        var addEdge = objectGraph.AddEdge(from.ToUpper(), to.ToUpper(), weight, isDirected);
        Console.WriteLine(addEdge
          ? "Ваше ребро успешно добавлена"
          : "Ошибка: эта вершина уже существует");

        Console.ReadKey();
      }
      else if (com == "4")
      {
        Console.WriteLine("Ввдеите вершину:");
        string vertex = Console.ReadLine();
          
        var remVert = objectGraph.RemoveVertex(vertex.ToUpper());
        Console.WriteLine(remVert
          ? "Ваша вершина успешно добавлена"
          : "Ошибка: эта вершина уже существует");

        Console.ReadKey();
      }
      else if (com == "5")
      {
        Console.WriteLine("Откуда?");
        string from = Console.ReadLine();
          
        Console.WriteLine("Куда?");
        string to = Console.ReadLine();
          
        Console.WriteLine("Взвешенность: ");
        bool isDirected = Convert.ToBoolean(Console.ReadLine());
          
        var remEdge = objectGraph.RemoveEdge(from.ToUpper(), to.ToUpper(), isDirected);
        Console.WriteLine(remEdge
          ? "Ваша вершина успешно добавлена"
          : "Ошибка: эта вершина уже существует");

        Console.ReadKey();
      }
      else if (com == "6")
      {
        Console.WriteLine("Введите имя файла:");
        string nameFile = Console.ReadLine();

        var addFromFile = objectGraph.AddFromFile(pathFile + nameFile);
        Console.WriteLine(addFromFile
          ? "Граф успешно добавлен"
          : "Ошибка: эта вершина уже существует");

        Console.ReadKey();
      }
      else if (com == "7")
      {
        Console.WriteLine("Введите имя файла:");
        string nameFile = Console.ReadLine();

        var addInFile = objectGraph.AddInFile(pathFile + nameFile);
        Console.WriteLine(addInFile
          ? "Граф успешно добавлен"
          : "Ошибка: эта вершина уже существует");

        Console.ReadKey();
      }
      else if (com == "8")
      {
        objectGraph.OutputsAdjacencyMatrix();

        Console.ReadKey();
      }
      else if (com == "9")
      {
        var objectMethod = Graph.BuildFullGraph(objectGraph);
        objectMethod.OutputsAdjacencyMatrix();
          
        Console.WriteLine("Полный граф успешно построен! Ёптааа");
        Console.ReadKey();
      }
      else if (com == "10")
      {
        var objectMethod = Graph.BuildComplementGraph(objectGraph);
        objectMethod.OutputsAdjacencyMatrix();
          
        Console.WriteLine("Дополненный граф успешно построен");
        Console.ReadKey();
      }
      else if (com == "11")
      {
        Console.WriteLine("Введите имя файла:");
        string nameFile = Console.ReadLine();
        Graph.BuildCombinedGraph(objectGraph, nameFile);
        //objectMethod.OutputsAdjacencyMatrix();
          
        Console.WriteLine("Объединённый граф успешно построен");
        Console.ReadKey();
      }
      else if (com == "12")
      {
        Console.WriteLine("Введите имя файла:");
        string nameFile = Console.ReadLine();
        Graph.BuildConnectedGraph(objectGraph, nameFile);
        //objectMethod.OutputsAdjacencyMatrix();
          
        Console.WriteLine("Соединённый граф успешно построен");
        Console.ReadKey();
      }
      else if (com == "13")
      {
        var resultMethod =  objectGraph.CheckingForDigraph();
        Console.WriteLine(resultMethod);
        Console.ReadKey();
      }
      else if (com == "14")
      {
        var resultMethod = objectGraph.CheckGraphType();
        Console.WriteLine(resultMethod);
        Console.ReadKey();
      }
      else if (com == "15")
      {
        var resultMethod = Graph.Execute(objectGraph);
        resultMethod.OutputAdjacencyList();
          
        Console.ReadKey();
      }
      else if (com == "0") break;
      else
      {
        Console.WriteLine("Ошибка ёпта!");
        Console.ReadKey();
      }
    }
  }
}

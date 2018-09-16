using System;
using System.Collections.Generic;
using System.IO;

class Solution
{

    static long Visit(int[][] graph, int[] visited, int node)
    {
        if (visited[node] == 0)
        {
            visited[node] = 1;
        }

        long result = 0;
        for (var i = 1; i < graph.Length; i++)
        {
            if (visited[i] == 0 && graph[node][i] > 0)
            {
                visited[i] = graph[node][i];
                result += graph[node][i];
                result += Visit(graph, visited, i);
            }
        }

        return result;
    }

    static long VisitBFS(Dictionary<int, Dictionary<int, int>> graph, int[] visited, int node) {
       
        long result = 0;
        var queue = new Queue<int>();
        queue.Enqueue(node);
        visited[node] = 1;
        while (queue.Count > 0)
        {
            int k = queue.Dequeue();
            var relatedNodes = graph[k].Keys;
            foreach (var related in relatedNodes)
            {
                if (visited[related] == 0)
                {
                    visited[related] = graph[k][related];
                    result += graph[k][related];
                    queue.Enqueue(related);
                }
            }
        }
        return result;
    }
    // Complete the roadsAndLibraries function below.
    static long roadsAndLibraries1(int n, int c_lib, int c_road, int[][] cities)
    {

        int i;
        int n1 = n + 1;
        int[][] graphMatrix = new int[n1][];

        for (i = 0; i < n1; i++)
        {
            graphMatrix[i] = new int[n1];
            for (int j = 0; j < n1; j++)
            {
                graphMatrix[i][j] = 0;
            }
        }

        int[] vertex;
        for (i = 0; i < cities.Length; i++)
        {
            vertex = cities[i];
            var u = vertex[0];
            var v = vertex[1];
            graphMatrix[u][v] = c_road;
            graphMatrix[v][u] = c_road;
        }


        int[] visited = new int[n1];
        for (i = 0; i < n; i++)
        {
            visited[i] = 0;
        }
        long totalLibCost = c_lib * n;
        long totalRoadCost = 0;

        for (i = 1; i < graphMatrix.Length; i++)
        {
            if (visited[i] == 0)
            {
                totalRoadCost += c_lib;
                totalRoadCost += Visit(graphMatrix, visited, i);
            }
        }
        return Math.Min(totalLibCost, totalRoadCost);
    }

    static long roadsAndLibraries2(int n, int c_lib, int c_road, int[][] cities)
    {
        int i;
        int n1 = n + 1;
        Dictionary<int, Dictionary<int, int>> graph = new Dictionary<int, Dictionary<int, int>>();

        for (i = 1; i < n1; i++)
        {
            graph[i] = new Dictionary<int, int>();
        }

        int[] vertex;
        for (i = 0; i < cities.Length; i++)
        {
            vertex = cities[i];
            var u = vertex[0];
            var v = vertex[1];
            graph[u].Add(v, c_road);
            graph[v].Add(u, c_road);
        }


        int[] visited = new int[n1];
        for (i = 0; i < n1; i++)
        {
            visited[i] = 0;
        }
        long totalLibCost = (long) c_lib * (long) n;
        long totalRoadCost = 0;

        for (i = 1; i < n1; i++)
        {
            if (visited[i] == 0)
            {
                totalRoadCost += c_lib;
                totalRoadCost += VisitBFS(graph, visited, i);
            }
        }

        return Math.Min(totalLibCost, totalRoadCost);
    }

    static void Main(string[] args)
    {
        ReadFromFile("input01.txt", "output01.txt");
    }

    static void ReadFromFile(string input, string output)
    {
        using (StreamWriter outputStream = new StreamWriter(output))
        using (StreamReader stream = new StreamReader(File.OpenRead(input)))
        {
            int q = int.Parse(stream.ReadLine());

            for (int i = 0; i < q; i++)
            {
                var splits = stream.ReadLine().Split(" ");
                int n = Convert.ToInt32(splits[0]);
                int m = Convert.ToInt32(splits[1]);
                int c_lib = Convert.ToInt32(splits[2]);
                int c_road = Convert.ToInt32(splits[3]);
                int[][] cities = new int[m][];
                for (int j = 0; j < m; j++)
                {
                    cities[j] = Array.ConvertAll(stream.ReadLine().Split(' '), city => Convert.ToInt32(city));
                }
                long result = roadsAndLibraries2(n, c_lib, c_road, cities);
                outputStream.WriteLine(result);
            }
        }
    }

    static void ReadFromStdIN()
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++)
        {
            string[] nmC_libC_road = Console.ReadLine().Split(' ');

            int n = Convert.ToInt32(nmC_libC_road[0]);

            int m = Convert.ToInt32(nmC_libC_road[1]);

            int c_lib = Convert.ToInt32(nmC_libC_road[2]);

            int c_road = Convert.ToInt32(nmC_libC_road[3]);

            int[][] cities = new int[m][];

            for (int i = 0; i < m; i++)
            {
                cities[i] = Array.ConvertAll(Console.ReadLine().Split(' '), citiesTemp => Convert.ToInt32(citiesTemp));
            }

            long result = roadsAndLibraries2(n, c_lib, c_road, cities);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}

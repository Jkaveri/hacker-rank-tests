namespace JourneyToTheMoon
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    class Solution
    {
        private static long journeyToMoon(int n, int[][] astronaut)
        {
            int i;
            Dictionary<int, Dictionary<int, int>> graph = new Dictionary<int, Dictionary<int, int>>();

            for (i = 0; i < n; i++)
            {
                graph[i] = new Dictionary<int, int>();
            }

            int[] vertex;
            for (i = 0; i < astronaut.Length; i++)
            {
                vertex = astronaut[i];
                var u = vertex[0];
                var v = vertex[1];
                graph[u].Add(v, 1);
                graph[v].Add(u, 1);
            }


            int[] visited = new int[n];
            for (i = 0; i < n; i++)
            {
                visited[i] = -1;
            }

            var numOfContries = 0;
            var nodes = new List<int>();
            for (i = 0; i < n; i++)
            {
                if (visited[i] == -1)
                {
                    var numOfNOdes = VisitBFS(graph, visited, i);
                    nodes.Add(numOfNOdes);
                    numOfContries++;
                }
            }
            long result = 0;
            int a = 0;
            var totalNodes = n;
            for (i = 0; i < numOfContries; i++)
            {
                a = nodes[i];
                result += Math.BigMul(a , (totalNodes - a));
                totalNodes -= a;
            }
            return result;
        }

      
        static int VisitBFS(Dictionary<int, Dictionary<int, int>> graph, int[] visited, int node)
        {

            int numOfNodes = 0;
            var queue = new Queue<int>();
            queue.Enqueue(node);
            visited[node] = node;
            while (queue.Count > 0)
            {
                int k = queue.Dequeue();
                var relatedNodes = graph[k].Keys;
                numOfNodes++;
                foreach (var related in relatedNodes)
                {
                    if (visited[related] == -1)
                    {
                        visited[related] = k;
                        queue.Enqueue(related);
                    }
                }
            }

            return numOfNodes;
        }

        static void Main(string[] args)
        {
            ReadFromFile("input03.txt", "output03.txt");
            // GenerateMaxTests((int)Math.Pow(10, 5), (int)Math.Pow(10, 4), "input03.txt");
        }

        static void ReadFromFile(string input, string output)
        {
            using (StreamWriter outputStream = new StreamWriter(output))
            using (StreamReader stream = new StreamReader(File.OpenRead(input)))
            {
                string[] np = stream.ReadLine().Split(' ');
                int n = int.Parse(np[0]);
                int p = int.Parse(np[1]);

                int[][] astronaut = new int[p][];
                for (int j = 0; j < p; j++)
                {
                    astronaut[j] = Array.ConvertAll(stream.ReadLine().Split(' '), a => Convert.ToInt32(a));
                }
                long result = journeyToMoon(n, astronaut);
                outputStream.WriteLine(result);
            }
        }

        static void GenerateMaxTests(int n, int p, string output)
        {

            using (var stream = new StreamWriter(output))
            {
                var a = n - 1;
                stream.WriteLine($"{n} {p}");
                for (int i = 0; i < p; i++)
                {
                    stream.Write(a);
                    stream.Write(" ");
                    stream.Write(--a);
                    stream.WriteLine("");
                }
            }
        }
    }

}
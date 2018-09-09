namespace GoingToTheOffice.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            string line;
            string[] splits;
            line = Console.ReadLine();
            splits = line.Split(' ');

            var n = int.Parse(splits[0]);
            var m = int.Parse(splits[1]);
            var graph = new Graph2(n);
            int u;
            int v;
            int w;

            for (var i = 0; i < m; i++)
            {
                splits = Console.ReadLine().Split(' ');
                u = int.Parse(splits[0]);
                v = int.Parse(splits[1]);
                w = int.Parse(splits[2]);
                graph.AddNode(u);
                graph.AddNode(v);
                graph.AddEdge(u, v, true, w);
            }
            splits = Console.ReadLine().Split(' ');
            var start = int.Parse(splits[0]);
            var end = int.Parse(splits[1]);
            var q = int.Parse(Console.ReadLine());
            for (int i = 0; i < q; i++)
            {
                splits = Console.ReadLine().Split(' ');
                u = int.Parse(splits[0]);
                v = int.Parse(splits[1]);
                graph.RemoveEdge(u, v, true);
            }

            var passed = graph.DFS();

            Console.WriteLine("Hello World!");

        }
    }

    public class Graph<T>
    {
        public List<T> Nodes { get => Edges.Select(t => t.Key).ToList(); }

        public Dictionary<T, List<T>> Edges { get; } = new Dictionary<T, List<T>>();

        public void AddNode(T node1)
        {
            if (!Edges.ContainsKey(node1))
            {
                Edges.Add(node1, new List<T>());
            }
        }

        public bool IsExistingNode(T node)
        {
            return Edges.ContainsKey(node);
        }

        public void AddEdge(T node1, T node2, bool bidirectional)
        {
            if (Edges.ContainsKey(node1))
            {
                Edges[node1].Add(node2);
            }
            else
            {
                Edges.Add(node1, new List<T> { node2 });
            }

            if (bidirectional)
            {
                if (Edges.ContainsKey(node2))
                {
                    Edges[node2].Add(node1);
                }
                else
                {
                    Edges.Add(node2, new List<T> { node1 });
                }
            }
        }

        public List<T> DFS()
        {
            var passed = new List<T>();

            Visit(Nodes.First(), passed);

            return passed;
        }

        private void Visit(T node, List<T> passed)
        {
            if (passed.Contains(node))
            {
                return;
            }

            passed.Add(node);

            var relations = Edges[node];

            foreach (var w in Nodes)
            {
                if (!relations.Contains(w) && !passed.Contains(node))
                {
                    Visit(w, passed);
                }
            }
        }


    }

    public class Graph2
    {
        public int NumOfNodes { get; }

        public int[][] Edges { get; }

        public Graph2(int nodes)
        {
            NumOfNodes = nodes;
            Edges = new int[nodes][];
            InitEdges(nodes);
        }

        public void AddNode(int node)
        {
            Edges[node][node] = 1;
        }

        public void AddEdge(int node1, int node2, bool bidirectional, int? weight = null)
        {
            var value = 0;
            if (weight == null)
            {
                value = 1;
            }
            else
            {
                value = weight.Value;
            }

            Edges[node1][node2] = value;
            if (bidirectional)
            {
                Edges[node2][node1] = value;
            }
        }

        public void RemoveEdge(int node1, int node2, bool bidirectional)
        {
            Edges[node1][node2] = 0;

            if (bidirectional)
                Edges[node2][node1] = 0;
        }


        /**
         * Deep first search
         */
        public int[] DFS()
        {
            var passed = new int[NumOfNodes];
            for (int v = 0; v < NumOfNodes; v++)
            {
                passed[v] = 0;
            }

            Visit2(0, passed);

            return passed;
        }

        private int[] Visit(int v, int[] passed)
        {
            passed[v] = 1; // true

            for (int w = 0; w < NumOfNodes; w++)
            {
                if (Edges[v][w] > 0 && passed[w] == 0)
                {
                    Visit(w, passed);
                }
            }

            return passed;
        }

        private int[] Visit2(int v, int[] passed)
        {
            var stack = new Stack<int>();
            stack.Push(v);

            while(stack.Count > 0)
            {
                var k = stack.Pop();
                passed[k] = 1;
                for (int w = 0; w < NumOfNodes; w++)
                {
                    if (Edges[k][w] > 0 && passed[w] == 0 && !stack.Contains(w))
                    {
                        stack.Push(w);
                    }
                }
            }

            return passed;
        }

        private void InitEdges(int nodes)
        {
            for (int i = 0; i < nodes; i++)
            {
                Edges[i] = new int[nodes];
                for (int j = 0; j < nodes; j++)
                {
                    Edges[i][j] = 0;
                }
            }
        }

    }

}

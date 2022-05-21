class Program
{
    public static void Main()
    {
        /*
            You can create an object of class Graph
            and do whatever you want to do.
        */
    }
}

public class Graph
{
    public List<List<int>> set = new List<List<int>>();
    public Graph(int setA, int setB)
    {
        try
        {
            if (setA < 0 || setB < 0) throw new IndexOutOfRangeException();
            for (int i = 0; i < setA; i++)
            {
                List<int> list = new List<int>();
                for (int j = 0; j < setB; j++)
                {
                    list.Add(0);
                }
                set.Add(list);
            }

            Console.WriteLine("Graph's been created!", Console.BackgroundColor = ConsoleColor.DarkCyan);
            Console.ResetColor();
        }
        catch(IndexOutOfRangeException)
        {
            Console.WriteLine("The number of vertices of sets cannot be less than zero!");
            Environment.Exit(0);
        }

    }
    public void ShowMatrix()
    {
        //set A means HORIZONTAL elements of matrix
        //set B means VERTICAL   elements of matrix

        if (set.Count > 0)
        {
            foreach (List<int> setA in set)
            {
                foreach (int setB in setA) Console.Write(setB + " ");
                Console.WriteLine();
            }

            for (int i = 0; i < set[0].Count * 2; i++) Console.Write("- ");
            Console.WriteLine();
        }
        else Console.WriteLine("Graph is empty!");
    }
    public void AddVertex(int setNumber)
    {
        if (setNumber == 1)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < set[0].Count; i++)
            {
                list.Add(0);
            }
            set.Add(list);
        }
        else if (setNumber == 2)
        {
            foreach (List<int> setA in set)
            {
                setA.Add(0);
            }
        }
        else Console.WriteLine("Wrong number of set! (1 or 2)");
    }
    public void DeleteVertex(int setNumber, int vertex)
    {
        try
        {
            if (setNumber == 1)
            {
                set.RemoveAt(vertex - 1);
            }
            else if (setNumber == 2)
            {
                foreach (List<int> setA in set)
                {
                    setA.RemoveAt(vertex - 1);
                }
            }
            else Console.WriteLine("Wrong number of set! (1 or 2)");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Wrong index of vertex!");
        }
    }
    public void AddEdge(int from, int to)
    {
        if (from >= 1 && from <= set.Count && to >= 1 && to <= set[from - 1].Count) set[from - 1][to - 1] = 1;
        else Console.WriteLine("Wrong index of edge, edge hasn't been added!");
    }
    public void DeleteEdge(int from, int to)
    {
        if (from >= 1 && from <= set.Count && to >= 1 && to <= set[from - 1].Count) set[from - 1][to - 1] = 0;
        else Console.WriteLine("Wrong index of edge, edge hasn't been added!");
    }
    public List<Vertex> GetDegrees()
    {
        List<Vertex> vertexes = new List<Vertex>();

        //setA checking

        foreach (List<int> setA in set)
        {
            int count = 0;
            foreach (int setB in setA)
            {
                if (setB == 1)
                {
                    count++;
                }
            }
            vertexes.Add(new Vertex("set A[" + (set.IndexOf(setA) + 1).ToString() + "]", count));
        }

        //setB checking

        for (int i = 0; i < set[0].Count; i++)
        {
            int count = 0;
            foreach (List<int> setA in set)
            {
                if (setA[i] == 1)
                {
                    count++;
                }
            }
            vertexes.Add(new Vertex("set B[" + (set.IndexOf(set[i]) + 1).ToString() + "]", count));
        }

        List<Vertex> oVertexes = vertexes.OrderBy(o => o.degree).ToList();

        return oVertexes;
    }

    public void ShowDegrees()
    {
        if (GetDegrees().Count > 0)
        {
            foreach (Graph.Vertex vertex in GetDegrees()) Console.WriteLine(vertex.name + " degree: " + vertex.degree);

            for (int i = 0; i < set[0].Count * 2; i++) Console.Write("- ");
            Console.WriteLine();
        }
    }
    public List<Edge> GetPowerOfEdges()
    {
        List<Edge> edge = new List<Edge>();

        foreach (List<int> setA in set)
        {
            foreach (int setB in setA)
            {
                if (setB == 1)
                {
                    edge.Add(new Edge("Edge(" + (set.IndexOf(setA) + 1).ToString() + ", " + (setA.IndexOf(setB) + 1).ToString() + ")", 2));
                }
            }
        }

        List<Edge> oEdges = edge.OrderBy(o => o.power).ToList();

        return oEdges;
    }
    public void ShowPowerOfEdges()
    {
        if (GetPowerOfEdges().Count > 0)
        {
            Console.WriteLine("Ordered vector edges' power: ");
            foreach (Edge edge in GetPowerOfEdges()) Console.WriteLine(edge.name + " power: " + edge.power);

            for (int i = 0; i < set[0].Count * 2; i++) Console.Write("- ");
            Console.WriteLine();
        }
        else Console.WriteLine("No edges!");
    }

    //-------STRUCTURES
    public struct Edge
    {
        public Edge(string name, int power)
        {
            this.name = name;
            this.power = power;
        }
        public string name;
        public int power;
    }
    public struct Vertex
    {
        public Vertex(string name, int degree)
        {
            this.name = name;
            this.degree = degree;
        }
        public string name;
        public int degree;
    }
}
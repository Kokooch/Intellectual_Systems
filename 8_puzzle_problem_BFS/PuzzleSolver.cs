namespace _8_puzzle_problem_BFS;

public static class PuzzleSolver
{
    private static int[,] _goalState = null!;
    private static int _count = 0;
    
    public static void SolvePuzzle(int[,] startState, int[,] goalState, List<(int, int)> directions)
    {
        Queue<Puzzle> queue = new Queue<Puzzle>();
        HashSet<string> visited = new HashSet<string>();
        _goalState = goalState;

        var (x, y) = FindZero(startState);
        queue.Enqueue(new Puzzle(startState, null, x, y, 0));
        visited.Add(GetStateKey(startState));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (IsGoalState(current.State))
            {
                PrintSolution(current);
                Console.WriteLine($"Number of swaps: {current.Steps}");
                return;
            }

            foreach (var (dx, dy) in directions)
            {
                int newX = current.X + dx;
                int newY = current.Y + dy;

                if (IsValid(newX, newY))
                {
                    int[,] newState = (int[,])current.State.Clone();
                    Swap(newState, current.X, current.Y, newX, newY);
                    string stateKey = GetStateKey(newState);

                    if (!visited.Contains(stateKey))
                    {
                        visited.Add(stateKey);
                        queue.Enqueue(new Puzzle(newState, current, newX, newY, current.Steps + 1));
                    }
                }
            }
        }

        Console.WriteLine("No solution found.");
    }

    public static (int, int) FindZero(int[,] state)
    {
        for (int i = 0; i < state.GetLength(0); i++)
        {
            for (int j = 0; j < state.GetLength(1); j++)
            {
                if (state[i, j] == 0)
                {
                    return (i, j);
                }
            }
        }
        return (-1, -1);
    }

    public static bool IsValid(int x, int y)
    {
        return x >= 0 && x < 3 && y >= 0 && y < 3;
    }

    public static void Swap(int[,] state, int x1, int y1, int x2, int y2)
    {
        int temp = state[x1, y1];
        state[x1, y1] = state[x2, y2];
        state[x2, y2] = temp;
    }

    public static bool IsGoalState(int[,] state)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (state[i, j] != _goalState[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static string GetStateKey(int[,] state)
    {
        string key = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                key += state[i, j] + ",";
            }
        }
        return key;
    }

    public static void PrintSolution(Puzzle puzzle)
    {
        Stack<Puzzle> stack = new Stack<Puzzle>();
        while (puzzle != null)
        {
            stack.Push(puzzle);
            puzzle = puzzle.Parent;
        }

        while (stack.Count > 0)
        {
            var step = stack.Pop();
            PrintState(step.State);
            Console.WriteLine();
        }
    }

    public static void PrintState(int[,] state)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(state[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
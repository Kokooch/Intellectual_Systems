namespace _8_puzzle_problem_A_heuristic;

public static class PuzzleSolver
{
    private static int _steps = 0;
    public static void Solve(int[,] start, int[,] goal)
    {
        _steps = 0;
        var startState = new PuzzleState(start, 0, PuzzleState.CalculateHeuristic(start, goal), null);
        var goalState = new PuzzleState(goal, 0, 0, null);

        var openSet = new SortedSet<PuzzleState>(Comparer<PuzzleState>.Create((a, b) => a.TotalCost == b.TotalCost ? a.Cost.CompareTo(b.Cost) : a.TotalCost.CompareTo(b.TotalCost)));
        openSet.Add(startState);

        var closedSet = new HashSet<PuzzleState>();

        while (openSet.Any())
        {
            var currentState = openSet.Min;
            openSet.Remove(currentState);

            if (IsGoal(currentState.Board, goalState.Board))
            {
                PrintSolution(currentState);
                Console.WriteLine($"Total steps: {_steps}");
                return;
            }

            closedSet.Add(currentState);

            foreach (var successor in currentState.GetSuccessors())
            {
                successor.Heuristic = PuzzleState.CalculateHeuristic(successor.Board, goalState.Board);

                if (closedSet.Any(state => state.Board.Cast<int>().SequenceEqual(successor.Board.Cast<int>())))
                {
                    continue;
                }

                if (!openSet.Any(state => state.Board.Cast<int>().SequenceEqual(successor.Board.Cast<int>())))
                {
                    openSet.Add(successor);
                }
                else
                {
                    var existingState = openSet.First(state => state.Board.Cast<int>().SequenceEqual(successor.Board.Cast<int>()));
                    if (successor.TotalCost < existingState.TotalCost)
                    {
                        openSet.Remove(existingState);
                        openSet.Add(successor);
                    }
                }
            }
        }
    }

    private static bool IsGoal(int[,] board, int[,] goal)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] != goal[i, j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static void PrintSolution(PuzzleState state)
    {
        if (state == null) return;
        _steps++;
        PrintSolution(state.Parent);
        Console.WriteLine("Move:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(state.Board[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    
}
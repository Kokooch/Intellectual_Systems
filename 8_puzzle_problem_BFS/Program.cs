namespace _8_puzzle_problem_BFS;

public class Program
{
    private static readonly int[,] GoalState = {
        {1, 2, 3},
        {4, 5, 6},
        {7, 8, 0}
    };

    private static readonly List<(int, int)> Directions = new List<(int, int)>
    {
        (0, 1), (1, 0), (0, -1), (-1, 0)
    };

    public static void Main()
    {
        int[,] startState = {
            {1, 2, 3},
            {4, 5, 6},
            {0, 7, 8}
        };

        PuzzleSolver.SolvePuzzle(startState, GoalState, Directions);
    }
}
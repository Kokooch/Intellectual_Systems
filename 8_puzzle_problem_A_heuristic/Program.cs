
namespace _8_puzzle_problem_A_heuristic
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[,] start =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 0, 7, 8 }
            };

            int[,] goal =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 }
            };

            PuzzleSolver.Solve(start, goal);
        }
    }
}

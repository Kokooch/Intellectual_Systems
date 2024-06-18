namespace _8_puzzle_problem_BFS;

public class Puzzle(int[,] state, Puzzle parent, int x, int y, int steps)
{
    public int[,] State { get; set; } = state;
    public Puzzle Parent { get; set; } = parent;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;
    public int Steps { get; set; } = steps;
    
    
}
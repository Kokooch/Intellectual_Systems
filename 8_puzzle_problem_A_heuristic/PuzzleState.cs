namespace _8_puzzle_problem_A_heuristic;

public class PuzzleState
{
    public int[,] Board { get; set; }
    public int ZeroRow { get; set; }
    public int ZeroCol { get; set; }
    public int Cost { get; set; }
    public int Heuristic { get; set; }
    public PuzzleState Parent { get; set; }

    public PuzzleState(int[,] board, int cost, int heuristic, PuzzleState parent)
    {
        Board = board;
        Cost = cost;
        Heuristic = heuristic;
        Parent = parent;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Board[i, j] == 0)
                {
                    ZeroRow = i;
                    ZeroCol = j;
                    return;
                }
            }
        }
    }

    public int TotalCost => Cost + Heuristic;

    public IEnumerable<PuzzleState> GetSuccessors()
    {
        var directions = new[] { (0, 1), (1, 0), (0, -1), (-1, 0) };
        foreach (var (dr, dc) in directions)
        {
            int newRow = ZeroRow + dr, newCol = ZeroCol + dc;
            if (newRow >= 0 && newRow < 3 && newCol >= 0 && newCol < 3)
            {
                var newBoard = (int[,])Board.Clone();
                newBoard[ZeroRow, ZeroCol] = newBoard[newRow, newCol];
                newBoard[newRow, newCol] = 0;
                yield return new PuzzleState(newBoard, Cost + 1, 0, this);
            }
        }
    }

    public static int CalculateHeuristic(int[,] board, int[,] goal)
    {
        int heuristic = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int value = board[i, j];
                if (value != 0)
                {
                    int goalRow = (value - 1) / 3;
                    int goalCol = (value - 1) % 3;
                    heuristic += Math.Abs(i - goalRow) + Math.Abs(j - goalCol);
                }
            }
        }
        return heuristic;
    }
}
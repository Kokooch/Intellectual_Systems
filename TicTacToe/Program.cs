using System;
using System.Collections.Generic;

class TicTacToe
{
    static void PrintBoard(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine($"{board[i, 0]} | {board[i, 1]} | {board[i, 2]}");
            if (i < 2)
            {
                Console.WriteLine("---------");
            }
        }
    }

    static bool CheckWinner(char[,] board, char player)
    {
        char[,] winConditions = new char[,]
        {
            { board[0, 0], board[0, 1], board[0, 2] },
            { board[1, 0], board[1, 1], board[1, 2] },
            { board[2, 0], board[2, 1], board[2, 2] },
            { board[0, 0], board[1, 0], board[2, 0] },
            { board[0, 1], board[1, 1], board[2, 1] },
            { board[0, 2], board[1, 2], board[2, 2] },
            { board[0, 0], board[1, 1], board[2, 2] },
            { board[2, 0], board[1, 1], board[0, 2] },
        };

        for (int i = 0; i < 8; i++)
        {
            if (winConditions[i, 0] == player && winConditions[i, 1] == player && winConditions[i, 2] == player)
            {
                return true;
            }
        }
        return false;
    }

    static bool IsFull(char[,] board)
    {
        foreach (var cell in board)
        {
            if (cell == ' ')
            {
                return false;
            }
        }
        return true;
    }

    static List<(int, int)> GetAvailableMoves(char[,] board)
    {
        List<(int, int)> moves = new List<(int, int)>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    moves.Add((i, j));
                }
            }
        }
        return moves;
    }

    static int Minimax(char[,] board, int depth, bool isMaximizing, int alpha, int beta)
    {
        if (CheckWinner(board, 'O'))
        {
            return 1;
        }
        if (CheckWinner(board, 'X'))
        {
            return -1;
        }
        if (IsFull(board))
        {
            return 0;
        }

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            foreach (var move in GetAvailableMoves(board))
            {
                board[move.Item1, move.Item2] = 'O';
                int score = Minimax(board, depth + 1, false, alpha, beta);
                board[move.Item1, move.Item2] = ' ';
                bestScore = Math.Max(score, bestScore);
                alpha = Math.Max(alpha, score);
                if (beta <= alpha)
                {
                    break;
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            foreach (var move in GetAvailableMoves(board))
            {
                board[move.Item1, move.Item2] = 'X';
                int score = Minimax(board, depth + 1, true, alpha, beta);
                board[move.Item1, move.Item2] = ' ';
                bestScore = Math.Min(score, bestScore);
                beta = Math.Min(beta, score);
                if (beta <= alpha)
                {
                    break;
                }
            }
            return bestScore;
        }
    }

    static (int, int)? FindBestMove(char[,] board, bool isMaximizing)
    {
        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        (int, int)? bestMove = null;

        foreach (var move in GetAvailableMoves(board))
        {
            board[move.Item1, move.Item2] = isMaximizing ? 'O' : 'X';
            int score = Minimax(board, 0, !isMaximizing, int.MinValue, int.MaxValue);
            board[move.Item1, move.Item2] = ' ';
            if (isMaximizing ? score > bestScore : score < bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }
        return bestMove;
    }

    static void Main()
    {
        char[,] board = new char[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }

        Console.WriteLine("Welcome to Tic-Tac-Toe! You play as X.");
        PrintBoard(board);

        Console.WriteLine("Enter your move (row and column) as 'row,col': ");
        var input = Console.ReadLine().Split(',');
        int row = int.Parse(input[0]);
        int col = int.Parse(input[1]);
        board[row, col] = 'X';
        PrintBoard(board);

        while (!IsFull(board) && !CheckWinner(board, 'X') && !CheckWinner(board, 'O'))
        {
            if (!IsFull(board))
            {
                var aiMoveO = FindBestMove(board, true);
                if (aiMoveO != null)
                {
                    board[aiMoveO.Value.Item1, aiMoveO.Value.Item2] = 'O';
                    Console.WriteLine("AI plays O:");
                    PrintBoard(board);
                }
            }

            if (!IsFull(board))
            {
                var aiMoveX = FindBestMove(board, false);
                if (aiMoveX != null)
                {
                    board[aiMoveX.Value.Item1, aiMoveX.Value.Item2] = 'X';
                    Console.WriteLine("AI plays X:");
                    PrintBoard(board);
                }
            }
        }

        if (CheckWinner(board, 'X'))
        {
            Console.WriteLine("X wins!");
        }
        else if (CheckWinner(board, 'O'))
        {
            Console.WriteLine("O wins!");
        }
        else
        {
            Console.WriteLine("It's a draw!");
        }
    }
}

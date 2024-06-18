using System;

namespace TicTacToe
{
    class Program
    {
        private static readonly char[,] InitialBoard = {
            { '1', '2', '3' },
            { '4', '5', '6' },
            { '7', '8', '9' }
        };
        
        static char[,] board = {
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Initial Board:");
            PrintBoard(true);

            Console.WriteLine("Enter the initial position for X (1-9): ");
            int initialMove;
            while (!int.TryParse(Console.ReadLine(), out initialMove) || initialMove < 1 || initialMove > 9 || !IsMoveValid(initialMove - 1))
            {
                Console.WriteLine("Invalid move. Please enter a valid position (1-9): ");
            }
            MakeMove(initialMove - 1, 'X');

            while (true)
            {
                PrintBoard();
                if (IsGameOver())
                    break;

                Console.WriteLine("Computer is making a move...");
                ComputerMove();
            }

            PrintBoard();
            char winner = GetWinner();
            if (winner != ' ')
                Console.WriteLine("The winner is: " + winner);
            else
                Console.WriteLine("It's a draw!");
        }

        static void PrintBoard(bool isInitial = false)
        {
            Console.WriteLine("-------------");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 3; j++)
                {
                    if (isInitial)
                    {
                        Console.Write(InitialBoard[i,j] + " | ");
                    }
                    else
                    {
                        Console.Write(board[i, j] + " | ");

                    }
                }
                Console.WriteLine("\n-------------");
            }
        }

        static bool IsMoveValid(int move)
        {
            return board[move / 3, move % 3] == ' ';
        }

        static void MakeMove(int move, char player)
        {
            board[move / 3, move % 3] = player;
        }

        static bool IsGameOver()
        {
            return GetWinner() != ' ' || !HasEmptyCells();
        }

        static bool HasEmptyCells()
        {
            foreach (char cell in board)
            {
                if (cell == ' ')
                    return true;
            }
            return false;
        }

        static char GetWinner()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != ' ')
                    return board[i, 0];
            }

            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != ' ')
                    return board[0, i];
            }

            // Check diagonals
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ' ')
                return board[0, 0];
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ' ')
                return board[0, 2];

            return ' ';
        }

        static void ComputerMove()
        {
            int bestScore = int.MinValue;
            int bestMove = -1;
            char currentPlayer = GetCurrentPlayer();

            for (int i = 0; i < 9; i++)
            {
                if (IsMoveValid(i))
                {
                    MakeMove(i, currentPlayer);
                    int score = Minimax(false, currentPlayer == 'X' ? 'O' : 'X');
                    board[i / 3, i % 3] = ' ';
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }
            MakeMove(bestMove, currentPlayer);
        }

        static char GetCurrentPlayer()
        {
            int xCount = 0, oCount = 0;
            foreach (char cell in board)
            {
                if (cell == 'X') xCount++;
                if (cell == 'O') oCount++;
            }
            return xCount > oCount ? 'O' : 'X';
        }

        static int Minimax(bool isMaximizing, char currentPlayer)
        {
            char winner = GetWinner();
            if (winner == 'X') return -10;
            if (winner == 'O') return 10;
            if (!HasEmptyCells()) return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (IsMoveValid(i))
                    {
                        MakeMove(i, currentPlayer);
                        int score = Minimax(false, currentPlayer == 'X' ? 'O' : 'X');
                        board[i / 3, i % 3] = ' ';
                        bestScore = Math.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 9; i++)
                {
                    if (IsMoveValid(i))
                    {
                        MakeMove(i, currentPlayer == 'X' ? 'O' : 'X');
                        int score = Minimax(true, currentPlayer);
                        board[i / 3, i % 3] = ' ';
                        bestScore = Math.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }
    }
}

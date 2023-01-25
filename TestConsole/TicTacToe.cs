using System;

namespace TestConsole
{
    public class TicTacToe
    {
        public GameBoard Board;

        public TicTacToe()
        {
        }
        public void Start()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("WELCOME TO MY TIC TAC TOE DIMENSION GAME :)");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Choose a dimension between 2 and 5:");

            while (true)
            {
                var playerInput = ParsingHelper.ParsingNumber();

                if (playerInput < 2 || playerInput > 5)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("This dimension does not exist.");
                    Console.ResetColor();
                    continue;
                }

                Board = new GameBoard(playerInput);

                Console.WriteLine();
                Console.WriteLine("Player 1: X");
                Console.WriteLine("Player 2: O");
                Console.WriteLine();
                Console.WriteLine("Press any key to begin:");
                Console.ReadLine();
                Run();
            }
        }

        public void Run()
        {
            Board.PrintGameBoard();

            while (true)
            {
                NextTurn();
                if (WinCheck())
                {
                    Board.SwitchPlayer();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{Board.ActualPlayer} win!");
                    break;
                }
                if (DrawCheck())
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"It's a draw...");
                    break;
                }
            }

            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("1: The same dimension.\n" +
                              "2: Choose a new dimension.\n" +
                              "Any other key: Exit game.");
            string inputUser = Console.ReadLine();

            if (inputUser == "1")
            {
                Board = new GameBoard(Board.Dimension);
                Run();
            }
            else if (inputUser == "2")
            {
                Start();
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Thanks for playing :)");

            Environment.Exit(0);
        }

        public void NextTurn()
        {
            Console.WriteLine($"{Board.ActualPlayer}: Select a number on the game board.");

            while (true)
            {
                var playerInput = ParsingHelper.ParsingNumber();

                if (playerInput < 1 || playerInput > Board.Dimension*Board.Dimension)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("This field number does not exist.");
                    Console.ResetColor();
                    continue;
                }

                var boardIndex = ConvertPlayerInputToArrayCoords(playerInput);

                if (Board.Blocks[boardIndex.Item1, boardIndex.Item2].Player == Player.None)
                {
                    Board.Blocks[boardIndex.Item1, boardIndex.Item2].Player = Board.ActualPlayer;
                    Board.SwitchPlayer();
                    break;
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Field is already taken.");
                Console.ResetColor();
            }

            Board.PrintGameBoard();
        }

        private Tuple<int, int> ConvertPlayerInputToArrayCoords(int playerInput)
        {
            for (int i = 0; i < Board.Blocks.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Blocks.GetLength(0); j++)
                {
                    if (playerInput == Board.Blocks[i, j].BlockNumber)
                    {
                        return new Tuple<int, int>(i, j);
                    }
                }
            }

            throw new ArgumentException("Should never happen....");
        }

        public bool WinCheck()
        {
            if (CheckRows() || CheckColumns() || CheckDiagonal() || CheckAntiDiagonal())
            {
                return true;
            }

            return false;
        }

        public bool CheckRows()
        {
            for (int i = 0; i < Board.Blocks.GetLength(0); i++)
            {
                int countForWinRow = 0;

                for (int j = 0; j < Board.Blocks.GetLength(0); j++)
                {
                    if (Board.Blocks[i, j].Player != Board.ActualPlayer && Board.Blocks[i, j].Player != Player.None)
                    {
                        countForWinRow++;

                        if (countForWinRow == Board.Blocks.GetLength(0))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool CheckColumns()
        {
            for (int j = 0; j < Board.Blocks.GetLength(1); j++)
            {
                int countForWinCol = 0;

                for (int i = 0; i < Board.Blocks.GetLength(1); i++)
                {
                    if (Board.Blocks[i, j].Player != Board.ActualPlayer && Board.Blocks[i, j].Player != Player.None)
                    {
                        countForWinCol++;

                        if (countForWinCol == Board.Blocks.GetLength(1))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool CheckDiagonal()
        {
            int countForWinDia = 0;

            for (int i = 0; i < Board.Blocks.GetLength(1); i++)
            {
                if (Board.Blocks[i, i].Player != Board.ActualPlayer && Board.Blocks[i, i].Player != Player.None)
                {
                    countForWinDia++;

                    if (countForWinDia == Board.Blocks.GetLength(1))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckAntiDiagonal()
        {
            int countForWinAntiDia = 0;

            for (int i = 0; i < Board.Blocks.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Blocks.GetLength(0); j++)
                {
                    if (i + j == Board.Dimension -1 && Board.Blocks[i, j].Player != Board.ActualPlayer && Board.Blocks[i, j].Player != Player.None)
                    {
                        countForWinAntiDia++;

                        if (countForWinAntiDia == Board.Blocks.GetLength(0))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool DrawCheck()
        {
            int countForDraw = 0;

            for (int j = 0; j < Board.Blocks.GetLength(1); j++)
            {
                for (int i = 0; i < Board.Blocks.GetLength(1); i++)
                {
                    if (Board.Blocks[j, i].Player == Player.None)
                    {
                        countForDraw++;
                    }
                }
            }

            if (countForDraw == 0)
            {
                return true;
            }

            return false;
        }
    }
}
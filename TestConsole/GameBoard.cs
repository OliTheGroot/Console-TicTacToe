using System;

namespace TestConsole
{
    public class GameBoard
    {
        public Block[,] Blocks;
        public readonly int Dimension;
        public Player ActualPlayer { get; set; }

        public GameBoard(int dimension)
        {
            Dimension = dimension;

            ActualPlayer = Player.Player1;

            Blocks = new Block[Dimension, Dimension];

            int countBlocknumber = 1;

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    Blocks[i, j] = new Block(countBlocknumber);
                    countBlocknumber++;
                }
            }
        }

        public void PrintGameBoard()
        {
            Console.Clear();

            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string subLine = "";

                    for (int k = 0; k < Dimension; k++)
                    {
                        subLine += Blocks[i, k].ToStringArray()[j];
                    }

                    Console.WriteLine(subLine);
                }
            }
        }

        public void SwitchPlayer()
        {
            if (ActualPlayer == Player.Player1)
            {
                ActualPlayer = Player.Player2;
            }
            else if (ActualPlayer == Player.Player2)
            {
                ActualPlayer = Player.Player1;
            }
            else if (ActualPlayer == Player.None)
            {
                throw new ArgumentException("Actual player cannot be null!");
            }
        }
    }
}
using System;

namespace TestConsole
{
    public class Block
    {
        public Player Player { get; set; }
        public int BlockNumber;

        public Block(int blockNumber)
        {
            Player = Player.None;
            BlockNumber = blockNumber;
        }

        public string PlayerSymbol
        {
            get
            {
                switch (Player)
                {
                    case Player.None:
                        return " ";

                    case Player.Player1:
                        return "X";

                    case Player.Player2:
                        return "O";

                    default:
                        throw new ArgumentException("The player type was not defined!");
                }
            }
        }

        public string[] ToStringArray()
        {
            string[] strings = new string[3];

            strings[0] = "       |";
            strings[1] = $"   {PlayerSymbol}   |";
            strings[2] = $"______{BlockNumber}|";

            if (BlockNumber > 9)
            {
                strings[2] = $"_____{BlockNumber}|";
            }

            return strings;
        }
    }
}
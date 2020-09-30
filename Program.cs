using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class GameBoard
    {
        public static string[] _initialBoardState;
        public static string[] _boardPositionalMarkers;
        public string[] _boardLayout;

    }

    class DisplayBoard
    {
        public string[] _boardLayout;
    }

    class Program
    {
        //
        // ..=........................MAIN.........................
        //
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            GameBoard playerBoard;
            DisplayBoard playerDisplay;

            playerBoard = InitialiseGameBoard(); //Initialising gameboard state
            InitialisePositionalMarkers();

            PrintGameBoard(playerBoard); //Displaying current board state.

            PlaceTokens(playerBoard);

            


        }
        // ...........................MAIN.........................

        static void PlaceTokens(GameBoard inGameBoard)
        {
            for (int i = 0; i < 4; i++)
            {
                string inputCoord, checkedCoord;
                bool isValid = false;
                Console.Write("\nEnter grid co-ordinates to place token: ");
                inputCoord = Console.ReadLine().ToUpper();
                for (int x = 0; x < GameBoard._boardPositionalMarkers.Length; x++)
                {
                    checkedCoord = GameBoard._boardPositionalMarkers[x];
                    isValid = (string.Equals(checkedCoord, inputCoord));
                    if (isValid == true)
                    {
                        inGameBoard._boardLayout[x] = "V";
                    }
                }
                PrintGameBoard(inGameBoard);
            }
        }

        static void PrintGameBoard(GameBoard boardToDisplay)
        {
            string tmp = "";
            for (int x = 0; x < 5; x++)
            {
                tmp = "";
                for(int i = 0 + (x * 5); i < 5 + (x * 5); i++)
                {
                    tmp += " "+boardToDisplay._boardLayout[i];
                }
                Console.WriteLine(tmp);
            }
            ////////////
        }

        static GameBoard InitialiseGameBoard()
        {
            GameBoard gameBoards = new GameBoard();
            //Game board initialised to 5x5 grid
            GameBoard._initialBoardState = new string[25];
            for (int x = 0; x < 25; x++)
            {
                GameBoard._initialBoardState[x] = "*";
            }
            gameBoards._boardLayout = GameBoard._initialBoardState;
            

            
                return gameBoards;
            /////////////////////
        }
        static void InitialisePositionalMarkers()
        {
            GameBoard._boardPositionalMarkers = new string[25];
            char rank = 'A';
            char file = '1';
            string position = "";
            for (int x = 0; x < 5; x++)
            {
                file = '1';
                for (int i = 0 + (x * 5); i < 5 + (x * 5); i++)
                {
                    position = "";
                    position += char.ToString(rank);
                    position += char.ToString(file);
                    GameBoard._boardPositionalMarkers[i] = position;
                    ++file;
                }
                ++rank;
            }
            ///////////////////
        }


    }
}

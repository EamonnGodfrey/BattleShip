using System;

namespace BattleShip
{
    class Board
    {
        public int gridSize;


    }
    class GameBoard
    {
        public static string[,] _initialBoardState;
        public static string[,] _boardPositionalMarkers;
        public string[,] _boardLayout;

    }

    class DisplayBoard
    {

    }

    class Program
    {
        //
        // ..=........................MAIN.........................
        //
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            GameBoard playerBoard, playerDisplayBoard, vsBoard, vsDisplayBoard;
            

            int gridSize = 5;
            gridSize += 1;
            //DisplayBoard playerDisplay;

            playerBoard = InitialisePlayerBoard(gridSize);              //Initialising all gameboard states
            playerDisplayBoard = InitialisePlayerBoard(gridSize);
            vsBoard = InitialisePlayerBoard(gridSize);
            vsDisplayBoard = InitialisePlayerBoard(gridSize);

            
            InitialisePositionalMarkers(gridSize);                    //InitialisePositionalMarkers();
            
            PrintPlayerBoard(playerBoard, gridSize);                //Displaying current board state.
            PrintPlayerBoard(playerDisplayBoard, gridSize);
            
            PlaceTokens(playerBoard, gridSize);




        }
        // ...........................MAIN.........................

        static void PlaceTokens(GameBoard inGameBoard, int inputGridSize)
        {
            for (int turn = 0; turn < 4; turn++)
            {
                string inputCoord, checkedCoord;
                bool isValid = false;
                Console.Write("\nEnter grid co-ordinates to place token: ");
                inputCoord = Console.ReadLine().ToUpper();
                for (int y = 0; y < inputGridSize; y++)
                {
                    for (int x = 0; x < inputGridSize; x++)
                    {
                        checkedCoord = GameBoard._boardPositionalMarkers[y, x];
                        if (inputCoord == "0")
                        {
                            break;
                        }
                        isValid = (string.Equals(checkedCoord, inputCoord));
                        if (isValid == true)
                        {
                            if (inGameBoard._boardLayout[y, x] != "V")
                            {
                                inGameBoard._boardLayout[y, x] = "V";
                                break;
                            }
                            else
                            { 
                                isValid = false;
                            }
                        }
                    }
                    if (isValid == true)
                    {
                        break;
                    }
                }
                while (isValid == false)
                {
                    Console.WriteLine("\nError\nPlease enter a valid co-ordinate: ");
                    inputCoord = Console.ReadLine().ToUpper();
                    for (int y = 0; y < inputGridSize; y++)
                    {
                        for (int x = 0; x < inputGridSize; x++)
                        {
                            checkedCoord = GameBoard._boardPositionalMarkers[y, x];
                            if (inputCoord == "0")
                            {
                                break;
                            }
                            isValid = (string.Equals(checkedCoord, inputCoord));
                            if (isValid == true)
                            {
                                if (inGameBoard._boardLayout[y, x] != "V")
                                {
                                    inGameBoard._boardLayout[y, x] = "V";
                                    break;
                                }
                                else
                                {
                                    isValid = false;
                                }
                            }
                        }
                        if (isValid == true)
                        {
                            break;
                        }
                    }
                }
                PrintPlayerBoard(inGameBoard, inputGridSize);
            }
        }

        static void PrintPlayerBoard(GameBoard boardToDisplay, int inputGridSize)
        {
            string tmp = "";
            for (int y = 0; y < inputGridSize; y++)
            {
                tmp = "";
                for (int x = 0; x < inputGridSize; x++)
                {
                    tmp += $" {boardToDisplay._boardLayout[y, x],2}";
                }
                Console.WriteLine(tmp);
            }
            Console.WriteLine();
        }

        static GameBoard InitialisePlayerBoard(int inputGridSize)
        {
            GameBoard playerBoard = new GameBoard();
            GameBoard._initialBoardState = new string[inputGridSize, inputGridSize];

            char rank = 'A';

            for (int y = 0; y < inputGridSize; y++)
            {
                for (int x = 0; x < inputGridSize; x++)
                {
                    if (y == 0 && x == 0)
                    {
                        GameBoard._initialBoardState[y, x] = " ";
                    }
                    else if (y == 0 && x != 0)
                    {
                        GameBoard._initialBoardState[y, x] = Convert.ToString(x);
                    }
                    else if (y != 0 && x == 0)
                    {
                        GameBoard._initialBoardState[y, x] = char.ToString(rank);
                        ++rank;
                    }
                    else
                    {
                        GameBoard._initialBoardState[y, x] = "*";
                    }
                }
            }
            playerBoard._boardLayout = GameBoard._initialBoardState;
            return playerBoard;
        }
        static void InitialisePositionalMarkers(int inputGridSize)
        {
            GameBoard._boardPositionalMarkers = new string[inputGridSize, inputGridSize];
            char rank = '@';
            string position = "";

            for (int y = 0; y < inputGridSize; y++)
            {
                for (int x = 0; x < inputGridSize; x++)
                {
                    if (y == 0 && x == 0 || y == 0 && x != 0 || y != 0 && x == 0)
                    {
                        GameBoard._boardPositionalMarkers[y, x] = "0";
                    }
                    else
                    {
                        position = "";
                        position += char.ToString(rank);
                        position += Convert.ToString(x);
                        GameBoard._boardPositionalMarkers[y, x] = position;
                    }
                   
                }
                ++rank;
            }
        }
    }
}

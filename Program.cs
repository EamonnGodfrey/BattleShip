using System;

namespace BattleShip
{
    class Program
    {
        //
        // ..=........................MAIN.........................
        //
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            GameBoard playerBoard, playerDisplayBoard, vsBoard, vsDisplayBoard;
            

            int gridSize = 10;
            //Console.Write("EnterPlayerGridSize: ");
            //int.TryParse(Console.ReadLine(), out gridSize);   Player Controlled Grid size
            gridSize += 1;

            Board._gridSize = gridSize;
            
            playerBoard = InitialisePlayerBoard();              //Initialising all gameboard states
            playerDisplayBoard = InitialisePlayerBoard();
            vsBoard = InitialisePlayerBoard();
            vsDisplayBoard = InitialisePlayerBoard();

            InitialisePositionalMarkers();                    //InitialisePositionalMarkers();
            
            PrintPlayerBoard(playerBoard);                //Displaying current board state.
            PrintPlayerBoard(playerDisplayBoard);
            
            PlaceTokens(playerBoard);




        }
        // ...........................MAIN.........................

        static void PlaceTokens(GameBoard inGameBoard)
        {
            for (int turn = 0; turn < 4; turn++)
            {
                string inputCoord, checkedCoord;
                bool isValid = false;
                Console.Write("\nEnter grid co-ordinates to place token: ");
                inputCoord = Console.ReadLine().ToUpper();
                for (int y = 0; y < Board._gridSize; y++)
                {
                    for (int x = 0; x < Board._gridSize; x++)
                    {
                        checkedCoord = Board._boardPositionalMarkers[y, x];
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
                    for (int y = 0; y < Board._gridSize; y++)
                    {
                        for (int x = 0; x < Board._gridSize; x++)
                        {
                            checkedCoord = Board._boardPositionalMarkers[y, x];
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
                PrintPlayerBoard(inGameBoard);
            }
        }
        // // // // // // // // // // // // // // // // // // // // // // // //
        static void PrintPlayerBoard(GameBoard boardToDisplay)
        {
            string tmp = "";
            for (int y = 0; y < Board._gridSize; y++)
            {
                tmp = "";
                for (int x = 0; x < Board._gridSize; x++)
                {
                    tmp += $" {boardToDisplay._boardLayout[y, x],2}";
                }
                Console.WriteLine(tmp);
            }
            Console.WriteLine();
        }
        // // // // // // // // // // // // // // // // // // // // // // // //
        static GameBoard InitialisePlayerBoard()
        {
            GameBoard playerBoard = new GameBoard();
            Board._initialBoardState = new string[Board._gridSize, Board._gridSize];

            char rank = 'A';

            for (int y = 0; y < Board._gridSize; y++)
            {
                for (int x = 0; x < Board._gridSize; x++)
                {
                    if (y == 0 && x == 0)
                    {
                        Board._initialBoardState[y, x] = " ";
                    }
                    else if (y == 0 && x != 0)
                    {
                        Board._initialBoardState[y, x] = Convert.ToString(x);
                    }
                    else if (y != 0 && x == 0)
                    {
                        Board._initialBoardState[y, x] = char.ToString(rank);
                        ++rank;
                    }
                    else
                    {
                        Board._initialBoardState[y, x] = "*";
                    }
                }
            }
            playerBoard._boardLayout = Board._initialBoardState;
            return playerBoard;
        }
        // // // // // // // // // // // // // // // // // // // // // // // //
        static void InitialisePositionalMarkers()
        {
            Board._boardPositionalMarkers = new string[Board._gridSize, Board._gridSize];
            char rank = '@';
            string position = "";

            for (int y = 0; y < Board._gridSize; y++)
            {
                for (int x = 0; x < Board._gridSize; x++)
                {
                    if (y == 0 && x == 0 || y == 0 && x != 0 || y != 0 && x == 0)
                    {
                        Board._boardPositionalMarkers[y, x] = "0";
                    }
                    else
                    {
                        position = "";
                        position += char.ToString(rank);
                        position += Convert.ToString(x);
                        Board._boardPositionalMarkers[y, x] = position;
                    }
                   
                }
                ++rank;
            }
        }
    }
}

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
            

            int gridSize = 5;
            //Console.Write("Enter the size of the grid you wish to play on (size 10 is 'standard'): ");
            //Board.GridSize = Convert.ToInt32(Console.ReadLine());   //Player Controlled Grid Size. Passed to Board.GridSize
            
            
            Board.GridSize = gridSize;

            //Board._gridSize = gridSize;
            
            playerBoard = InitialisePlayerBoard();              //Initialising all gameboard states
            playerDisplayBoard = InitialisePlayerBoard();
            vsBoard = InitialisePlayerBoard();
            vsDisplayBoard = InitialisePlayerBoard();

            InitialisePositionalMarkers();                    //InitialisePositionalMarkers();
            
            PrintPlayerBoards(playerDisplayBoard, playerBoard);
            
            PlaceTokens(playerBoard);
            Console.Clear();

            PrintPlayerBoards(playerDisplayBoard, playerBoard);
            FireSalvo(playerDisplayBoard, playerBoard);
            FireSalvo(playerDisplayBoard, playerBoard);
            FireSalvo(playerDisplayBoard, playerBoard);




        }
        // ...........................MAIN.........................

        static void FireSalvo(GameBoard displayBoard, GameBoard playerBoard)
        {
            int[] gridReference = new int[2];
            bool loop = true;
            while (loop) 
            { 
                Console.Write("Please enter firing coordinates: ");
                gridReference = CoordinateGetter(Console.ReadLine());
                if (displayBoard._boardLayout[gridReference[0], gridReference[1]] != "X")
                {
                    if (playerBoard._boardLayout[gridReference[0], gridReference[1]] == "V")
                    {
                        displayBoard._boardLayout[gridReference[0], gridReference[1]] = "X";
                        playerBoard._boardLayout[gridReference[0], gridReference[1]] = "X";
                        PrintPlayerBoards(displayBoard, playerBoard);
                        Console.WriteLine("HIT");
                        loop = false;
                    }
                    else
                    {
                        displayBoard._boardLayout[gridReference[0], gridReference[1]] = "O";
                        PrintPlayerBoards(displayBoard, playerBoard);
                        Console.WriteLine("SPLASH");
                        loop = false;
                    }
                }
                else
                {
                    Console.WriteLine("\nAlready fired on co-ordinate");
                }
            }
        }

        static int[] CoordinateGetter(string inputCoord)
        {
            int[] coordinates = new int[2];
            bool isValid = false;
            string checkedCoord;
            inputCoord = inputCoord.ToUpper();
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
                        coordinates[0] = y;
                        coordinates[1] = x;
                        break;
                    }
                }
                if (isValid == true)
                {
                    break;
                }
            }
            while (isValid == false)
            {
                Console.Write("Error\nPlease enter a valid grid co-ordinate: ");
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
                            coordinates[0] = y;
                            coordinates[1] = x;
                            break;
                        }
                    }
                    if (isValid == true)
                    {
                        break;
                    }
                }
            }
            return coordinates;
        }

        static void PlaceTokens(GameBoard inGameBoard)
        {
            for (int turn = 0; turn < 4; turn++)
            {
                bool loop = true;
                while (loop)
                {
                    int[] gridReference = new int[2];
                    Console.Write("\nEnter grid co-ordinate to place token: ");
                    gridReference = CoordinateGetter(Console.ReadLine());
                    if (inGameBoard._boardLayout[gridReference[0], gridReference[1]] != "V")
                    {
                        inGameBoard._boardLayout[gridReference[0], gridReference[1]] = "V";
                        PrintSingleBoard(inGameBoard);
                        loop = false;
                    }
                    else
                    {
                        Console.WriteLine("\nToken already placed at co-ordinate");
                    }
                }
            }
        }
        
        // // // // // // // // // // // // // // // // // // // // // // // //
        static void PrintSingleBoard(GameBoard boardToDisplay)
        {
            Console.WriteLine();
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

        static void PrintPlayerBoards(GameBoard displayBoard, GameBoard playerBoard)
        {
            PrintSingleBoard(displayBoard);
            string tmp = "";
            for (int x = 0; x < (Board.GridSize * 3)+1; x++)
            {
                tmp += "-";
            }
            Console.WriteLine(tmp);
            PrintSingleBoard(playerBoard);
        }
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

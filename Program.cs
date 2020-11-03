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
            GameBoard playerOneBoard, playerOneDisplayBoard, playerTwoBoard, playerTwoDisplayBoard;
            Battleships[] pOneShips = new Battleships[5];
            Battleships[] pTwoShips = new Battleships[5];
            bool gameOver = false;


            InitialiseGame();                                        //Splash screen and grid size initialisation
            InitialiseShips(pOneShips);                             //Initialising ship lists
            InitialiseShips(pTwoShips);
            playerOneBoard = InitialisePlayerBoard();             //Initialising all gameboard states
            playerOneDisplayBoard = InitialisePlayerBoard();
            playerTwoBoard = InitialisePlayerBoard();
            playerTwoDisplayBoard = InitialisePlayerBoard();
            PrintPlayerBoards(playerOneDisplayBoard, playerOneBoard);   //Initial board display

            // // // // // // // // // // // // // // //

            PlaceTokens(playerOneBoard, pOneShips);

            
            bool loop = false;
            while (loop == false)
            {
                FireSalvo(playerOneDisplayBoard, playerOneBoard, pOneShips);
                loop = WinCondition(pOneShips);
            }



            Console.WriteLine("GAME WON");
            Console.ReadLine();


        }
        // ..........................^^MAIN^^........................

        static void InitialiseShips(Battleships[] shipListIN)
        {
            for (int x = 0; x < shipListIN.Length; x++)     //Used to instantiate the Battleships objects
            {
                switch (x)      //Battleship list is always Carrier, Cruiser, Destroyer, Submarine, Patrol. Therefore switch is used to instantiate the different battleship types
                {
                    case 0:
                        shipListIN[x] = new Battleships();
                        shipListIN[x]._columnPosition = new int[5];
                        shipListIN[x]._rowPosition = new int[5];
                        shipListIN[x]._id = "Carrier";
                        shipListIN[x]._shipLength = 5;
                        shipListIN[x]._hitsTaken = 0;
                        shipListIN[x]._shipActive = true;
                        break;
                    case 1:
                        shipListIN[x] = new Battleships();
                        shipListIN[x]._columnPosition = new int[4];
                        shipListIN[x]._rowPosition = new int[4];
                        shipListIN[x]._id = "Cruiser";
                        shipListIN[x]._shipLength = 4;
                        shipListIN[x]._hitsTaken = 0;
                        shipListIN[x]._shipActive = true;
                        break;
                    case 2:
                        shipListIN[x] = new Battleships();
                        shipListIN[x]._columnPosition = new int[3];
                        shipListIN[x]._rowPosition = new int[3];
                        shipListIN[x]._id = "Destroyer";
                        shipListIN[x]._shipLength = 3;
                        shipListIN[x]._hitsTaken = 0;
                        shipListIN[x]._shipActive = true;
                        break;
                    case 3:
                        shipListIN[x] = new Battleships();
                        shipListIN[x]._columnPosition = new int[3];
                        shipListIN[x]._rowPosition = new int[3];
                        shipListIN[x]._id = "Submarine";
                        shipListIN[x]._shipLength = 3;
                        shipListIN[x]._hitsTaken = 0;
                        shipListIN[x]._shipActive = true;
                        break;
                    case 4:
                        shipListIN[x] = new Battleships();
                        shipListIN[x]._columnPosition = new int[2];
                        shipListIN[x]._rowPosition = new int[2];
                        shipListIN[x]._id = "Patrol Boat";
                        shipListIN[x]._shipLength = 2;
                        shipListIN[x]._hitsTaken = 0;
                        shipListIN[x]._shipActive = true;
                        break;
                    default:
                        break;

                }
            }
        }

        static bool WinCondition(Battleships[] inShipList)
        {
            bool isGameOver = false;
            int activeShips = inShipList.Length;

            for (int y = 0; y < inShipList.Length; y++)
            {
                if (inShipList[y]._shipActive == false)
                {
                    --activeShips;
                }
            }
            if (activeShips == 0)
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        static void FireSalvo(GameBoard displayBoard, GameBoard playerBoard, Battleships[] inPlayerShips)
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
                        for (int y = 0; y < inPlayerShips.Length; y++)
                        {
                            for (int x = 0; x < inPlayerShips[y]._shipLength; x++)
                            {
                                if (inPlayerShips[y]._rowPosition[x] == gridReference[0] && inPlayerShips[y]._columnPosition[x] == gridReference[1])
                                {
                                    ++inPlayerShips[y]._hitsTaken;
                                    if (inPlayerShips[y]._hitsTaken == inPlayerShips[y]._shipLength)
                                    {
                                        Console.WriteLine("||     Battleship Sunk     ||");
                                        inPlayerShips[y]._shipActive = false;
                                    }
                                    break;
                                }
                            }
                        }

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

        static void PlaceTokens(GameBoard inGameBoard, Battleships[] inPlayerShips)
        {
            for (int ship = 0; ship < inPlayerShips.Length; ship++)
            {
                int[] gridReference = new int[2];
                bool shipPlaced = false;
                Console.WriteLine("BOARD SETUP");

                while (shipPlaced == false)
                {
                    Console.WriteLine("Please select an initial co-ord, then a direction for it to extend towards.\n");
                    Console.WriteLine($"Ship Name: {inPlayerShips[ship]._id} | Ship Length: {inPlayerShips[ship]._shipLength}");
                    Console.Write("Select initial coordinate: ");
                    gridReference = CoordinateGetter(Console.ReadLine());


                    Console.WriteLine("'UP', 'DOWN', 'LEFT', 'RIGHT'");
                    Console.Write("Enter direction to place ship: ");
                    switch (Console.ReadLine().ToUpper())
                    {
                        case "RIGHT":
                            if (gridReference[1] + inPlayerShips[ship]._shipLength <= inGameBoard._boardLayout.GetLength(1))    //Check to see that ship can be placed along 
                            {                                                                                                   //the lateral length of the array
                                bool isValid = false;
                                for (int x = gridReference[1]; x < gridReference[1] + inPlayerShips[ship]._shipLength; x++)
                                {
                                    if (inGameBoard._boardLayout[gridReference[0], x] != "V")
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        isValid = false;                                                                                                                    
                                        break;
                                    }
                                }
                                if (isValid)
                                {
                                    for (int x = gridReference[1]; x < gridReference[1] + inPlayerShips[ship]._shipLength; x++)
                                    {
                                        inGameBoard._boardLayout[gridReference[0], x] = "V";
                                        inPlayerShips[ship].RowPosition[x - gridReference[1]] = gridReference[0];          //Two lines of code trying to assign grid co-ords. Not working currently. YOU ARE HERE
                                        inPlayerShips[ship].ColumnPosition[x - gridReference[1]] = x;
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;
                        case "LEFT":
                            if (gridReference[1] - inPlayerShips[ship]._shipLength >= 0)
                            {
                                bool isValid = false;
                                for (int x = gridReference[1]; x > gridReference[1] - inPlayerShips[ship]._shipLength; x--)
                                {
                                    if (inGameBoard._boardLayout[gridReference[0], x] != "V")
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }
                                if (isValid)
                                {
                                    for (int x = gridReference[1]; x > gridReference[1] - inPlayerShips[ship]._shipLength; x--)
                                    {
                                        inGameBoard._boardLayout[gridReference[0], x] = "V";
                                        inPlayerShips[ship].RowPosition[gridReference[1] - x] = gridReference[0];
                                        inPlayerShips[ship].ColumnPosition[gridReference[1] - x] = x;
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;
                        case "DOWN":
                            if (gridReference[0] + inPlayerShips[ship]._shipLength <= inGameBoard._boardLayout.GetLength(0))
                            {
                                bool isValid = false;
                                for (int x = gridReference[0]; x < gridReference[0] + inPlayerShips[ship]._shipLength; x++)
                                {
                                    if (inGameBoard._boardLayout[x, gridReference[1]] != "V")
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }
                                if (isValid)
                                {
                                    for (int x = gridReference[0]; x < gridReference[0] + inPlayerShips[ship]._shipLength; x++)
                                    {
                                        inGameBoard._boardLayout[x, gridReference[1]] = "V";
                                        inPlayerShips[ship].RowPosition[x - gridReference[0]] = x;
                                        inPlayerShips[ship].ColumnPosition[x - gridReference[0]] = gridReference[1];
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;
                        case "UP":
                            if (gridReference[0] - inPlayerShips[ship]._shipLength >= 0)
                            {
                                bool isValid = false;
                                for (int x = gridReference[0]; x > gridReference[0] - inPlayerShips[ship]._shipLength; x--)
                                {
                                    if (inGameBoard._boardLayout[x, gridReference[1]] != "V")
                                    {
                                        isValid = true;
                                    }
                                    else
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }
                                if (isValid)
                                {
                                    for (int x = gridReference[0]; x > gridReference[0] - inPlayerShips[ship]._shipLength; x--)
                                    {
                                        inGameBoard._boardLayout[x, gridReference[1]] = "V";
                                        inPlayerShips[ship].RowPosition[gridReference[0] - x] = x;
                                        inPlayerShips[ship].ColumnPosition[gridReference[0] - x] = gridReference[1];
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;
                        default:
                            shipPlaced = false;
                            break;
                    }
                    

                    if (shipPlaced == false)
                    {
                        Console.WriteLine("\n-Invalid Placement-");
                        Console.WriteLine("Please select a valid initial co-ord and ensure the placement does not intersect with another ship \nor extend past the board.");
                    }
                }
                Console.Clear();
                PrintSingleBoard(inGameBoard);
            }
        }
        // // // // // // // // // // // // // // // // // // // // // // // //
        static void PrintSingleBoard(GameBoard boardToDisplay)
        {   //Displays a single board given to the method. Uses a 2d for loop to build a string comprised of the 2d array of the board
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
        {   //Prints two GameBoards given to it, intended to replicate the visual style of a 
            PrintSingleBoard(displayBoard);
            string tmp = "";
            for (int x = 0; x < (Board._gridSize * 3)+1; x++)
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
        static void InitialiseGame()
        {
            IntroPrintLogo();
            IntroGridSize();
            InitialisePositionalMarkers();
        }
        static void IntroGridSize()
        {
            Console.Clear();
            string tmp;
            bool check = false;
            Console.WriteLine("Welcome to Battle Ship\n" +
                              "Please select the size of your board. Available choices are: (a) 5x5, (b) 10x10 (standard) , (c) 15x15");
            Console.Write("Input your selection: ");
            tmp = Console.ReadLine().ToUpper();
            switch (tmp)
            {
                case "A":
                    Board.GridSize = 5;
                    check = true;
                    break;
                case "B":
                    Board.GridSize = 10;
                    check = true;
                    break;
                case "C":
                    Board.GridSize = 15;
                    check = true;
                    break;
                default:
                    break;
            }
            while (check == false)
            {
                Console.Write("Error. Please select one of the available options, 'a', 'b' or 'c': ");
                tmp = Console.ReadLine().ToUpper();
                switch (tmp)
                {
                    case "A":
                        Board.GridSize = 5;
                        check = true;
                        break;
                    case "B":
                        Board.GridSize = 10;
                        check = true;
                        break;
                    case "C":
                        Board.GridSize = 15;
                        check = true;
                        break;
                    default:
                        break;
                }
            }
        }

        static void IntroPrintLogo()
        {
            string intro =  "88                                     88                      88          88 \n"  +            
                            "88                       ,d      ,d    88                      88          \"\" \n"+  
                            "88                       88      88    88                      88              \n" +
                            "88,dPPYba,  ,adPPYYba, MM88MMM MM88MMM 88  ,adPPYba, ,adPPYba, 88,dPPYba,  88 8b,dPPYba, \n"+
                            "88P'    \"8a \"\"     `Y8   88      88    88 a8P_____88 I8[    \"\" 88P'    \"8a 88 88P'    \"8a\n" +
                            "88       d8 ,adPPPPP88   88      88    88 8PP\"\"\"\"\"\"\"  `\"Y8ba,  88       88 88 88       d8 \n" +
                            "88b,   ,a8\" 88,    ,88   88,     88,   88 \"8b,   ,aa aa    ]8I 88       88 88 88b,   ,a8\" \n" +
                            "8Y\"Ybbd8\"'  `\"8bbdP\"Y8   \"Y888   \"Y888 88  `\"Ybbd8\"' `\"YbbdP\"' 88       88 88 88`YbbdP\"' \n" +
							"		                                                              88\n " +
							"		                                                              88";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(intro);
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            
        }
    }
}

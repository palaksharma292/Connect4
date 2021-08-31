//Connect 4 Multiplayer game
//Refer to comments for explanation
//Creator: Palak Sharma

using System;
using System.Collections.Generic;

namespace Connect4
{
    //Created a class 'Game' that acts as a super class to all other classes
    //that define the functions of different types of players(Computer & Human)
    public abstract class Game
    {
        //Every player will have a symbol which is used in the grid
        public char Symbol { get; set; }

        //Every player will have a win/lose status which will define if they have won or not
        //In this case, 'false' means not won and 'true' means won
        //Will remain 'false' till the player does a connect4 with it's symbol
        public bool Match { get; set; } = false;

        //The game will have a 6*7 grid which will be used as the base of the game
        //This grid is common to all objects/players
        //Hence, it is a static field.
        public static char[,] grid = new char[6, 7];

        public Game()
        {
            //publically accessible constructor
            //The grid which is a 2 dimensional Array is initialized with ' '(Blankspace) 
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    grid[i, j] = ' ';
                }
            }
        }
        public static void DisplayGrid()
        {
            //The DisplayGrid function is used to display a static field(grid) which does not need an object
            //Hence, it is a static function because
            //it should give the same grid in the console window irrespective of any object

            Console.WriteLine("The Current Grid");
            //Displaying the grid
            for (int i = 0; i < 6; i++)
            {
                Console.Write($"{i,3} {'|',3}");
                for (int j = 0; j < 7; j++)
                {
                    Console.Write($"{grid[i, j],3}");
                }
                Console.Write($"{'|',3}");
                Console.WriteLine("");
            }
            Console.WriteLine("----------------------------------");
            Console.Write("       ");
            for (int i=0;i<7;i++)
            {
                Console.Write($"{i,3}");
            }
            Console.WriteLine("\n");
        }

        //For this program, both computer player and human have different ways to pick a column to add thier symbol too
        //So it is defines as abstract to make sure it is defined in the subclasses of this class
        public abstract int ChooseColumn();

        //Whenever a player takes a turn, TakeTurn() is called 
        //Here, a column number is chosen based on the return value of overridden ChooseColumn() in the object/player's respective class
        //Then, the bottom-most empty space in the column is filled with the calling player's symbol
        public void TakeTurn()
        {
            int col = ChooseColumn();
            while (grid[0, col] != ' ')
            {
                col = ChooseColumn();
            }

            for (int j = 5; j >= 0; j--)
            {
                if (grid[j, col] == ' ')
                {
                    grid[j, col] = Symbol;
                    break;
                }
            }

            DisplayGrid();
            //Win() is called to check if there is any new formation of Connect4 after the recently added symbol
            Win();
            //if There is no such winning formation, message for next turn is displayed.
            if (Match == false)
            {
                Console.WriteLine("Press ENTER for next turn");
                Console.ReadKey();
            }
        }

        public void Win()
        {
            //Win() checks for formations vertically, horizonatlly and diagonally 
            //If there is a formation in any of the 3 above stated fashions, it means the player has won 
            //So, Match is updated to true
            if (CheckVertical()||CheckHorizontal()||CheckDiagonal())
            {
                //these functions check for the 4 consecutive matching symbols' formation column-wise, row-wise and diagonally
                //whenever there is an instance of the player's symbol, count is incremented by 1
                //if there is any break in the chain of matching symbols due to empty space or other player's symbol,
                //the count is changed to 0.
                //if count==4, means there is a connect4 formation and true is returned
                //if there is no such pattern throughout the grid, false is returned
                Match = true;
            }
        }

        public bool CheckVertical()
        {
            int count = 0;
            //Checking if the Connect 4 exists vertically
            for (int i=0;i<7;i++)
            {
                for (int j=5;j>=0;j--)
                {
                    if(grid[j,i]==Symbol)
                    {
                        count++;
                        if (count == 4)
                        {
                            Console.WriteLine($"Vertical line in column {i}");
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                count = 0;
            }
            return false;
        }
        public bool CheckHorizontal()
        {
            int count = 0;
            //Checking if the Connect 4 HORIZONATLLY
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (grid[i,j] == Symbol)
                    {
                        count++;
                        if (count == 4)
                        {
                            Console.WriteLine($"Horizontal line in row {i}");
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                }
                
                count = 0;
            }
            return false;
        }
        public bool CheckDiagonal()
        {
            int count = 0;
            //Checking if the Connect 4 exists vertically
            for (int i = 0; i < 7; i++)
            {
                int k = i, j = 0;
                while(k<6)
                {
                    if (grid[j, k] == Symbol)
                    {
                        count++;
                        if (count == 4)
                        {
                            Console.WriteLine($"Diagonal starting in row {i}");
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                    k++;
                    j++;
                }
                if (count == 4)
                {
                    return true;
                }
                count = 0;
            }

            for (int i = 1; i < 6; i++)
            {
                int k = i, j = 0;
                while (k < 6)
                {
                    if (grid[k, j] == Symbol)
                    {
                        count++;
                        if (count == 4)
                        {
                            Console.WriteLine($"Diagonal starting in row {i + 1}");
                            return true;
                        }
                    }
                    else
                    {
                        count = 0;
                    }
                    k++;
                    j++;
                }
                count = 0;
            }
            return false;
        }
    }

    //A subclass of the Game class that defines the BasicComputer Player or A Novice Computer's functions in the game
    public class BasicComputer: Game
    {
        //The public parameterized constructor defines the Symbol
        public BasicComputer(char s)
        {
            Symbol = s;
        }

        //For BasicComputers, since they cannot access the situation strategically, they choose the column randomly
        public override int ChooseColumn()
        {
            Random r = new Random();
            int column = r.Next(0, 7);
            return column;
        }
    }

    //A subclass of Game class which is designed to declare the functions of an Actual human player
    public class Human : Game
    {
        //public default constructor takes user opinion on thier choice of Symbol and saves it as thier symbol for the game
        public Human()
        {
            Console.WriteLine("Enter Your Symbol of choice:     0 for X and 1 for O");
            int s = int.Parse(Console.ReadLine());
            if(s==0)
            {
                Symbol = 'X';
            }
            else if (s==1)
            {
                Symbol = 'O';
            }
        }

        //Sice, humans can analyze the situation and choose thier own columns for thier turn strategically, 
        //Humans enter the number of column to add the symbol to.
        //This column number is already indicated on the grid that is displayed to avoid confusion
        public override int ChooseColumn()
        {
            Console.WriteLine("Choose A Column Number:");
            int column = int.Parse(Console.ReadLine());
            return column;
        }
    }
    class Program
    {
        //At a time 2 players play a game and they are stored in List names players of datatype Game
        //Turnstaken and index are counter variables to keep a track of the number of turns and the index of the player whose turn is next
        static List<Game> players = new List<Game>();
        static int turnsTaken = 0;
        static int index = 0;
        
        public static void PlayersTurn()
        {
            //For players to take turns with appropriate message
            turnsTaken = 0;
            index = 0;
            //The players take turns one by one until one of the player has won the game
            //or the grid is completely full making another move impossible
            //Everytime a player takes a turn, both the variables are updated per requirement
            do
            {
                Console.WriteLine($"\n\nPlayer {index + 1}'s Turn");
                players[index].TakeTurn();
                index++;
                if (index == 2)
                {
                    index = 0;
                }
                turnsTaken++;
            } while (players[0].Match != true && players[1].Match != true && turnsTaken < 7 * 6);

        }

        public static void ResultCheck()
        {
            //When a game is terminated due to one the conditions in the PlayersTurn()'s do-while loop,
            //the exact condition that resulted inn termination is determined using if-else if block
            //and respective message is displayed to show the outcome of the game
            Console.WriteLine("\n\n----------------------------------------------------------------------");
            if (players[0].Match == true)
            {
                Console.WriteLine("Player 1 is the WINNER!!!");
            }
            else if (players[1].Match == true)
            {
                Console.WriteLine("Player 2 is the WINNER!!!");
            }
            else if (turnsTaken == 7 * 6)
            {
                Console.WriteLine("Players out of moves!!! No One Wins!!!");
            }
            Console.WriteLine("Press ENTER for NEW Game");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            string input;
            do
            {
                Console.Clear();
                //User is given choices for the desired course of action
                Console.WriteLine("For Computer(novice) vs Computer(novice) press 1");
                Console.WriteLine("For player vs Computer(novice) press 2");
                Console.WriteLine("For player vs player press 3");
                Console.WriteLine("To Exit press 4");
                input = Console.ReadLine().Substring(0,1);

                //Grid to begin game
                switch (input)
                {
                    case "1":
                        //if user chooses computer vs computer
                        //Two BasicComputer players/objects are declared
                        //And used for the duration of the game
                        Console.WriteLine("Starting Computer vs Computer!!!\n\n");
                        players.Clear();
                        players.Add(new BasicComputer('X'));
                        players.Add(new BasicComputer('O'));
                        Console.WriteLine($"Player 1: Computer 1's symbol is {players[0].Symbol}.");
                        Console.WriteLine($"Player 2: Computer 2's symbol is {players[1].Symbol}.\n\n");
                        Game.DisplayGrid();
                        Console.WriteLine("Press ENTER for Begin!!!");
                        Console.ReadKey();

                        //Call players to take turns
                        PlayersTurn();
                        //Result of the game
                        ResultCheck();
                        
                        break;

                    case "2":
                        //if user chooses human vs computer
                        //One BasicComputer and one human players/objects are declared
                        //And used for the duration of the game
                        Console.WriteLine("Starting Player vs Computer!!!\n\n");
                        players.Clear();
                        players.Add(new Human());
                        if (players[0].Symbol == 'X')
                        {
                            players.Add(new BasicComputer('O'));
                        }
                        else
                        {
                            players.Add(new BasicComputer('X'));
                        }
                        Console.WriteLine($"Player 1: Human's symbol is {players[0].Symbol}.");
                        Console.WriteLine($"Player 2: Computer's symbol is {players[1].Symbol}.\n\n");
                        Game.DisplayGrid();
                        Console.WriteLine("Press ENTER for Begin!!!");
                        Console.ReadKey();

                        //Call players to take turns
                        PlayersTurn();
                        //Result of the game
                        ResultCheck();

                        break;

                    case "3":
                        //if user chooses human vs human
                        //Two human players/objects are declared
                        //And used for the duration of the game
                        Console.WriteLine("Starting Player vs Player!!!\n\n");
                        players.Clear();
                        Console.WriteLine("Player 1");
                        players.Add(new Human());
                        Console.WriteLine("Player 2");
                        players.Add(new Human());
                        Console.WriteLine($"Player 1: Human's symbol is {players[0].Symbol}.");
                        Console.WriteLine($"Player 2: Human's symbol is {players[1].Symbol}.\n\n");
                        Game.DisplayGrid();
                        Console.WriteLine("Press ENTER for Begin!!!");
                        Console.ReadKey();

                        //Call players to take turns
                        PlayersTurn();
                        //Result of the game
                        ResultCheck();
                        break;

                    case "4":
                        //if user chooses to exit the game, appropriate message is displayed before exiting the program
                        Console.WriteLine("----------------------------------------------------------------------");
                        Console.WriteLine("Exiting the Game. Thanks for Playing.");
                        Console.WriteLine("----------------------------------------------------------------------");
                        break;

                    default:
                        //if the user gives an unexpected response, Invalid choice message is displayed and user is prompted to try again
                        Console.WriteLine("----------------------------------------------------------------------");
                        Console.WriteLine("Invalid choice!");
                        Console.WriteLine("Press ENTER to try again!");
                        Console.WriteLine("----------------------------------------------------------------------");
                        Console.ReadKey();
                        break;
                }
            } while (input != "4");
        }
    }
}

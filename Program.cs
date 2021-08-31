//Connect 4 Multiplayer game

using System;
using System.Collections.Generic;

namespace Connect4
{
    public abstract class Game
    {
        public char Symbol { get; set; }
        public bool Match { get; set; } = false;

        public static char[,] grid = new char[6, 7];

        public Game()
        {
            //Creating a static grid for the game
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

        public abstract int ChooseColumn();
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
            Win();
            if (Match == false)
            {
                Console.WriteLine("Press ENTER for next turn");
                Console.ReadKey();
            }
        }

        public void Win()
        {
            if (CheckVertical()||CheckHorizontal()||CheckDiagonal())
            {
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
    public class BasicComputer: Game
    {
        public BasicComputer(char s)
        {
            Symbol = s;
        }
        public override int ChooseColumn()
        {
            Random r = new Random();
            int column = r.Next(0, 7);
            return column;
        }
    }

    public class Human : Game
    {
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
        public override int ChooseColumn()
        {
            Console.WriteLine("Choose A Column Number:");
            int column = int.Parse(Console.ReadLine());
            return column;
        }
    }
    class Program
    {
        static List<Game> players = new List<Game>();
        static int turnsTaken = 0;
        static int index = 0;
        
        public static void PlayersTurn()
        {
            //Do-While only closes when either wins or the grid is filled.                        
            turnsTaken = 0;
            index = 0;
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
                Console.WriteLine("For Computer(novice) vs Computer(novice) press 1");
                Console.WriteLine("For player vs Computer(novice) press 2");
                Console.WriteLine("For player vs player press 3");
                Console.WriteLine("To Exit press 4");
                input = Console.ReadLine().Substring(0,1);

                //Grid to begin game
                switch (input)
                {
                    case "1":
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
                        Console.WriteLine("----------------------------------------------------------------------");
                        Console.WriteLine("Exiting the Game. Thanks for Playing.");
                        Console.WriteLine("----------------------------------------------------------------------");
                        break;
                    default:
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

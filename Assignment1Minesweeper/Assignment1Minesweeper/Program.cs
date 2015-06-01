/*
 * Kenneth Adair
 * www.cslearner.com
 * This was an assignment for my C# class.  We were asked to make a simple program that randomly generated "bombs"
 * and blank spaces for a minesweeper map.  I'm not the best programmer but I know how to use methods so I figured
 * out I could make a basic minesweeper game including player input and win conditions with nothing but lots and lots
 * of methods.  This program/game is the result.  It's messy but it works!  I'm actually very proud of myself, this is
 * my first game I've ever made.  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1Minesweeper
{
    class Program
    {
        //EmptyMapGenerator() is a method that generators an 2D array with dashes in each spot
        public static char[,] EmptyMapGenerator()
        {
            // Arrays are flipped in C# so it's [y, x]
            // y = 5
            // x = 10
            char[,] array = new char[5, 10];
            //This first loop is looping through the ROWS (y value), 5 rows
            for (int y = 0; y < 5; y++)
            {
                //This second loop is looping through the COLUMNS (x value), 10 columns
                for (int x = 0; x < 10; x++)
                {
                    //This is generating an empty map
                    array[y, x] = '-';
                }
            }
            return array;
        }

        //This method randomly generates bomgbs in our matrix
        public static char[,] BombFiller(char[,] EmptyMap){
            //r becomes a random number generator
            var r = new Random();
            //This loop creates randomized bombs
            for (int i = 0; i < 10; i++)
            {   //places random bombs on our grid
                EmptyMap[r.Next(5), r.Next(10)] = 'X';
            }

                return EmptyMap;
        }

        //This method displays any map we put into it onto the console
        public static char[,] MapDisplay(char[,] Map)
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
            for (int y = 0; y < 5; y++)
            {
                Console.Write(y + " ");
                //This second loop is looping through the COLUMNS (x value), 10 columns
                for (int x = 0; x < 10; x++)
                {
                    //This is generating an empty map
                    Console.Write(Map[y,x] + " ");
                }
                Console.WriteLine();
            }
            return Map;
        }

        //This method will be called by another method.  
        //This one checks a single square to see if it has an X or not
        public static int CheckSpecificBox(char[,] map, int x, int y){
            if (x < 0 || x > 9 || y < 0 || y > 4)
            {
                return 0;
            }
            else if (map[y, x] == 'X')
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //This method will use CheckSpecificBox many times to check the 8 boxes around a box to determine a square number
        //This method will be called by another method to update the entire map
        public static void CheckEightBoxes(char[,] map, int x, int y)
        {
            //If this space IS a bomb, do nothing and move on
            if (map[y, x] == 'X')
            {
                return;
            }
            else
            {
                //Check 9 squares including itself for nearby bombs
                //The reason we have the square check itself is for simplicity of the loops
                //Since the above IF statement catches if it's a bomb we don't have to worry

                //Counter tracks how many bombs are around the square
                int counter = 0;
                //Goes through each Y value nearby
                for (int yy = y - 1; yy < y + 2; yy++)
                {
                    //Goes through each X value nearby
                    for (int xx = x - 1; xx < x + 2; xx++)
                    {
                        //If there is a bomb at the specific counter we increment the counter
                        if (CheckSpecificBox(map, xx, yy) == 1)
                        {
                            counter++;
                        }
                        //counter = counter + CheckSpecificBox(map, xx, yy);
                    }
                }
                //Once it has counted between 0 and 8 bombs we update the square to reflect how many bombs are nearby
                if (counter > 0)
                {
                    if (counter == 1)
                    {
                        map[y, x] = '1';
                    }
                    else if (counter == 2)
                    {
                        map[y, x] = '2';
                    }
                    else if (counter == 3)
                    {
                        map[y, x] = '3';
                    }
                    else if (counter == 4)
                    {
                        map[y, x] = '4';
                    }
                    else if (counter == 5)
                    {
                        map[y, x] = '5';
                    }
                    else if (counter == 6)
                    {
                        map[y, x] = '6';
                    }
                    else if (counter == 7)
                    {
                        map[y, x] = '7';
                    }
                    else
                    {
                        map[y, x] = '8';
                    }
                }
            }
        }

        //This class checks the entire map and updates the number values to reflect how many bombs there are
        public static char[,] UpdateMapWithNumbers(char[,] map)
        {
            //This first loop is looping through all the ROWS (y value), 5 rows
            for (int y = 0; y < 5; y++)
            {
                //This second loop is looping through the COLUMNS (x value), 10 columns
                for (int x = 0; x < 10; x++)
                {
                    CheckEightBoxes(map, x, y);
                }
            }            
            return map;
        }

        //This generates the computer's map that we play against
        public static char[,] ComputerMap()
        {
            char[,] map = EmptyMapGenerator();
            map = BombFiller(map);
            map = UpdateMapWithNumbers(map);
            return map;
        }

        //This is the map the player will see
        public static char[,] EmptyDisplayMapGenerator()
        {
            // Arrays are flipped in C# so it's [y, x]
            // y = 5
            // x = 10
            char[,] array = new char[5, 10];
            //This first loop is looping through the ROWS (y value), 5 rows
            for (int y = 0; y < 5; y++)
            {
                //This second loop is looping through the COLUMNS (x value), 10 columns
                for (int x = 0; x < 10; x++)
                {
                    //This is generating an empty map
                    array[y, x] = ' ';
                }
            }
            return array;
        }

        public static void CheckAllNearbySafeSquares(char[,] ComputerMap, char[,] PlayerMap, int x, int y)
        {
            //Goes through each Y value nearby
            for (int yy = y - 1; yy < y + 2; yy++)
            {
                //Goes through each X value nearby
                for (int xx = x - 1; xx < x + 2; xx++)
                {
                    if (xx < 0 || xx > 9 || yy < 0 || yy > 4)
                    {
                        //Do nothing since this square doesn't exist
                    }
                    //If the square is empty, reveal it
                    //Make sure to check that the player map is empty or the program will trigger
                    // an infinite recursive loop
                    else if (PlayerMap[yy, xx] == ' ' && ComputerMap[yy, xx] == '-')
                    {
                        //If the square is empty reveal it and then recursively check all nearby squares
                        PlayerMap[yy,xx] = ComputerMap[yy,xx];
                        CheckAllNearbySafeSquares(ComputerMap, PlayerMap, xx, yy);
                    }
                    else if (ComputerMap[yy, xx] == 'X')
                    {
                        //Do nothing if that square is a bomb
                    }
                    else
                    {
                        //If the square is a number reveal it but don't recursively check that one.
                        PlayerMap[yy, xx] = ComputerMap[yy, xx];
                    }
                }
            }
        }

        //This allows a user to designate a spot as a bomb, same as a "right-click"
        public static void DeclareBomb(char[,] PlayerMap, int x, int y)
        {
            PlayerMap[y, x] = 'X';
        }

        public static void Lose()
        {
            Console.WriteLine("Sorry you lose!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public static void Win()
        {
            Console.WriteLine("Congrats you win!");
            Console.ReadLine();
            Environment.Exit(0);
        }

        public static void CheckWinCondition(char[,] ComputerMap, char[,] PlayerMap)
        {
            //This first loop is looping through the ROWS (y value), 5 rows
            for (int y = 0; y < 5; y++)
            {
                //This second loop is looping through the COLUMNS (x value), 10 columns
                for (int x = 0; x < 10; x++)
                {
                    //If they are both equal, keep going and see if we win
                    if (ComputerMap[y, x] == PlayerMap[y, x])
                    {
                        //Do nothing, but keep looping and see if we win
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //If every square in the player matrix and the NPC matrix are the same you win
            Win();
        }

        //This is the same as a left-click, means the user thinks the spot is safe
        public static void Reveal(char[,] ComputerMap, char[,] PlayerMap, int x, int y)
        {
            //If the user clicks on a bomb they die.
            if (ComputerMap[y, x] == 'X')
            {
                Lose();
            }
            //If the user clicked on an empty square it reveals nearby squares
            else if (ComputerMap[y, x] == '-')
            {
                CheckAllNearbySafeSquares(ComputerMap, PlayerMap, x, y);
            }
            else
            {
                //If the user just clicked on a number, just reveal the number;
                PlayerMap[y, x] = ComputerMap[y, x];
            }
        }

        //If the player enters cheatcode lowercase they win
        public static void CheatCode(char[,] ComputerMap, char[,] PlayerMap)
        {
            //This first loop is looping through the ROWS (y value), 5 rows
            for (int y = 0; y < 5; y++)
            {
                //This second loop is looping through the COLUMNS (x value), 10 columns
                for (int x = 0; x < 10; x++)
                {
                    //Updates the map so that the player map is equal to the computer's map
                    PlayerMap[y, x] = ComputerMap[y, x];
                }
            }
        }

        public static void PlayerInput(char[,] ComputerMap, char[,] PlayerMap)
        {
            //This clears the console so all they see is the map and options to pick
            Console.Clear();
            //Variables we need for the user input
            String UserInput, UserInputX, UserInputY; 
            int x, y;

            //These lines are the output that pront the user what they should do
            Console.WriteLine("   Minesweeper map: ");
            MapDisplay(PlayerMap);
            Console.WriteLine("Would you like to designate a square as safe or designate a square as a bomb?");
            Console.WriteLine("Enter 1 to designate a square as safe or 2 to designate a square as a bomb.");
            Console.WriteLine("Enter 'cheatcode' to win automatically or 'displaymap' to display the computer's map including bombs");
            Console.WriteLine("(Enter 3 to quit.)");

            //This line accepts the user input and puts it into "UserInput"
            UserInput = Console.ReadLine();

            //If the user says they want to designate a square as safe, do this:
            if(UserInput=="1"){
                Console.WriteLine("Designating a square as safe: ");
                Console.WriteLine("Please enter the X coordinate: ");
                UserInputX = Console.ReadLine();
                if(UserInputX == "0" || UserInputX == "1" || UserInputX == "2" || UserInputX == "3" || UserInputX == "4" || UserInputX == "5" || UserInputX == "6" || UserInputX == "7" || UserInputX == "8" || UserInputX == "9"){
                x = int.Parse(UserInputX);
                }else{return;}
                Console.WriteLine("Please enter the Y coordinate: ");
                UserInputY = Console.ReadLine();
                 if(UserInputY == "0" || UserInputY == "1" || UserInputY == "2" || UserInputY == "3" || UserInputY == "4"){
                y = int.Parse(UserInputY);
                 }else{return;}
                Reveal(ComputerMap, PlayerMap, x, y);
            }
            //If the user says they want to designate a square as a bomb, do this:
            else if(UserInput=="2"){
                //Prompts the user for an X coordinate
                Console.WriteLine("Designating a square as a bomb: ");
                Console.WriteLine("Please enter the X coordinate: ");
                //This takes the user input as a string initially
                UserInputX = Console.ReadLine();
                //This checks to make sure the user input is a number
                if(UserInputX == "0" || UserInputX == "1" || UserInputX == "2" || UserInputX == "3" || UserInputX == "4" || UserInputX == "5" || UserInputX == "6" || UserInputX == "7" || UserInputX == "8" || UserInputX == "9"){
                    //If the user input is a number then cast it into an int
                    x = int.Parse(UserInputX);
                    //If the user input is not a number then restart the loop
                }else{return;}
                //This asks the user for a Y coordinate
                Console.WriteLine("Please enter the Y coordinate: ");
                //This takes the input as a string initially
                UserInputY = Console.ReadLine();
                //This checks to ensure the user input is a valid number
                if (UserInputY == "0" || UserInputY == "1" || UserInputY == "2" || UserInputY == "3" || UserInputY == "4")
                {
                    //If the user input an integer then we cast the string as an integer
                    y = int.Parse(UserInputY);
                    //If the user input is not a number then restart the loop
                }else {return; }
                //This method "DeclareBomb" just designates a square as a bomb on the player's map.
                DeclareBomb(PlayerMap, x, y);
            }
             //If the user says they want to quit do this:
             else if(UserInput=="3"){
                    Console.WriteLine("Thank you for playing!");
                    Console.ReadLine();
                    Environment.Exit(0);
             }

            else if (UserInput == "cheatcode")
            {
                Console.WriteLine();
                CheatCode(ComputerMap, PlayerMap);
                Console.WriteLine();
                MapDisplay(PlayerMap);
                Console.WriteLine();
            }

            else if (UserInput == "displaymap")
            {
                Console.WriteLine();
                MapDisplay(ComputerMap);
                Console.WriteLine();
                Console.ReadLine();
            }
            //If the user enters invalid input then just restart the loop.
            else
            {
                return;
            }
            CheckWinCondition(ComputerMap, PlayerMap);
        }

        //This is the MAIN, it just calls our methods to play the game
        static void Main(string[] args)
        {
                char[,] NPCMap = ComputerMap();
                char[,] PlayerMap = EmptyDisplayMapGenerator();
                while (true)
                {
                    PlayerInput(NPCMap, PlayerMap);
                }
            }
    }
}
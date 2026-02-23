// Posted by Wagacca, modified by community. See post 'Timeline' for change history
// Retrieved 2026-02-17, License - CC BY-SA 3.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        // game field dimensions
        static int windowWidth;
        static int windowHeight;

        // random generator for food placement
        static Random random = new Random();

        // player score (starts at 5 so snake has initial length)
        static int score = 5;

        // flag to end the game
        static bool isGameOver = false;

        // the snake head object
        static SnakeHead snakeHead;

        // current movement direction
        static string currentDirection = "RIGHT";

        // snake body segments stored as separate x and y lists
        static List<int> bodyXPositions = new List<int>();
        static List<int> bodyYPositions = new List<int>();

        // food coordinates
        static int foodX;
        static int foodY;

        static void Main(string[] args)
        {
            InitializeGame();
            RunGameLoop();
            DisplayGameOver();
        }

        // sets up console window and initial game state
        static void InitializeGame()
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            windowWidth = Console.WindowWidth;
            windowHeight = Console.WindowHeight;

            snakeHead = new SnakeHead();
            snakeHead.X = windowWidth / 2;
            snakeHead.Y = windowHeight / 2;
            snakeHead.Color = ConsoleColor.Red;

            foodX = random.Next(0, windowWidth);
            foodY = random.Next(0, windowHeight);
        }

        // main game loop - runs until game over
        static void RunGameLoop()
        {
            while (true)
            {
                Console.Clear();

                CheckBorderCollision();
                DrawBorders();

                Console.ForegroundColor = ConsoleColor.Green;

                CheckFoodCollision();
                DrawBodyAndCheckSelfCollision();

                if (isGameOver)
                    break;

                DrawSnakeHead();
                DrawFood();

                HandleInputForFrame();

                UpdateSnakePosition();
                RemoveTailIfTooLong();
            }
        }

        // check if snake head hit any wall
        static void CheckBorderCollision()
        {
            if (snakeHead.X == windowWidth - 1 || snakeHead.X == 0 ||
                snakeHead.Y == windowHeight - 1 || snakeHead.Y == 0)
            {
                isGameOver = true;
            }
        }

        // draws all four borders around the playing field
        static void DrawBorders()
        {
            DrawHorizontalBorder(0);
            DrawHorizontalBorder(windowHeight - 1);
            DrawVerticalBorder(0);
            DrawVerticalBorder(windowWidth - 1);
        }

        // draws a horizontal line of blocks at given y position
        static void DrawHorizontalBorder(int y)
        {
            for (int i = 0; i < windowWidth; i++)
            {
                Console.SetCursorPosition(i, y);
                Console.Write("■");
            }
        }

        // draws a vertical line of blocks at given x position
        static void DrawVerticalBorder(int x)
        {
            for (int i = 0; i < windowHeight; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write("■");
            }
        }

        // if snake is on the food, increase score and move food
        static void CheckFoodCollision()
        {
            if (foodX == snakeHead.X && foodY == snakeHead.Y)
            {
                score++;
                foodX = random.Next(1, windowWidth - 2);
                foodY = random.Next(1, windowHeight - 2);
            }
        }

        // renders all body segments and checks if head overlaps any of them
        static void DrawBodyAndCheckSelfCollision()
        {
            for (int i = 0; i < bodyXPositions.Count(); i++)
            {
                Console.SetCursorPosition(bodyXPositions[i], bodyYPositions[i]);
                Console.Write("■");

                if (bodyXPositions[i] == snakeHead.X && bodyYPositions[i] == snakeHead.Y)
                {
                    isGameOver = true;
                }
            }
        }

        // draws the snake head with its color
        static void DrawSnakeHead()
        {
            Console.SetCursorPosition(snakeHead.X, snakeHead.Y);
            Console.ForegroundColor = snakeHead.Color;
            Console.Write("■");
        }

        // draws food on the screen
        static void DrawFood()
        {
            Console.SetCursorPosition(foodX, foodY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■");
        }

        // waits for 500ms and reads player input during that time
        static void HandleInputForFrame()
        {
            DateTime frameStartTime = DateTime.Now;
            bool directionChangedThisTick = false;

            while (true)
            {
                DateTime currentTime = DateTime.Now;
                if (currentTime.Subtract(frameStartTime).TotalMilliseconds > 500)
                    break;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                    directionChangedThisTick = TryChangeDirection(keyPressed, directionChangedThisTick);
                }
            }
        }

        // tries to change direction based on pressed key, returns true if direction was changed
        static bool TryChangeDirection(ConsoleKeyInfo keyPressed, bool alreadyChanged)
        {
            if (alreadyChanged)
                return true;

            if (keyPressed.Key.Equals(ConsoleKey.UpArrow) && currentDirection != "DOWN")
            {
                currentDirection = "UP";
                return true;
            }

            if (keyPressed.Key.Equals(ConsoleKey.DownArrow) && currentDirection != "UP")
            {
                currentDirection = "DOWN";
                return true;
            }

            if (keyPressed.Key.Equals(ConsoleKey.LeftArrow) && currentDirection != "RIGHT")
            {
                currentDirection = "LEFT";
                return true;
            }

            if (keyPressed.Key.Equals(ConsoleKey.RightArrow) && currentDirection != "LEFT")
            {
                currentDirection = "RIGHT";
                return true;
            }

            return false;
        }

        // adds current head position to body and moves the head
        static void UpdateSnakePosition()
        {
            bodyXPositions.Add(snakeHead.X);
            bodyYPositions.Add(snakeHead.Y);

            MoveHead();
        }

        // moves the head one step in current direction
        static void MoveHead()
        {
            switch (currentDirection)
            {
                case "UP":
                    snakeHead.Y--;
                    break;
                case "DOWN":
                    snakeHead.Y++;
                    break;
                case "LEFT":
                    snakeHead.X--;
                    break;
                case "RIGHT":
                    snakeHead.X++;
                    break;
            }
        }

        // removes the oldest body segment if body is longer than score
        static void RemoveTailIfTooLong()
        {
            if (bodyXPositions.Count() > score)
            {
                bodyXPositions.RemoveAt(0);
                bodyYPositions.RemoveAt(0);
            }
        }

        // shows the final score when game ends
        static void DisplayGameOver()
        {
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 + 1);
        }

        // represents the snake head with position and color
        class SnakeHead
        {
            public int X { get; set; }
            public int Y { get; set; }
            public ConsoleColor Color { get; set; }
        }
    }
}
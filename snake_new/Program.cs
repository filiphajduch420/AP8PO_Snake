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
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;
            
            Random random = new Random();
            
            int score = 5;
  
            bool isGameOver = false;
            
            SnakeHead snakeHead = new SnakeHead();
            snakeHead.X = windowWidth / 2;
            snakeHead.Y = windowHeight / 2;
            snakeHead.Color = ConsoleColor.Red;
            
            string currentDirection = "RIGHT";
            
            List<int> bodyXPositions = new List<int>();
            List<int> bodyYPositions = new List<int>();
            
            int foodX = random.Next(0, windowWidth);
            int foodY = random.Next(0, windowHeight);
            
            DateTime frameStartTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            
            bool directionChangedThisTick = false;
            
            while (true)
            {
                Console.Clear();
                
                // Check collision with borders
                if (snakeHead.X == windowWidth - 1 || snakeHead.X == 0 || 
                    snakeHead.Y == windowHeight - 1 || snakeHead.Y == 0)
                { 
                    isGameOver = true;
                }
                
                // Draw top border
                for (int i = 0; i < windowWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }
                
                // Draw bottom border
                for (int i = 0; i < windowWidth; i++)
                {
                    Console.SetCursorPosition(i, windowHeight - 1);
                    Console.Write("■");
                }
                
                // Draw left border
                for (int i = 0; i < windowHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }
                
                // Draw right border
                for (int i = 0; i < windowHeight; i++)
                {
                    Console.SetCursorPosition(windowWidth - 1, i);
                    Console.Write("■");
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                
                // Check if snake ate food
                if (foodX == snakeHead.X && foodY == snakeHead.Y)
                {
                    score++;
                    foodX = random.Next(1, windowWidth - 2);
                    foodY = random.Next(1, windowHeight - 2);
                } 
                
                // Draw snake body and check self-collision
                for (int i = 0; i < bodyXPositions.Count(); i++)
                {
                    Console.SetCursorPosition(bodyXPositions[i], bodyYPositions[i]);
                    Console.Write("■");
                    
                    if (bodyXPositions[i] == snakeHead.X && bodyYPositions[i] == snakeHead.Y)
                    {
                        isGameOver = true;
                    }
                }
                
                if (isGameOver)
                {
                    break;
                }
                
                // Draw snake head
                Console.SetCursorPosition(snakeHead.X, snakeHead.Y);
                Console.ForegroundColor = snakeHead.Color;
                Console.Write("■");
                
                // Draw food
                Console.SetCursorPosition(foodX, foodY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                
                frameStartTime = DateTime.Now;
                directionChangedThisTick = false;
                
                // Input handling loop (500ms frame time)
                while (true)
                {
                    currentTime = DateTime.Now;
                    if (currentTime.Subtract(frameStartTime).TotalMilliseconds > 500) 
                    { 
                        break; 
                    }
                    
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                        
                        if (keyPressed.Key.Equals(ConsoleKey.UpArrow) && 
                            currentDirection != "DOWN" && 
                            !directionChangedThisTick)
                        {
                            currentDirection = "UP";
                            directionChangedThisTick = true;
                        }
                        
                        if (keyPressed.Key.Equals(ConsoleKey.DownArrow) && 
                            currentDirection != "UP" && 
                            !directionChangedThisTick)
                        {
                            currentDirection = "DOWN";
                            directionChangedThisTick = true;
                        }
                        
                        if (keyPressed.Key.Equals(ConsoleKey.LeftArrow) && 
                            currentDirection != "RIGHT" && 
                            !directionChangedThisTick)
                        {
                            currentDirection = "LEFT";
                            directionChangedThisTick = true;
                        }
                        
                        if (keyPressed.Key.Equals(ConsoleKey.RightArrow) && 
                            currentDirection != "LEFT" && 
                            !directionChangedThisTick)
                        {
                            currentDirection = "RIGHT";
                            directionChangedThisTick = true;
                        }
                    }
                }
                
                // Add current head position to body
                bodyXPositions.Add(snakeHead.X);
                bodyYPositions.Add(snakeHead.Y);
                
                // Move snake head based on direction
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
                
                // Remove tail if snake is too long
                if (bodyXPositions.Count() > score)
                {
                    bodyXPositions.RemoveAt(0);
                    bodyYPositions.RemoveAt(0);
                }
            }
            
            // Display game over message
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 + 1);
        }
        
        class SnakeHead
        {
            public int X { get; set; }
            public int Y { get; set; }
            public ConsoleColor Color { get; set; }
        }
    }
}
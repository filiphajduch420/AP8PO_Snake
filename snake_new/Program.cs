// Posted by Wagacca, modified by community. See post 'Timeline' for change history
// Retrieved 2026-02-17, License - CC BY-SA 3.0

namespace Snake
{
    // entry point - just starts the game
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }

    // main game class - connects all parts together and runs the loop
    class Game
    {
        private GameField _field;
        private Snake _snake;
        private Food _food;
        private int _score = 5;
        private bool _isGameOver = false;

        // set up game objects
        public Game()
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            _field = new GameField(Console.WindowWidth, Console.WindowHeight);
            _snake = new Snake(_field.Width / 2, _field.Height / 2);
            _food = new Food(_field.Width, _field.Height);
        }

        // main entry - init, loop, then show result
        public void Run()
        {
            RunGameLoop();
            DisplayGameOver();
        }

        // game loop - runs every frame until game over
        private void RunGameLoop()
        {
            while (true)
            {
                Console.Clear();

                CheckCollisions();

                if (_isGameOver)
                    break;

                DrawFrame();
                HandleInput();
                UpdateSnake();
            }
        }

        // check all collisions - borders, food and self
        private void CheckCollisions()
        {
            if (_field.IsOnBorder(_snake.HeadX, _snake.HeadY))
                _isGameOver = true;

            if (_food.IsAt(_snake.HeadX, _snake.HeadY))
            {
                _score++;
                _food.Respawn(_field.Width, _field.Height);
            }

            if (_snake.IsCollidingWithSelf())
                _isGameOver = true;
        }

        // draw everything on screen
        private void DrawFrame()
        {
            DrawBorders();
            DrawSnakeBody();
            DrawSnakeHead();
            DrawFood();
        }

        // draw all four borders
        private void DrawBorders()
        {
            for (int i = 0; i < _field.Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, _field.Height - 1);
                Console.Write("■");
            }

            for (int i = 0; i < _field.Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(_field.Width - 1, i);
                Console.Write("■");
            }
        }

        // draw all body segments
        private void DrawSnakeBody()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < _snake.BodyLength; i++)
            {
                var segment = _snake.GetBodySegment(i);
                Console.SetCursorPosition(segment.x, segment.y);
                Console.Write("■");
            }
        }

        // draw the head
        private void DrawSnakeHead()
        {
            Console.SetCursorPosition(_snake.HeadX, _snake.HeadY);
            Console.ForegroundColor = _snake.HeadColor;
            Console.Write("■");
        }

        // draw food
        private void DrawFood()
        {
            Console.SetCursorPosition(_food.X, _food.Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■");
        }

        // wait 500ms and read keyboard input during that time
        private void HandleInput()
        {
            DateTime frameStart = DateTime.Now;
            bool changed = false;

            while (true)
            {
                if (DateTime.Now.Subtract(frameStart).TotalMilliseconds > 500)
                    break;

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (!changed)
                        changed = _snake.TryChangeDirection(key.Key);
                }
            }
        }

        // move snake and trim tail
        private void UpdateSnake()
        {
            _snake.Move();
            _snake.TrimTail(_score);
        }

        // show final score
        private void DisplayGameOver()
        {
            Console.SetCursorPosition(_field.Width / 5, _field.Height / 2);
            Console.WriteLine("Game over, Score: " + _score);
            Console.SetCursorPosition(_field.Width / 5, _field.Height / 2 + 1);
        }
    }
}
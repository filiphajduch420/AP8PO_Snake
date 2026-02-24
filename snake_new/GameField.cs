namespace Snake
{
    // represents the playing field with borders
    class GameField
    {
        public int Width { get; }
        public int Height { get; }

        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
        }

        // check if given position is on the border
        public bool IsOnBorder(int x, int y)
        {
            return x == Width - 1 || x == 0 || y == Height - 1 || y == 0;
        }
    }
}


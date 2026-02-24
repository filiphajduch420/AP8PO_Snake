namespace Snake
{
    // represents the food that the snake eats
    class Food
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private Random _random = new Random();

        public Food(int fieldWidth, int fieldHeight)
        {
            Respawn(fieldWidth, fieldHeight);
        }

        // move food to a new random position inside the field
        public void Respawn(int fieldWidth, int fieldHeight)
        {
            X = _random.Next(1, fieldWidth - 2);
            Y = _random.Next(1, fieldHeight - 2);
        }

        // check if food is at given position
        public bool IsAt(int x, int y)
        {
            return X == x && Y == y;
        }
    }
}


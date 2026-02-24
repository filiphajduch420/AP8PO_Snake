namespace Snake
{
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    // represents the snake - head, body and movement
    class Snake
    {
        public int HeadX { get; set; }
        public int HeadY { get; set; }
        public ConsoleColor HeadColor { get; set; }
        public Direction Direction { get; set; }

        // body segments stored as coordinates
        private List<(int x, int y)> _bodySegments = new List<(int x, int y)>();

        public Snake(int startX, int startY)
        {
            HeadX = startX;
            HeadY = startY;
            HeadColor = ConsoleColor.Red;
            Direction = Direction.Right;
        }

        // number of body segments
        public int BodyLength => _bodySegments.Count;

        // get body segment position at index
        public (int x, int y) GetBodySegment(int index)
        {
            return _bodySegments[index];
        }

        // check if head overlaps any body segment
        public bool IsCollidingWithSelf()
        {
            for (int i = 0; i < _bodySegments.Count; i++)
            {
                if (_bodySegments[i].x == HeadX && _bodySegments[i].y == HeadY)
                    return true;
            }
            return false;
        }

        // save current head position to body and move head
        public void Move()
        {
            _bodySegments.Add((HeadX, HeadY));

            switch (Direction)
            {
                case Direction.Up:
                    HeadY--;
                    break;
                case Direction.Down:
                    HeadY++;
                    break;
                case Direction.Left:
                    HeadX--;
                    break;
                case Direction.Right:
                    HeadX++;
                    break;
            }
        }

        // remove oldest body segment if body is longer than allowed
        public void TrimTail(int maxLength)
        {
            if (_bodySegments.Count > maxLength)
            {
                _bodySegments.RemoveAt(0);
            }
        }

        // try to change direction, prevents reversing
        public bool TryChangeDirection(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow && Direction != Direction.Down)
            {
                Direction = Direction.Up;
                return true;
            }
            if (key == ConsoleKey.DownArrow && Direction != Direction.Up)
            {
                Direction = Direction.Down;
                return true;
            }
            if (key == ConsoleKey.LeftArrow && Direction != Direction.Right)
            {
                Direction = Direction.Left;
                return true;
            }
            if (key == ConsoleKey.RightArrow && Direction != Direction.Left)
            {
                Direction = Direction.Right;
                return true;
            }
            return false;
        }
    }
}

namespace Snake
{
    // represents the snake - head, body and movement
    class Snake
    {
        public int HeadX { get; set; }
        public int HeadY { get; set; }
        public ConsoleColor HeadColor { get; set; }
        public string Direction { get; set; }

        // body segments stored as x and y lists
        private List<int> _bodyXPositions = new List<int>();
        private List<int> _bodyYPositions = new List<int>();

        public Snake(int startX, int startY)
        {
            HeadX = startX;
            HeadY = startY;
            HeadColor = ConsoleColor.Red;
            Direction = "RIGHT";
        }

        // number of body segments
        public int BodyLength => _bodyXPositions.Count;

        // get body segment position at index
        public (int x, int y) GetBodySegment(int index)
        {
            return (_bodyXPositions[index], _bodyYPositions[index]);
        }

        // check if head overlaps any body segment
        public bool IsCollidingWithSelf()
        {
            for (int i = 0; i < _bodyXPositions.Count; i++)
            {
                if (_bodyXPositions[i] == HeadX && _bodyYPositions[i] == HeadY)
                    return true;
            }
            return false;
        }

        // save current head position to body and move head
        public void Move()
        {
            _bodyXPositions.Add(HeadX);
            _bodyYPositions.Add(HeadY);

            switch (Direction)
            {
                case "UP":
                    HeadY--;
                    break;
                case "DOWN":
                    HeadY++;
                    break;
                case "LEFT":
                    HeadX--;
                    break;
                case "RIGHT":
                    HeadX++;
                    break;
            }
        }

        // remove oldest body segment if body is longer than allowed
        public void TrimTail(int maxLength)
        {
            if (_bodyXPositions.Count > maxLength)
            {
                _bodyXPositions.RemoveAt(0);
                _bodyYPositions.RemoveAt(0);
            }
        }

        // try to change direction, prevents reversing
        public bool TryChangeDirection(ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow && Direction != "DOWN")
            {
                Direction = "UP";
                return true;
            }
            if (key == ConsoleKey.DownArrow && Direction != "UP")
            {
                Direction = "DOWN";
                return true;
            }
            if (key == ConsoleKey.LeftArrow && Direction != "RIGHT")
            {
                Direction = "LEFT";
                return true;
            }
            if (key == ConsoleKey.RightArrow && Direction != "LEFT")
            {
                Direction = "RIGHT";
                return true;
            }
            return false;
        }
    }
}


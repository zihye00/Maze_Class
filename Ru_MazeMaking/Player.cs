namespace Ru_MazeMaking
{
    // 좌표 만들기
    internal class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();  

        Board _board;

        public void Initialize(int posY, int posX, int destY, int destX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                //여기에 0.1초마다 실행될 로직을 넣어준다.
                //조건이 있다 -> 최대 최솟값을 채워줌
                int randValue = _random.Next(0, 5);
                switch (randValue)
                {
                    case 0: // up
                        if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                            PosY = PosY - 1;
                        break;
                    case 1: // down
                        if (PosY + 1 < _board.Size &&  _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                            PosY = PosY + 1;
                        break;
                    case 2: // left
                        if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                            PosX = PosX- 1;
                        break;
                    case 3: // right
                        if (PosX + 1 < _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                            PosX = PosX + 1;
                        break;
                }
            }
        }
    }
}

namespace Ru_MazeMaking
{
    class Board
    {
        const char CIRCLE = '\u25cf';

        public TileType[,] _tile;
        public int _size;


        public enum TileType
        {
            Empty,
            Wall,
        }

        public void Initialize(int size)
        {
            // size가 홀수여야 함(짝수면 리턴)
            if (size % 2 == 0)
                return;

            _tile = new TileType[size, size];
            _size = size;

            // Mazes for Programmers

            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        void GenerateBySideWinder()
        {
            // 일단 길을 다 막아버리는 작업
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                int count = 1;
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    //마지막 점이라면 멈추도록(벽 제외
                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    //오른쪽 끝까지 가지 않도록 막음
                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    //아래 끝까지 가지 않도록 막음
                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        _tile[y + 1, x - randomIndex * 2] = TileType.Empty; //점 두개씩 넘어가는 거라 *2
                        count = 1;
                    }
                }
            }
        }


        void GenerateByBinaryTree()
        {
            // 일단 길을 다 막아버리는 작업
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }

            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            Random rand = new Random();
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;

                    //마지막 점이라면 멈추도록(벽 제외
                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    //오른쪽 끝까지 가지 않도록 막음
                    if (y == _size - 2)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    //아래 끝까지 가지 않도록 막음
                    if (x == _size - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine(); //25개마다 줄넘기
            }

            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Cyan;
                case TileType.Wall:
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.Cyan;
            }
        }
    }


}

//왜 Y좌표부터 계산하는지
//_size, size 둘다 사용하는 이유? 외과타일 선정할떄
//콘솔창 줄간격 때문에? 미로 모양이 1:1로 안보여 줄간격 0.6으로 설정
// 콘솔창 실행 안되던 것은 프레임 단위 -> currenttick &로 추가해줌

#region
//namespace Prev_class
//{
//    class MyLinkedListNode<T>
//    {
//        public T Data;
//        public MyLinkedListNode<T> Next;
//        public MyLinkedListNode<T> Prev;
//    }

//    class MyLinkedList<T>
//    {
//        public MyLinkedListNode<T> Head; //첫번째
//        public MyLinkedListNode<T> Tail; //마지막
//        public int Count = 0;
//    }

//    public MyLinkedListNode<T> AddLast(T data)
//    {
//        MyLinkedListNode<T> newRoom = new MyLinkedListNode<T>();
//        newRoom.Data = data;

//        // 만약 아직 방이 아예 없다면 새로 추가한 첫번째 방이 곧 Head다.
//        if (Head == null)
//            Head = newRoom;

//        // 기존의 마지막 방과 새로 추가되는 방을 연결해준다.
//        if (Tail != null)
//        {
//            Tail.Next = newRoom;
//            newRoom.Prev = Tail;
//        }

//        // 새로 추가되는 방을 마지막 방으로 인정한다.
//        Tail = newRoom;
//        Count++;
//        return newRoom;

//    }

//    //101 102 103 104 105
//    public void Remove(MyLinkedListNode<T> room)
//    {
//        // 기존의 첫번째 방의 다음 방을 첫번째 방으로 인정한다.
//        if (Head == room)
//            Head = Head.Next;

//        // 기존의 마지막 방의 이전 방을 마지막 방으로 인정한다.
//        if (Tail == room)
//            Tail = Tail.Prev;

//        if (room.Prev != null)
//            room.Prev.Next = room.Next;

//        if (room.Next != null)
//            room.Next.Prev = room.Prev;
//    }

//    class Board
//    {
//        public MyLinkedList<int> _data3 = new MyLinkedList<int>(); //연결리스트

//        public void Initialize()
//        {
//            _data3.AddLast(101);
//            _data3.AddLast(102);
//            MyLinkedListNode<int> node = _data3.AddLast(103);
//            _data3.AddLast(104);
//            _data3.AddLast(105);

//            _data3.Romove(node);
//        }
//    }

//    //List
//    class MyList<T>
//    {
//        const int DEFAULT_SIZE = 1;
//        T[] _data = new T[DEFAULT_SIZE];

//        public int Count = 0; //실제로 사용중인 데이터 개수
//        public int Capacity { get { return _data.Length; } } // 예약된 데이터 개수

//        // !O(N) O(1) : 예외 케이스 : 이사 비용은 무시한다.
//        public void Add(T item)
//        {
//            //1. 공간이 충분히 남아 있는지 확인한다.
//            if (Count >= Capacity)
//            {
//                // 공간을 다시 늘려서 확보한다
//                T[] newArray = new T[Count * 2];
//                for (int i = 0; i < Count; i++)
//                    newArray[i] = _data[i];
//                _data = newArray;
//            }

//            // 2. 공간에다가 데이터를 넣어준다.
//            _data[Count] = item;
//            Count++;
//        }

//        // O(1)
//        // +a 인덱스
//        public T this[int index]
//        {
//            get { return _data[index]; }
//            set { _data[index] = value; }
//        }

//        // O(N) : 최악의 경우까지 생각해서 (index가 0일때, N번 다 돌도록)
//        public void RemoveAt(int index)
//        {
//            // 101 102 104 105 0
//            for (int i = index; i < Count - 1; i++)
//                _data[i] = _data[i + 1];
//            _data[Count - 1] = default(T);
//            Count--;
//        }
//    }

//    internal class BoardList
//    {
//        public int[] _data = new int[25]; //배열
//        public MyList<int> _data2 = new MyList<int>(); //동적 배열
//        public LinkedList<int> _data3 = new LinkedList<int>(); //(이중)연결 리스트

//        public void Initialize()
//        {
//            _data2.Add(101);
//            _data2.Add(102);
//            _data2.Add(103);
//            _data2.Add(104);
//            _data2.Add(105);

//            int temp = _data2[2];

//            _data2.RemoveAt(2);
//        }
//    }

//}
#endregion
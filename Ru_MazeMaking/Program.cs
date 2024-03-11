using System.Runtime.CompilerServices;

namespace Ru_MazeMaking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //함수 시작하자마자 initialize실행
            Board board = new Board();
            board.Initialize(25);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;

            while (true)
            {
                #region 프레임 관리
                //FPS 프레임 (60프레임 OK 30프레임 이하 NO)

                /*              int currentTick = System.Environment.TickCount;
                                int elapsedTick = currentTick - lastTick;
                                // 만약에 경과한 시간이 1/30초보다 작다면(TickCount는 milesecend단위라서 1초가 1000ms)
                                if (elapsedTick < 1000 / 30)
                                {
                                    continue;
                                }
                                lastTick = currentTick;*/

                // 만약에 경과한 시간이 1/30초보다 작다면 -> 용이하게 수정
                int currentTick = Environment.TickCount & Int32.MaxValue;
                if (currentTick - lastTick < WAIT_TICK)
                {
                    continue;
                }
                lastTick = currentTick;
                #endregion

                //입력
                //로직
                //렌더링

                Console.SetCursorPosition(0, 0);

                board.Render();
            }
        }
    }
}

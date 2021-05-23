using Ungine;

namespace Outrun
{
    class OutRun : Game
    {
        public OutRun(int width, int height, string name) : base(width, height, name)
        {
        }

        public override void OnLose()
        {
            SetCurrentScene(new LoseScene());
        }
        public override void OnWin()
        {
            SetCurrentScene(new WinScene());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new OutRun(720, 480, "Outrun");

            game.SetCurrentScene(new OutrunScene());

            game.Start();
        }
    }
}

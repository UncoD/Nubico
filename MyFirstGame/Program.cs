using csharp_sfml_game_framework;

namespace MyFirstGame
{
    class MyFirstGame : Game
    {
        public MyFirstGame(int width, int height, string name) : base(width, height, name)
        {
        }

        public override void OnLose()
        {
            SetCurrentScene(new LoseScene());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var myGame = new MyFirstGame(500, 400, "My Game");
            myGame.SetCurrentScene(new MyScene());
            myGame.Start();
        }
    }
}

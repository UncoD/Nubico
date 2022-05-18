using Nubico.GameBase;

namespace ExampleTopDownGame
{
    class TopDownGame : Game
    {
        public TopDownGame(int width, int height, string name) : base(width, height, name)
        {
            DrawObjectBorders = false;
            Gravity = new SFML.System.Vector2f(0, 0);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var myGame = new TopDownGame(1280, 720, "TopDown game");
            myGame.SetCurrentScene(new MyScene());
            myGame.Start();
        }
    }
}

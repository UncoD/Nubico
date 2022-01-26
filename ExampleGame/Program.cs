using Nubico.GameBase;

namespace ExampleGame
{
    class MyFirstGame : Game
    {
        public MyFirstGame(int width, int height, string name) : base(width, height, name)
        {
            DrawObjectBorders = true;
        }

        public void MyMethod()
        {
            Console.WriteLine("my game");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var myGame = new MyFirstGame(500, 400, "My Game");
            myGame.SetCurrentScene(new MyScene(5));
            myGame.Start();
        }
    }
}

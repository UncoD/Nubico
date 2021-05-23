using Ungine;

namespace jRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(1190, 420, "jRPG");

            game.SetCurrentScene(new StartScene());

            game.Start();
        }
    }
}

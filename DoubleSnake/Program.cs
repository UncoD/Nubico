using Ungine;
using MyFirstGame.Scenes;
using SFML.Window;

namespace MyFirstGame
{
    class DoubleSnake : Game
    {
        public DoubleSnake(int width, int height, string name, Styles style) : base(width, height, name, style)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new DoubleSnake(0, 0, "~ Double Ssnake ~", Styles.Fullscreen);
            game.SetCurrentScene(new Menu());
            game.Start();
        }
    }
}

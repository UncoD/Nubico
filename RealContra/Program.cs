using csharp_sfml_game_framework;
using RealContra.Scenes;

namespace RealContra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var game = new RealContraGame(720, 480, "Contra");

            game.SetCurrentScene(new StartScene());

            game.Start();
        }
    }
}

internal class RealContraGame : Game
{
    public RealContraGame(int width, int height, string name) : base(width, height, name) { }

    public override void OnLose()
    {
        base.OnLose();
        SetCurrentScene(new LoseScene());
    }

    public override void OnWin()
    {
        base.OnWin();
        SetCurrentScene(new WinScene());
    }
}
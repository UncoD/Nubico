using System;
using csharp_sfml_game_framework;

namespace Asteroid
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var game = new AsteroidGame(900, 600, "Asteroid");

            game.MusicController.LoopMusic(true);
            game.MusicController.PlayMusic("Music/Harppen.ogg");

            game.SetCurrentScene(new StartScene());

            game.Start();
        }
    }
}

internal class AsteroidGame : Game
{
    public AsteroidGame(int width, int height, string name) : base(width, height, name)
    {
    }

    public override void OnLose()
    {
        base.OnLose();
        Environment.Exit(0);
    }
}
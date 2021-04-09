using csharp_sfml_game_framework;
using System;

namespace FlappyFish
{
    class FlappyFish : Game
    {
        public FlappyFish(int width, int height, string name) : base(width, height, name) { }

        public override void OnLose()
        {
            SetCurrentScene(new LoseScene());
            MusicController.StopMusic();
            SoundController.PlaySound("Sound/lose.wav");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new FlappyFish(1000, 600, "Flappy Fish");
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                // Для работы в Линукс
                // game.Window.SetFramerateLimit(250);
            }
            
            game.SetCurrentScene(new StartScene());

            game.Start();
        }
    }
}

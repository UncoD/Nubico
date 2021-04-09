using csharp_sfml_game_framework;

namespace Tetris
{
    class Tetris : Game
    {
        public Tetris(int width, int height, string name) : base(width, height, name)
        {
        }

        public override void OnLose()
        {
            SetCurrentScene(new LoseScene($"You lose! Score {Score}"));
            MusicController.StopMusic();
            MusicController.LoopMusic(false);
            MusicController.PlayMusic("Music/gameover.ogg");
        }
    }

    class Program
    {     
        
        static void Main(string[] args)
        {
            var game = new Tetris(Config.width, Config.height, "Tetris");

            game.SetCurrentScene(new StartScene());

            game.Start();
        }
    }
}

using csharp_sfml_game_framework;

namespace SpaceInvaders
{
    class Invaders : Game
    {
        public Invaders(int width, int height, string name) : base(width, height, name) { }

        public override void OnLose()
        {
            SetCurrentScene(new LoseScene());
            MusicController.StopMusic();
            SoundController.PlaySound("Sound/lose.wav");
        }

        public override void OnWin()
        {
            SetCurrentScene(new WinScene());
            MusicController.StopMusic();
            SoundController.PlaySound("Sound/win.wav");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Invaders(480, 600, "Invaders");
            
            game.SetCurrentScene(new StartScene());

            game.Start();
        }
    }
}

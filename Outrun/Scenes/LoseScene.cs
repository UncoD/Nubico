using csharp_sfml_game_framework;
using SFML.Graphics;
using SFML.Window;

namespace Outrun
{
    public class LoseScene : GameScene
    {
        private GameObject brokenCar;
        private Fire fire1;
        private Fire fire2;
        private Fire fire3;
        public LoseScene()
        {
            var loseText = new BlinkingTextObject("CRASH!", Game.Width / 2f, 100, 40);
            loseText.SetColor(Color.Red);
            brokenCar = new GameObject(Game.Width / 2f, Game.Height, "Art/chill_crash_000.png");
            fire1 = new Fire(Game.Width / 2f, Game.Height - 10);
            fire2 = new Fire(Game.Width / 2f - brokenCar.Width + 40, Game.Height - 20);
            fire3 = new Fire(Game.Width / 2f + brokenCar.Width - 30, Game.Height);

            var controlText = new TextObject("Esc - exit\nSpace - restart", 150, Game.Height - 50);

            AddToScene(loseText, controlText, brokenCar, fire1, fire2, fire3);

            MusicController.StopMusic();
            MusicController.PlayMusic("Music/Lose.ogg");
        }

        public override void OnEachFrame()
        {
            if (brokenCar.Y > Game.Height / 2f + 50)
            {
                brokenCar.MoveIt(0, -6);
                fire1.MoveIt(0, -8);
                fire2.MoveIt(0, -8);
                fire3.MoveIt(0, -8);
            }
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            switch (pressedKey)
            {
                case Keyboard.Key.Space:
                    Game.SetCurrentScene(new OutrunScene());
                    break;
                case Keyboard.Key.Escape:
                    Game.Close();
                    break;
            }
        }
    }
}
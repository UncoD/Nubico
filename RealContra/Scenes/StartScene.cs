using Ungine;
using RealContra.Backgrounds;
using SFML.Window;

namespace RealContra.Scenes
{
    internal class StartScene : GameScene
    {
        private int timer = 50;

        public StartScene()
        {
            MusicController.StopMusic();
            MusicController.PlayMusic("Sound/Background.wav");
            AddToScene(new StartBackground(Game.Width / 2, Game.Height / 2));
            AddToScene(new TextObject("Real Contra", Game.Width / 2 - 240, 50) { Size = 50 });
            AddToScene(new TextObject("Control:", 100, 150) { Size = 15 });
            AddToScene(new TextObject("Space, Up", 100, 170) { Size = 15 });
            AddToScene(new TextObject("Left, Right", 100, 200) { Size = 15 });
        }

        public override void OnEachFrame()
        {
            timer--;
            if (timer == 0)
                AddToScene(new BlinkingTextObject("Press any key", Game.Width / 2 - 200, Game.Height - 50, 5)
                { Size = 30 });
            base.OnEachFrame();
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            if (timer < 0)
                Game.SetCurrentScene(new Level1());
            base.OnKeyPress(key, isAlreadyPressed);
        }
    }
}
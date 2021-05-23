using Ungine;
using RealContra.Backgrounds;
using SFML.Window;

namespace RealContra.Scenes
{
    public class LoseScene : GameScene
    {
        private int timer = 50;

        public LoseScene()
        {
            MusicController.StopMusic();
            AddToScene(new LoseBackground(Game.Width / 2, Game.Width / 2 - 120));
        }

        public override void OnEachFrame()
        {
            timer--;
            if (timer == 0)
                AddToScene(new BlinkingTextObject("Press any key", Game.Width / 2 - 200, Game.Height - 50, 5)
                    {Size = 30});
            base.OnEachFrame();
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            if (timer < 0)
                Game.SetCurrentScene(new StartScene());
            base.OnKeyPress(key, isAlreadyPressed);
        }
    }
}
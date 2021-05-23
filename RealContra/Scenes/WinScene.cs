using Ungine;
using RealContra.Backgrounds;
using SFML.Window;

namespace RealContra.Scenes
{
    public class WinScene : GameScene
    {
        private int timer = 50;

        public WinScene()
        {
            MusicController.StopMusic();
            AddToScene(new WinBackground1(Game.Width / 2, Game.Height / 2));
            AddToScene(new SimpleFire(10, 360));
            AddToScene(new SimpleFire(90, 320));
            AddToScene(new SimpleFire(200, 240));
            AddToScene(new SimpleFire(360, 100));
            AddToScene(new SimpleFire(430, 40));
            AddToScene(new SimpleFire(450, 210));
            AddToScene(new SimpleFire(640, 340));
            AddToScene(new Fire(460, 310));
            AddToScene(new Fire(270, Game.Height - 90));
            AddToScene(new Fire(600, 310));
            AddToScene(new Explosion(270, 320));
            AddToScene(new Explosion(400, 350));
            AddToScene(new Explosion(645, 350));
            AddToScene(new WinBackground2(Game.Width / 2, Game.Height / 2));
            AddToScene(new BlinkingTextObject("You won!", Game.Width / 2 - 170, 150, 7) {Size = 40});
        }

        public override void OnEachFrame()
        {
            if (timer == 0)
                AddToScene(new BlinkingTextObject("Press any key", Game.Width / 2 - 200, Game.Height - 50, 5)
                    {Size = 30});
            timer--;
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
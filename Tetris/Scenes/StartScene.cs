using Ungine;
using SFML.Window;

namespace Tetris
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
            var startScreen = new Background("Art/splashscreen.png", Game.Width / 2f, Game.Height / 2f);
            var textOnScreen = new BlinkingTextObject("Press Space", Game.Width / 2f - 110, 450) { Size = 20 };

            AddToScene(startScreen, textOnScreen);

            MusicController.PlayMusic("Music/music.ogg");
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            if (key == Keyboard.Key.Space)
                Game.SetCurrentScene(new MainScene());
            if (key == Keyboard.Key.Escape) 
                    Game.Close();
        }
    }
}
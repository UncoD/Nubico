using csharp_sfml_game_framework;
using SFML.Window;

namespace Tetris
{
    public class LoseScene : GameScene
    {
        public LoseScene(string text)
        {
            var screen = new Background("Art/splashscreen.png", Game.Width / 2f, Game.Height / 2f);
            var textObject = new BlinkingTextObject(text, Game.Width / 2f - 120, 450) { Size = 16 };

            AddToScene(screen, textObject);
                       
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
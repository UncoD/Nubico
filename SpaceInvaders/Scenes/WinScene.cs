using csharp_sfml_game_framework;
using SFML.Window;

namespace SpaceInvaders
{
    public class WinScene : GameScene
    {
        public WinScene()
        {
            var wlScreen = new Background("Art/splashscreen.png", Game.Width / 2f, Game.Height / 2f);
            var textWl = new BlinkingTextObject($"You win! Score {Game.Score}", 30, 380, 16);
            
            AddToScene(wlScreen, textWl);
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            if (pressedKey == Keyboard.Key.Space)
            {
                Game.SetCurrentScene(new MainScene());
            }
        }
    }
}
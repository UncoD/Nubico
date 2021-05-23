using Ungine;
using SFML.Window;

namespace SpaceInvaders
{
    public class LoseScene : GameScene
    {
        public LoseScene()
        {
            var screen = new Background("Art/splashscreen.png", Game.Width / 2f, Game.Height / 2f);
            var textObject = new BlinkingTextObject($"You lose! Score {Game.Score}", 30, 380, 16);
            
            AddToScene(screen, textObject);
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
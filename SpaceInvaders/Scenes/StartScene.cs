using Ungine;
using SFML.Window;

namespace SpaceInvaders
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
            var startScreen = new Background("Art/splashscreen.png", Game.Width / 2f, Game.Height / 2f);
            var textOnScreen = new BlinkingTextObject("Press Space", 130, 380, 20);

            AddToScene(startScreen, textOnScreen);
        }
        
        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            if (pressedKey == Keyboard.Key.Space)
            {
                var gameScene = new MainScene();
                Game.SetCurrentScene(gameScene);
            }
        }
    }
}
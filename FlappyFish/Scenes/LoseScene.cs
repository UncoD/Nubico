using csharp_sfml_game_framework;
using SFML.Window;

namespace FlappyFish
{
    public class LoseScene : GameScene
    {
        public LoseScene()
        {
            var screen = new Background("Art/ocean.jpg", Game.Width / 2f, Game.Height / 2f);
            var textObject = new BlinkingTextObject($"Your score is {Game.Score}", 30, 380) {Size = 16};
            
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

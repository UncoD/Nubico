using csharp_sfml_game_framework;
using SFML.Window;

namespace FlappyFish
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
            var startScreen = new Background("Art/ocean.jpg", Game.Width / 2f, Game.Height / 2f);
            var textOnScreen = new BlinkingTextObject("Press Space", 40, 380) {Size = 20};

            AddToScene(startScreen, textOnScreen);

            Game.SoundController.PlaySound("Music/stage_2.ogg");
        }
        
        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            if (key == Keyboard.Key.Space)
            {
                Game.SetCurrentScene(new MainScene());
            }
        }
    }
}

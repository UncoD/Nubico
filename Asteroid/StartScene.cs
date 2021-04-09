using csharp_sfml_game_framework;
using SFML.System;
using SFML.Window;

namespace Asteroid
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
            var startScreen = new GameObject(Game.Width / 2f, Game.Height / 2f, "Art/start.jpg");
            startScreen.Scale = new Vector2f(0.77f, 0.77f);

            AddToScene(startScreen);
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            if (key == Keyboard.Key.Space) Game.SetCurrentScene(new SpaceScene());
        }
    }
}
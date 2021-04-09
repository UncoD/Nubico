using csharp_sfml_game_framework;
using SFML.System;
using SFML.Window;

namespace Asteroid
{
    public class VictoryScene : GameScene
    {
        public VictoryScene()
        {
            var victoryScreen = new GameObject(Game.Width / 2f, Game.Height / 2f, "Art/victory.jpg");
            victoryScreen.Scale = new Vector2f(1f, 1f);

            AddToScene(victoryScreen);
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            if (key == Keyboard.Key.Space) Game.SetCurrentScene(new StartScene());
        }
    }
}
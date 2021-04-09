using csharp_sfml_game_framework;
using SFML.System;
using SFML.Window;

namespace Asteroid
{
    public class DefeatScene : GameScene
    {
        public DefeatScene()
        {
            var defeatScreen = new GameObject(Game.Width / 2f, Game.Height / 2f, "Art/defeat.jpg");
            defeatScreen.Scale = new Vector2f(1.85f, 1.85f);

            AddToScene(defeatScreen);
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            if (key == Keyboard.Key.Space) Game.SetCurrentScene(new StartScene());
        }
    }
}
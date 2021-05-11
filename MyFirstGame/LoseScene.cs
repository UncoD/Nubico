using csharp_sfml_game_framework;
using SFML.Graphics;
using SFML.Window;

namespace MyFirstGame
{
    public class LoseScene : GameScene
    {
        public LoseScene()
        {
            var helloText = new HelloText("Game Over!", 200, 100);
            helloText.Size = 30;
            helloText.SetColor(Color.Red);

            var restartText = new HelloText("Press R to restart", 300, 180);
            restartText.SetColor(Color.Magenta);

            AddToScene(helloText, restartText);
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            switch (pressedKey)
            {
                case Keyboard.Key.Escape:
                    Game.Close();
                    break;
                case Keyboard.Key.R:
                    Game.SetCurrentScene(new MyScene());
                    break;
            }
        }
    }
}
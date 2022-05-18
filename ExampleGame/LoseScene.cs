using Nubico.GameBase;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ExampleGame
{
    public class LoseScene : GameScene
    {
        public LoseScene()
        {
            var helloText = new HelloText("Game Over!", 0, 100);
            helloText.Size = 30;
            helloText.Position = new Vector2f(Game.Width / 2, 100);
            helloText.SetColor(Color.Red);

            var restartText = new RestartBtn("Click on this to restart", Game.Width / 2, 180);
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
            }
        }

        private class RestartBtn : HelloText {
            public RestartBtn(string text, float x, float y) : base(text, x, y) {}

            public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool IsAlreadyClicked)
            {
                if (mouseButton == Mouse.Button.Left && HoverOnThis())
                {
                    Game.SetCurrentScene(new MyScene(5));
                }
            }
        }
    }
}
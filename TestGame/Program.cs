using Nubico.GameBase;
using Nubico.Objects;
using SFML.System;
using SFML.Window;

namespace TestGame
{
    public class MyScene : GameScene
    {
        public MyScene()
        {
            var text = new TextObject("Text", 200, 100);
            AddToScene(text);
        }

        public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked)
        {
            if (!isAlreadyClicked)
            {
                var pobj = new PhysicsObject(position.X / 100, position.Y / 100);
                AddToScene(pobj);
            }
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var game = new Game(800, 600, "My game");
            game.SetCurrentScene(new MyScene());
            game.Start();
        }
    }
}
using Nubico.GameBase;
using Nubico.Objects;
using SFML.System;
using SFML.Window;

namespace ExamplePhysicsGame
{
    class Program
    {
        public class MyScene : GameScene
        {
            public MyScene()
            {
                var obj = new PhysicsObject(600, 400, true, true);
                AddToScene(obj);
            }

            public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked)
            {
                if (!isAlreadyClicked)
                {
                    var obj = new PhysicsObject(position.X, position.Y, false, false);
                    AddToScene(obj);
                }
            }
        }

        static void Main(string[] args)
        {
            var game = new Game(1200, 800, "Example Physics Game");
            game.DrawObjectBorders = true;
            game.SetCurrentScene(new MyScene());
            game.Start();
        }
    }
}
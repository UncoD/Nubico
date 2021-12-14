using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using Nubico.GameBase;
using Nubico.Objects;
using SFML.Graphics;
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
                var obj = new PhysicsObject(4, 3);
                AddToScene(obj);
            }

            public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked)
            {
                if (!isAlreadyClicked)
                {
                    var obj = new PhysicsObject(position.X / 100, position.Y / 100);
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
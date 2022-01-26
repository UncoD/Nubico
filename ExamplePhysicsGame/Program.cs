using Nubico.GameBase;
using Nubico.Objects.Physics;
using Nubico.Objects.Physics.Shapes;
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
                for (int i = 0; i < 10; i++)
                {
                    var body = new RectangleBody
                    (
                        new Vector2f(96, 96),
                        new Vector2f(200 + i * 96, 700),
                        new BodyParams { Density = 1, Friction = 0.3f, Restitution = 0.1f },
                        true
                    );
                    var obj = new PhysicsObject(body);
                    obj.Scale = new Vector2f(8, 8);
                    AddToScene(obj);
                }
            }

            public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
            {
                if (!isAlreadyPressed)
                    Game.DrawObjectBorders = !Game.DrawObjectBorders;
            }

            public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked)
            {
                switch (mouseButton)
                {
                    case Mouse.Button.Left when !isAlreadyClicked:
                    {
                        var body = new CircleBody
                        (
                            60, (Vector2f)position,
                            new BodyParams { Density = 1, Friction = 0.3f, Restitution = 0.4f }
                        );
                        var obj = new PhysicsObject(body);
                        obj.Scale = new Vector2f(6, 6);
                        AddToScene(obj);
                        break;
                    }
                    case Mouse.Button.Right when !isAlreadyClicked:
                    {
                        var body = new RectangleBody
                        (
                            new Vector2f(10, 90), (Vector2f)position,
                            new BodyParams { Density = 1, Friction = 0.1f, Restitution = 0.1f }
                        );
                        var obj = new PhysicsObject(body);
                        AddToScene(obj);
                        break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            var game = new Game(1200, 800, "Example Physics Game")
            {
                DrawObjectBorders = true
            };
            game.SetCurrentScene(new MyScene());
            game.Start();
        }
    }
}
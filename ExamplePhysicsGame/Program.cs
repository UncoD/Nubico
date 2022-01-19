using Nubico.GameBase;
using Nubico.Objects;
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
            private bool isStatic = true;
            private readonly TextObject text;

            public MyScene()
            {
                var body = new RectangleBody(new Vector2f(600, 20), new Vector2f(600, 700), true);
                var obj = new PhysicsObject(body);
                AddToScene(obj);

                text = new TextObject(isStatic ? "Static" : "Dynamic", 50, 50);
                AddToScene(text);
            }
            
            public override void OnMouseClick(Mouse.Button mouseButton, Vector2i position, bool isAlreadyClicked)
            {
                switch (mouseButton)
                {
                    case Mouse.Button.Left when !isAlreadyClicked:
                    {
                        var body = new CircleBody(0.5f, (Vector2f)position, isStatic);
                        var obj = new PhysicsObject(body);
                        AddToScene(obj);
                        break;
                    }
                    case Mouse.Button.Right when !isAlreadyClicked:
                        isStatic = !isStatic;
                        text.SetText(isStatic ? "Static" : "Dynamic");
                        break;
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
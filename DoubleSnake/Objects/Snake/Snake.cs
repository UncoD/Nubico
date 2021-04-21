using csharp_sfml_game_framework;
using MyFirstGame.Scenes;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace MyFirstGame.Objects
{
    class Snake : GameObject
    {
        string pathToHeadSprite;
        string pathToBodySprite;
        int startLength = 3;
        LinkedList<SnakePart> parts = new LinkedList<SnakePart>();
        int speed = 2;
        int timer = 2000;
        int startTimer = 2000;
        Vector2i direction = new Vector2i(0, -1);
        bool directionChoosen = false;
        public bool ArrowControl { get; private set; } = false;

        Play scene;

        public Snake(float x, float y, string pathToHeadSprite,
            string pathToBodySprite, bool arrowControl, Play parentScene) : base(x, y)
        {
            scene = parentScene;
            ArrowControl = arrowControl;
            for (int i = 0; i < startLength; i++)
            {
                var part = new SnakePart(x, y, i == 0 ? pathToHeadSprite : pathToBodySprite, this, i == 0);
                part.MoveIt(0, i * part.Height * 3);
                parts.AddLast(part);
            }

            foreach (var part in parts)
            {
                GameScene.AddToScene(part);
            }

            this.pathToHeadSprite = pathToHeadSprite;
            this.pathToBodySprite = pathToBodySprite;
        }

        public override void OnEachFrame()
        {
            timer -= speed * 100;
            if (timer <= 0)
            {
                var oldHead = parts.First.Value;
                var newHead = new SnakePart(oldHead.Position.X + direction.X * oldHead.Height,
                    oldHead.Position.Y + direction.Y * oldHead.Height, pathToHeadSprite, this, true);
                oldHead.SetSprite(pathToBodySprite);
                oldHead.IsHead = false;
                parts.AddFirst(newHead);
                GameScene.AddToScene(newHead);
                var tail = parts.Last.Value;
                tail.DeleteFromGame();
                parts.RemoveLast();

                timer = startTimer;

                directionChoosen = false;
            }
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            if (directionChoosen) return;

            if (((pressedKey == Keyboard.Key.W && !ArrowControl)
                || (pressedKey == Keyboard.Key.Up && ArrowControl)) && direction.Y != 1)
            {
                direction = new Vector2i(0, -1);
            } else if (((pressedKey == Keyboard.Key.S && !ArrowControl)
                || (pressedKey == Keyboard.Key.Down && ArrowControl)) && direction.Y != -1)
            {
                direction = new Vector2i(0, 1);
            }
            else if (((pressedKey == Keyboard.Key.A && !ArrowControl)
                || (pressedKey == Keyboard.Key.Left && ArrowControl)) && direction.X != 1)
            {
                direction = new Vector2i(-1, 0);
            }
            else if (((pressedKey == Keyboard.Key.D && !ArrowControl)
                || (pressedKey == Keyboard.Key.Right && ArrowControl)) && direction.X != -1)
            {
                direction = new Vector2i(1, 0);
            }

            directionChoosen = true;
        }

        public void AddPart()
        {
            var last = parts.Last.Value;
            var penult = parts.Last.Previous.Value;
            var dx = penult.Position.X - last.Position.X < 0 ? -1 : penult.Position.X - last.Position.X > 0 ? 1 : 0;
            var dy = penult.Position.Y - last.Position.Y < 0 ? -1 : penult.Position.Y - last.Position.Y > 0 ? 1 : 0;
            var part = new SnakePart(last.Position.X - dx * last.Height,
                    last.Position.Y - dy * last.Height, pathToBodySprite, this, false);
            parts.AddLast(part);
            GameScene.AddToScene(part);
        }

        public void GameOver()
        {
            Game.SetCurrentScene(new Menu(false, true, ArrowControl));
        }

        public void Draw()
        {
            Game.SetCurrentScene(new Menu(true, true));
        }
    }
}

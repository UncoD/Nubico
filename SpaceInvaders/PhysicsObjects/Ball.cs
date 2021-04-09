using System;
using System.Collections.Generic;
using csharp_sfml_game_framework;
using SFML.Graphics;
using SFML.System;

namespace SpaceInvaders
{
    public class Ball : PhysicsObject
    {
        private readonly List<GameObject> tail;
        private readonly Player owner;
        
        public Ball(string pathToSprite, string tailPath, float x, float y, Player owner) : base(x, y, pathToSprite)
        {
            this.owner = owner;
            SpeedX = new Random().Next() % 7 - 3;
            SpeedY = -4;
            Health = 4;
            tail = new List<GameObject>();
            
            var posX = x - Width / 6f * SpeedX;
            var posY = y - Height / 6f * SpeedY;
            var scale = Scale;
            for (var i = 0; i < Health - 1; i++)
            {
                var nextTail = new GameObject(posX, posY,tailPath)
                {
                    Scale = new Vector2f(scale.X * 2 / 3, scale.Y * 2 / 3)
                };
                nextTail.SetSpriteColor(new Color(255, 255, 255, (byte) ((Health - i - 1) * 70)));

                posX -= nextTail.Width / 6 * SpeedX;
                posY -= nextTail.Height / 6 * SpeedY;
                scale = nextTail.Scale;
                
                tail.Add(nextTail);
                GameScene.AddToScene(nextTail);
            }
        }

        public override void OnEachFrame()
        {
            MoveIt(SpeedX, SpeedY);
            
            if (X + Width / 2 >= Game.Width || X - Width / 2 <= 0)
            {
                SpeedX = -SpeedX;
            }
            
            if (Y - Height / 2 <= 0 )
            {
                SpeedY = -SpeedY;
            }
            
            if (Y + Height / 2 >= Game.Height)
            {
                Game.OnLose();
            }
            
            SetPosForTail();
        }

        private void SetPosForTail()
        {
            var posX = X - Width / 5 * SpeedX;
            var posY = Y - Height / 5 * SpeedY;

            foreach (var ball in tail)
            {
                ball.Position = new Vector2f(posX, posY);

                posX -= ball.Width / 3 * SpeedX;
                posY -= ball.Width / 3 * SpeedY;

                ball.OnEachFrame();
            }
        }

        public override void OnCollide(GameObject collideObject)
        {
            if (collideObject is Player && Y + Height / 2f <= collideObject.Y - collideObject.Height / 6f)
            {
                SpeedY = -SpeedY;
            }
            
            if (collideObject is Bullet bullet)
            {
                switch (collideType)
                {
                    case CollideType.Horizontal:
                        SpeedX = -SpeedX;
                        break;
                    case CollideType.Vertical:
                        SpeedY = -SpeedY;
                        break;
                }

                bullet.DeleteFromGame();
                
                Health--;
                if (tail.Count > 0)
                {
                    tail[tail.Count - 1].DeleteFromGame();
                    tail.RemoveAt(tail.Count - 1);
                }

                if (Health <= 0)
                {
                    DeleteFromGame();
                    owner.Store++;
                }
            }

            if (collideObject is Invader invader)
            {
                if (collideType == CollideType.Horizontal)
                {
                    SpeedX = -SpeedX;
                }
                
                if (collideType == CollideType.Vertical)
                {
                    SpeedY = -SpeedY;
                }

                invader.Health--;
                
                Health--;
                if (tail.Count > 0)
                {
                    tail[tail.Count - 1].DeleteFromGame();
                    tail.RemoveAt(tail.Count - 1);
                }

                if (Health <= 0)
                {
                    DeleteFromGame();
                    owner.Store++;
                }
            }
        }
    }
}
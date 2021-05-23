using System;
using System.Linq;
using Ungine;
using SFML.Graphics;

namespace SpaceInvaders
{
    public class Invader : PhysicsObject
    {
        public bool IsMoveLeft = true;
        public readonly bool IsHunter;
        private int Level { get; }
        
        public Invader(string pathToSprite, float x, float y, bool isHunter) : base(x, y, pathToSprite)
        {
            Health = new Random().Next() % 3 + 2;
            Level = Health;
            IsHunter = isHunter;
        }

        public override void OnEachFrame()
        {
            SetSpriteColor(new Color(255, 255, 255, (byte) (Health * (255 / Level))));
            
            if (IsMoveLeft)
            {
                MoveIt(-SpeedX, 0);
            }
            else
            {
                MoveIt(SpeedX, 0);
            }

            if (Health <= 0)
            {
                Game.Score += 100 * GameScene.GameObjects.Count(o => o is Ball) * Level;
                DeleteFromGame();
                SoundController.PlaySound("Sound/killinvader.wav");
            }
        }

        public override void OnCollide(GameObject collideObject)
        {
            if (collideObject is Bunker)
            {
                DeleteFromGame();
            }

            if (collideObject is Player)
            {
                Game.OnLose();
            }
        }
    }
}
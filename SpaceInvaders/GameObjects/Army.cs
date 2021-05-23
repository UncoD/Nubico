using System;
using System.Collections.Generic;
using System.Linq;
using Ungine;

namespace SpaceInvaders
{
    /// <summary>
    /// Армия Пришельцев, координирующая их действия
    /// </summary>
    public class Army : GameObject
    {
        public readonly List<Invader> Invaders;
        private readonly Random rand = new Random();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">Число Пришельцев</param>
        /// <param name="game"></param>
        public Army(int count) : base(0, 0)
        {
            Invaders = new List<Invader>();

            // Пути к текстурам Пришельцев
            var pathsInvaders = new List<(string path, bool isHunter)>
            {
                ("Art/invader_000.png", false),
                ("Art/invader_001.png", false),
                ("Art/invader_002.png", true),
                ("Art/invader_003.png", false),
                ("Art/invader_004.png", true),
                ("Art/invader_005.png", true)
            };

            // Размещение Пришельцев на поле
            InvadersPlacing(count, pathsInvaders);
        }

        private void InvadersPlacing(int count, List<(string path, bool isHunter)> pathsInvaders)
        {
            var invaderY = 110;
            var invaderX = 35;
            for (var i = 0; i < count; i++)
            {
                var type = rand.Next() % pathsInvaders.Count;
                var invader = new Invader(pathsInvaders[type].path, invaderX, invaderY, pathsInvaders[type].isHunter)
                {
                    SpeedX = 0.4f,
                    SpeedY = 10
                };
                Invaders.Add(invader);
                GameScene.AddToScene(invader);

                invaderX += 60;
                if (invaderX > Game.Width - 60)
                {
                    invaderY -= 45;
                    invaderX = 35;
                }
            }
        }

        public override void OnEachFrame()
        {
            if (Invaders.Any(i => i.X - i.Width / 2 <= 0 || i.X + i.Width / 2 >= Game.Width))
            {
                foreach (var invader in Invaders)
                {
                    invader.IsMoveLeft = !invader.IsMoveLeft;
                    invader.MoveIt(0, invader.SpeedY);
                }
            }
            
            foreach (var invader in Invaders)
            {
                invader.OnEachFrame();
                if (invader.Y + invader.Height >= Game.Height)
                {
                    Game.OnLose();
                }
            
                if (invader.IsHunter && rand.NextDouble() <= 0.001)
                {
                    GameScene.AddToScene(new Bullet("Art/missile.png", invader.X, invader.Y + invader.Height / 2));
                }

                if (invader.IsBroken && rand.NextDouble() <= 0.5)
                {
                    GameScene.AddToScene(new Bonus("Art/platform_bonus_000.png", invader.X, invader.Y));
                }
            }

            Invaders.RemoveAll(i => i.IsBroken);

            if (Invaders.Count == 0)
            {
                Game.OnWin();
            }
        }
    }
}
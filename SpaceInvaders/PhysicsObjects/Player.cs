using System.Collections.Generic;
using csharp_sfml_game_framework;
using SFML.Window;

namespace SpaceInvaders
{
    /// <summary>
    /// Игровая доска, которая может выстреливать и отбивать энергетические шары
    /// </summary>
    public class Player : PhysicsObject
    {
        public int Store = 3;
        private bool CanShoot { get; set; }
        private int rechargeTime;
        private int rechargeSpeed = 50;
        // TODO GFF: + Переименовать в TexturePath...
        private List<string> texturePathsFull;
        private List<string> texturePathsEmpty;
        private List<string> texturePathsRebound;
        private int maxHealth;

        public Player(string pathToSprite, float x, float y) : base(x, y, pathToSprite)
        {
            Health = 1;
            maxHealth = 3;

            // Прописываем текстуры для разных состояний доски
            texturePathsFull = new List<string>
            {
                "Art/platform_000.png",
                "Art/platform_001.png",
                "Art/platform_002.png"
            };

            texturePathsEmpty = new List<string>
            {
                "Art/platform_009.png",
                "Art/platform_010.png",
                "Art/platform_011.png"
            };

            texturePathsRebound = new List<string>
            {
                "Art/platform_012.png",
                "Art/platform_013.png",
                "Art/platform_014.png"
            };
        }

        public override void OnEachFrame()
        {
            rechargeTime++;
            
            if (rechargeTime == rechargeSpeed)
            {
                rechargeTime = 0;
            }

            if (rechargeTime == 0 && Store > 0)
            {
                CanShoot = true;
                SetSprite(texturePathsFull[Health - 1]);
            }

            if (Store > 3)
            {
                Store = 3;
            }

            base.OnEachFrame();
        }
        
        /// <summary>
        /// Реакция доски на нажатие клавиш
        /// </summary>
        /// <param name="pressedKey"></param>
        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            if (pressedKey == Keyboard.Key.Left && X - Width / 2 > 0)
            {
                MoveIt(-6, 0);
            }

            if (pressedKey == Keyboard.Key.Right && X + Width / 2 < Game.Width)
            {
                MoveIt(6, 0);
            }

            if (pressedKey == Keyboard.Key.Space && CanShoot && Store > 0)
            {
                CanShoot = false;
                Store--;
                var ball = new Ball("Art/ball_000.png", "Art/ball_001.png", X, Y - Height, this);
                GameScene.AddToScene(ball);
                SoundController.PlaySound("Sound/bullet.wav");

                // TODO GFF: + Убирать подсветку с платформы, когда Стор = 0
                if (Store <= 0)
                {
                    SetSprite(texturePathsEmpty[Health - 1]);
                }
            }
        }

        // TODO GFF: + Переименовать в collideObject
        /// <summary>
        /// Действия при столкновении с Доской
        /// </summary>
        /// <param name="collideObject"></param>
        public override void OnCollide(GameObject collideObject)
        {
            if (collideObject is Bonus)
            {
                // TODO GFF: + Переделать в maxHealth
                if (Health < maxHealth)
                {
                    Health++;
                }

                SetSprite(texturePathsFull[Health - 1]);
            }

            if (collideObject is Bullet)
            {
                Health--;

                if (Health <= 0)
                {
                    Game.OnLose();
                }
                else
                {
                    SetSprite(texturePathsFull[Health - 1]);
                }

            }

            if (collideObject is Ball)
            {
                SetSprite(texturePathsRebound[Health - 1]);
            }
        }
    }
}
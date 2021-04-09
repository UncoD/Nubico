using System;

namespace jRPG
{
    class BattleMob : BattleUnit
    {
        private bool finalBoss;

        public BattleMob(float x, float y, int health, int attack, int speed, string pathToTexture, bool finalBoss = false) : base(x, y, health, attack, speed)
        {
            SetSprite(pathToTexture);
            DrawPriority = 5;
            Scale = new SFML.System.Vector2f(1, 1);
            Scale = new SFML.System.Vector2f(1, 1);
            this.finalBoss = finalBoss;
        }

        public override void Attack()
        {
            Projectile projectile = new Projectile(X, Y, opponent.X, opponent.Y, mapScene, "Art/attackr.png");
            mapScene.AddToScene(projectile);
            base.Attack();
        }

        public override void OnEachFrame()
        {
            if (IsReady()) {
                int r = new Random().Next(3);
                if (r == 0) {
                    Attack();
                } else if (r == 1) {
                    Heal();
                } else if (r == 2) {
                    Block();
                }
            }
            base.OnEachFrame();
        }

        public bool isFinalBoss() {
            return finalBoss;
        }
    }
}

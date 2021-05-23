using Ungine;
using SFML.Window;

namespace jRPG
{
    class BattlePlayer : BattleUnit
    {
        public BattlePlayer(int x, int y, int level) : base(x, y, 100 + level, 12 + level / 2, 1 + level)
        {
            SetSprite("Art/player.png");
            DrawPriority = 5;
            Scale = new SFML.System.Vector2f(1, 1);
            Scale = new SFML.System.Vector2f(1, 1);
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            base.OnKeyPress(pressedKey, isAlreadyPressed);
            if (!IsReady())
            {
                return;
            }
            if (pressedKey == Keyboard.Key.Z)
            {
                Attack();
            }
            else if (pressedKey == Keyboard.Key.X)
            {
                Heal();
            }
            else if (pressedKey == Keyboard.Key.C)
            {
                Block();
            }
        }

        public override void Attack()
        {
            Projectile projectile = new Projectile(X, Y, opponent.X, opponent.Y, mapScene, "Art/attackl.png");
            mapScene.AddToScene(projectile);
            base.Attack();
        }
    }
}

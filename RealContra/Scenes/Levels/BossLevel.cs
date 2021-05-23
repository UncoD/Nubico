using Ungine;
using RealContra.Backgrounds;

namespace RealContra
{
    internal class BossLevel : GameScene
    {
        public BossLevel(int health)
        {
            AddToScene(new Background(Game.Width / 2f, Game.Height / 2f));
            AddToScene(new Grass(Game.Width / 2f, Game.Height - 16, 9));
            AddToScene(new Stone(10, Game.Height / 2f, 6));
            AddToScene(new Stone(Game.Width - 10, Game.Height / 2f, 6));

            AddToScene(new Grass(80, Game.Height / 8 * 5+50, 1));
            AddToScene(new Grass(Game.Width / 2, Game.Height / 8 * 5 + 50, 1));
            AddToScene(new Grass(Game.Width - 80, Game.Height / 8 * 5 + 50, 1));
            AddToScene(new Stone(Game.Width / 3, Game.Height / 4 * 3 + 80, 1));
            AddToScene(new Grass(Game.Width / 3, Game.Height / 4 * 3 + 50, 1));
            AddToScene(new Stone(Game.Width / 3 * 2, Game.Height / 4 * 3 + 80, 1));
            AddToScene(new Grass(Game.Width / 3 * 2, Game.Height / 4 * 3 + 50, 1));

            AddToScene(new Helicopter(Game.Width / 2, 70));
            AddToScene(new Man(110, Game.Height / 2f + 100, health));
            for (var i = 0; i < 10; i++)
                AddToScene(new Heart(50 + i * 28, 50, 0));
            for (var i = 0; i < health; i++)
                AddToScene(new Heart(50 + i * 28, 50, 1));
        }
    }
}
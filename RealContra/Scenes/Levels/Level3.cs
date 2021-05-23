using Ungine;
using RealContra.Backgrounds;

namespace RealContra
{
    internal class Level3 : GameScene
    {
        public bool flag = true;
        public Man man;
        public FireMan[] FM;
        public RunMan[] RM;
        public Stone wall1;
        public Stone wall2;

        public Level3(int health)
        {
            AddToScene(new Background(Game.Width / 2f, Game.Height / 2f));
            AddToScene(new Grass(Game.Width / 2f, Game.Height - 16, 9));
            wall1 = new Stone(10, Game.Height / 2f, 6);
            wall2 = new Stone(Game.Width - 10, Game.Height / 2f, 6);
            AddToScene(wall1, wall2);

            AddToScene(new Grass(Game.Width / 3 + 50, Game.Height / 4, 5));//1
            AddToScene(new Grass(Game.Width - 80, Game.Height / 4, 1));//2
            AddToScene(new Grass(Game.Width / 4, Game.Height / 2, 4));//3
            AddToScene(new Grass(Game.Width / 4 * 3 + 55, Game.Height / 2 + 16, 3));//4
            AddToScene(new Grass(80, Game.Height / 4 * 3, 1));//5
            AddToScene(new Grass(Game.Width / 4 * 3, Game.Height / 4 * 3, 5));//6
            AddToScene(new Grass(Game.Width / 8 * 5, Game.Height / 4 + 65, 1));//7
            AddToScene(new Grass(Game.Width / 2 + 62, Game.Height / 2 + 92, 1));//8
            AddToScene(new Stone(Game.Width / 3 + 30, Game.Height - 40, 1));
            AddToScene(new Grass(Game.Width / 3 + 30, Game.Height - 80, 1));//9

            FM = new FireMan[]
            {new FireMan(Game.Width/4,Game.Height/4-50,"right"),//1
             new FireMan(Game.Width-80,Game.Height/4-50,"left"),//2
             new FireMan(80,Game.Height/2-50,"right"),//3
             new FireMan(Game.Width-80,Game.Height/2-34,"left"),//4
             new FireMan(80,Game.Height/4*3-50,"right"),//5
             new FireMan(Game.Width-80,Game.Height/4*3-50,"left"),//6
             new FireMan(Game.Width-80,Game.Height-60,"left")};//7
            AddToScene(FM);

            RM = new RunMan[]
            {new RunMan(Game.Width/3+100,Game.Height-60,Game.Width-80),
             new RunMan(Game.Width/2+62,Game.Height/4*3-50,Game.Width-80),
             new RunMan(Game.Width/8*5+70,Game.Height/2-34,Game.Width/4*3+55),
             new RunMan(Game.Width/4,Game.Height/4-50,Game.Width/2)};
            AddToScene(RM);

            man = new Man(110, 400, health);
            AddToScene(man);
            for (var i = 0; i < 10; i++)
                AddToScene(new Heart(50 + i * 28, 50, 0));
            for (var i = 0; i < health; i++)
                AddToScene(new Heart(50 + i * 28, 50, 1));
        }

        public override void OnEachFrame()
        {
            if (FM[0].Health == 0 && FM[1].Health == 0 && FM[2].Health == 0 &&
                FM[3].Health == 0 && FM[4].Health == 0 && FM[5].Health == 0 &&
                FM[6].Health == 0 && RM[0].Health == 0 && RM[1].Health == 0 &&
                RM[2].Health == 0 && RM[3].Health == 0)
            {
                wall1.DeleteFromGame();
                wall2.DeleteFromGame();
                if (flag)
                {
                    flag = false;
                    AddToScene(new Arrow(70, 350, "left"));
                    AddToScene(new Arrow(650, 350, "right"));
                }

                if (man.X > Game.Width || man.X < 0)
                    Game.SetCurrentScene(new BossLevel(man.Health));
            }

            base.OnEachFrame();
        }
    }
}
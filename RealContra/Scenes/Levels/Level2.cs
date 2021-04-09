using csharp_sfml_game_framework;
using RealContra.Backgrounds;

namespace RealContra
{
    internal class Level2 : GameScene
    {
        public bool flag = true;
        public Man man;
        public FireMan[] FM;
        public RunMan[] RM;
        public Stone wall1;
        public Stone wall2;

        public Level2(int health)
        {
            AddToScene(new Background(Game.Width / 2f, Game.Height / 2f));
            AddToScene(new Grass(Game.Width / 2f, Game.Height - 16, 9));
            wall1 = new Stone(10, Game.Height / 2f, 6);
            wall2 = new Stone(Game.Width - 10, Game.Height / 2f, 6);
            AddToScene(wall1, wall2);

            AddToScene(new Grass(Game.Width / 4, Game.Height / 4, 5));//1
            AddToScene(new Grass(Game.Width / 4, Game.Height / 2, 1));//2
            AddToScene(new Grass(Game.Width / 2, Game.Height / 2, 1));//3
            AddToScene(new Grass(Game.Width / 4 * 3, Game.Height / 2, 1));//4
            AddToScene(new Grass(Game.Width / 2, Game.Height / 4 * 3, 5));//5
            AddToScene(new Grass(Game.Width / 8 * 7, Game.Height / 8 * 7, 1));//6
            AddToScene(new Grass(Game.Width / 8 * 3, Game.Height / 8 * 5, 1));//7
            AddToScene(new Grass(Game.Width / 2 + 70, Game.Height / 4 + 45, 1));//8

            RM = new RunMan[]
            {new RunMan(80,Game.Height-50,Game.Width-80),
             new RunMan(80,Game.Height-50,Game.Width/3),
             new RunMan(Game.Width/3*2,Game.Height-50,Game.Width-80),
             new RunMan(Game.Width/4+50,Game.Height/4*3-30,Game.Width/4*3-50),
             new RunMan(Game.Width/4,Game.Height/4-50,Game.Width/2-30)};
            FM = new FireMan[]
            {new FireMan(Game.Width/4,Game.Height/2-50,"right"),
             new FireMan(Game.Width/4*3,Game.Height/2-50,"left"),
             new FireMan(Game.Width/4*3-50,Game.Height/4*3-38,"left")};
            AddToScene(RM);
            AddToScene(FM);

            man = new Man(110, 50, health);
            AddToScene(man);
            for (var i = 0; i < 10; i++)
                AddToScene(new Heart(50 + i * 28, 50, 0));
            for (var i = 0; i < health; i++)
                AddToScene(new Heart(50 + i * 28, 50, 1));
        }

        public override void OnEachFrame()
        {
            if (FM[0].Health == 0 && FM[1].Health == 0 && FM[2].Health == 0 &&
                RM[0].Health == 0 && RM[1].Health == 0 && RM[2].Health == 0 &&
                RM[3].Health == 0 && RM[4].Health == 0)
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
                    Game.SetCurrentScene(new Level3(man.Health));
            }

            base.OnEachFrame();
        }
    }
}
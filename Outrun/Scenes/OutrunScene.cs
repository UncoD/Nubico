using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ungine;
using SFML.Graphics;
using SFML.System;

namespace Outrun
{
    public class OutrunScene : GameScene
    {
        private Random rand = new Random();
        private int timeOfCreation;
        private Car car;
        private Background chillBackground;
        private Background dangerBackground;
        private List<GameObject> carNitro = new List<GameObject>();
        private List<GameObject> carHealth = new List<GameObject>();
        private int time;
        private string mode = "chill";
        private BlinkingTextObject dangerText;

        public OutrunScene()
        {
            car = new Car(Game.Width / 2f, Game.Height - 45, "chill");
            chillBackground = new Background("chill", Game.Width / 2f, Game.Height / 2f);
            dangerBackground = new Background("danger", Game.Width / 2f, Game.Height / 2f) {DrawPriority = -1};
            dangerText = new BlinkingTextObject("DANGER ZONE!", Game.Width / 2f, 100, 30);
            dangerText.SetColor(Color.Magenta);
            AddToScene(dangerBackground, dangerText, chillBackground, car);

            for (var i = 0; i <= car.MaxNitro; i++)
            {
                var item = new GameObject(-100, -100, "Art/ui_001.png")
                {
                    Scale = new Vector2f(2, 2), DrawPriority = 52
                };
                carNitro.Add(item);
                AddToScene(carNitro[i]);
            }

            for (var i = 0; i <= car.Health; i++)
            {
                var item = new GameObject(50 + 50 * i, 50, "Art/ui_000.png")
                {
                    Scale = new Vector2f(2, 2), DrawPriority = 52
                };
                carHealth.Add(item);
                AddToScene(carHealth[i]);
            }

            MusicController.StopMusic();
            MusicController.PlayMusic("Music/ChillZone.ogg");
        }

        public override void OnEachFrame()
        {
            time += Car.Turbo;
            Debug.WriteLine(time);
            if (time >= 700 && time <= 1200)
            {
                chillBackground.SetSpriteColor(new Color(255, 255, 255, (byte) (15255 - (time - Car.Turbo + 1))));
                if (mode == "chill")
                {
                    mode = "danger";
                    MusicController.StopMusic();
                    MusicController.PlayMusic("Music/DangerZone.ogg");
                }
            }

            if (time > 1200 && !chillBackground.IsBroken)
            {
                chillBackground.DeleteFromGame();
                car.DangerMode(mode);
            }

            if (time > 1200 && !dangerText.IsBroken)
            {
                dangerText.DeleteFromGame();
            }

            if (time >= 1500)
            {
                Game.OnWin();
            }

            timeOfCreation = (timeOfCreation + 1) % 15;

            if (timeOfCreation == 0)
            {
                if (rand.NextDouble() <= 0.1)
                {
                    if (!(rand.NextDouble() >= 0.2))
                    {
                        AddToScene(new BrokenPalm(Game.Width / 2f, 280, mode));
                    }
                    else
                    {
                        AddToScene(new Palm(Game.Width / 2f, 280, mode));
                    }
                }

                if (rand.NextDouble() <= 0.015)
                {
                    var bonus = new RepairBonus(Game.Width / 2f, 280);
                    AddToScene(bonus);
                }
                else if (rand.NextDouble() <= 0.01)
                {
                    var bonus = new NitroBonus(Game.Width / 2f, 280);
                    AddToScene(bonus);
                }
                else if (rand.NextDouble() <= 0.09 * (mode == "danger" ? Car.Turbo : 1))
                {
                    if (rand.NextDouble() <= 0.3)
                    {
                        AddToScene(new BrokenCar(Game.Width / 2f, 280, mode));
                    }
                    else if (rand.NextDouble() <= 0.5)
                    {
                        AddToScene(new Puddle(Game.Width / 2f, 280, mode));
                    }
                    else if (mode == "danger")
                    {
                        AddToScene(new Barrel(Game.Width / 2f, 280, mode));
                    }
                }
                else if (mode == "danger" && rand.NextDouble() <= 0.1)
                {
                    AddToScene(new DeathRay(Game.Width / 2f, Game.Height / 2f));
                }
            }

            for (var i = 0; i < carNitro.Count - car.Nitro; i++)
            {
                carNitro[i].Position = new Vector2f(-100, -100);
            }

            for (var i = carNitro.Count - car.Nitro; i < carNitro.Count; i++)
            {
                carNitro[i].Position = new Vector2f(Game.Width - 200 + 50 * i, 50);
            }

            for (var i = car.Health; i < carHealth.Count; i++)
            {
                carHealth[i].Position = new Vector2f(-100, -100);
            }

            for (var i = 0; i < car.Health; i++)
            {
                carHealth[i].Position = new Vector2f(50 + 50 * i, 50);
            }
        }
    }
}
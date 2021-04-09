using System.Collections.Generic;
using csharp_sfml_game_framework;
using SFML.Window;

namespace Asteroid
{
    public class Car : PhysicsObject
    {
        private readonly List<string> carSprites;
        private int currentCar = 0;
        private int speed = 0;

        public Car(float x, float y) : base(x, y)
        {
            DrawPriority = 3;
            carSprites = new List<string>
            {
                "Art/Danger Zone Outrun/auto_000.png",
                "Art/Danger Zone Outrun/auto_001.png"
            };
        }

        public override void OnKeyPress(Keyboard.Key key)
        {
            switch (key)
            {
                case Keyboard.Key.Left:
                    MoveIt(-2, 0);
                    break;
                case Keyboard.Key.Right:
                    MoveIt(2, 0);
                    break;
            }
        }

        public override void OnEachFrame()
        {
            SetSprite(carSprites[currentCar]);

            if (speed == 0)
            {
                currentCar = (currentCar + 1) % 2;
            }
            speed = (speed + 1) % 60;

            base.OnEachFrame();
        }
    }
}
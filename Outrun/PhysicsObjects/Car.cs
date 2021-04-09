using csharp_sfml_game_framework;
using SFML.System;
using SFML.Window;

namespace Outrun
{
    public class Car : PhysicsObject
    {
        public static int Turbo = 1;

        private const int NitroTime = 150;
        private int nitroTimeCount;
        public int Nitro { get; private set; }
        public int MaxNitro { get; }

        private const int CrashTime = 50;
        private int crashTimeCount;
        private bool isLookingRight;
        private string mode;
        private State state = State.Ride;

        private enum State
        {
            Ride,
            Nitro,
            Crash
        }

        public Car(float x, float y, string mode) : base(x, y, $"Art/{mode}_run_000.png")
        {
            MaxNitro = 3;
            DrawPriority = 50;
            Health = 3;
            this.mode = mode;
            AddAnimation($"{mode}_car", 60, $"Art/{mode}_run_000.png", $"Art/{mode}_run_001.png");
            AddAnimation($"{mode}_nitro", 60, $"Art/{mode}_nitro_000.png", $"Art/{mode}_nitro_001.png");
            AddAnimation($"{mode}_crash", 0, $"Art/{mode}_crash_000.png");
            Scale = new Vector2f(2.1f, 2.1f);
            PlayAnimation($"{mode}_car");
        }

        public void DangerMode(string mode)
        {
            this.mode = mode;
            AddAnimation($"{mode}_car", 60, $"Art/{mode}_run_000.png", $"Art/{mode}_run_001.png");
            AddAnimation($"{mode}_nitro", 60, $"Art/{mode}_nitro_000.png", $"Art/{mode}_nitro_001.png");
            AddAnimation($"{mode}_crash", 0, $"Art/{mode}_crash_000.png");
            PlayAnimation($"{mode}_car");
            Turbo = 2;
            SetAnimationDelay($"{mode}_car", 30);
        }

        public override void OnHealthChanges(int health)
        {
            if (health <= 0)
            {
                Game.SetCurrentScene(new LoseScene());
            }
        }

        public override void OnEachFrame()
        {
            if (state == State.Nitro)
            {
                nitroTimeCount = (nitroTimeCount + 1) % NitroTime;
            }

            if (state == State.Crash)
            {
                crashTimeCount = (crashTimeCount + 1) % CrashTime;
            }

            if (nitroTimeCount == 0 && state == State.Nitro || crashTimeCount == 0 && state == State.Crash)
            {
                PlayAnimation($"{mode}_car");
                Turbo = mode == "danger" ? 2 : 1;
                state = State.Ride;
            }

            if (X - Width / 2 <= 0 || X + Width / 2 >= Game.Width)
            {
                SetAnimationDelay($"{mode}_car", 15);
            }
            else
            {
                SetAnimationDelay($"{mode}_car", 30);
            }

            if (X + Width / 2 <= 0 || X - Width / 2 >= Game.Width)
            {
                Game.OnLose();
            }
        }

        public override void OnCollide(GameObject collideObject)
        {
            if (Y + 20 >= collideObject.Y
                && Y - 20 <= collideObject.Y
                && X - 50 <= collideObject.X + collideObject.Width / 2
                && X + 50 >= collideObject.X - collideObject.Width / 2)
            {
                switch (collideObject)
                {
                    case RepairBonus _:
                        if (Health < 3)
                        {
                            Health++;
                        }
                        collideObject.DeleteFromGame();

                        break;
                    case NitroBonus _:
                        if (Nitro < MaxNitro)
                        {
                            Nitro++;
                        }
                        collideObject.DeleteFromGame();

                        break;
                }
            }

            if (collideObject is Palm
                && Y + 20 >= collideObject.Y
                && Y - 20 <= collideObject.Y
                && X - 20 <= collideObject.X
                && X + 20 >= collideObject.X)
            {
                collideObject.DeleteFromGame();
                Crash();
            }

            if ((collideObject is BrokenPalm || collideObject is BrokenCar || collideObject is Barrel)
                && CheckCollide(collideObject))
            {
                collideObject.DeleteFromGame();
                Crash();
            }

            if (collideObject is Puddle puddle && !puddle.IsUnderCar && CheckCollide(collideObject))
            {
                if (mode == "danger")
                {
                    collideObject.DeleteFromGame();
                }
                puddle.IsUnderCar = true;
                Crash();
            }

            if (collideObject is DeathRay ray && ray.IsRay)
            {
                if (ray.IsLeftRay && X < 280 && X > 100)
                {
                    Game.OnLose();
                }
                else if (!ray.IsLeftRay && X > Game.Width - 280 && X < Game.Width - 100)
                {
                    Game.OnLose();
                }
            }
        }

        private void Crash()
        {
            Health--;
            PlayAnimation($"{mode}_crash");
            state = State.Crash;
            CheckHorizontalFlip();
        }

        private bool CheckCollide(GameObject collideObject)
        {
            return Y + 20 >= collideObject.Y
                   && Y - 20 <= collideObject.Y
                   && X - 25 <= collideObject.X + collideObject.Width / 2
                   && X + 25 >= collideObject.X - collideObject.Width / 2;
        }

        private void CheckHorizontalFlip()
        {
            if (X < Game.Width / 2f && !isLookingRight || X > Game.Width / 2f && isLookingRight)
            {
                isLookingRight = !isLookingRight;
                FlipX();
            }
        }

        public override void OnKeyPress(Keyboard.Key pressedKey, bool isAlreadyPressed)
        {
            switch (pressedKey)
            {
                case Keyboard.Key.Left:
                    MoveIt(-2, 0);
                    break;
                case Keyboard.Key.Right:
                    MoveIt(2, 0);
                    break;
                case Keyboard.Key.Space:
                    if (isAlreadyPressed || state != State.Ride || Nitro == 0)
                        break;
                    PlayAnimation($"{mode}_nitro");
                    Nitro--;
                    state = State.Nitro;
                    Turbo = 8;
                    break;
            }
        }
    }
}
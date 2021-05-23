using System;
using Ungine;
using SFML.System;
using SFML.Window;

namespace Asteroid
{
    public class Spaceship : BounceObject
    {
        private const int MAX_HEALTH = 5;
        private const float MIN_SPEED = 1;
        private const float MAX_SPEED = 5;
        private int fireCooldown;
        protected Game game;
        private int immunityCooldown;
        private readonly Random rand = new Random();
        protected SpaceScene scene;

        public Spaceship(float x, float y, Game game_, SpaceScene scene_) : base(x, y, 135)
        {
            DrawPriority = 4;
            SetSprite("Art/ship.png");
            Scale = new Vector2f(0.2f, 0.2f);
            Scale = new Vector2f(0.2f, 0.2f);

            speed = 3;
            angle = 72;
            fireCooldown = 0;
            immunityCooldown = 0;
            Health = MAX_HEALTH;

            game = game_;
            scene = scene_;
        }

        private void Fire()
        {
            if (fireCooldown > 0) return;

            fireCooldown = 100;
            var bullet = new Bullet(X, Y, angle, game);
            scene.AddToScene(bullet);
        }

        private void emitParticle(int energy)
        {
            if (energy == 0) return;

            var hpBasis = 40 * energy;

            var particle = new Particle(
                X + rand.Next(-10, 10),
                Y + rand.Next(-10, 10),
                angle + 150 + (float) rand.NextDouble() * 60,
                (float) rand.NextDouble() * 0.3f * energy,
                (int) Math.Round(hpBasis - Math.Sqrt(rand.Next(hpBasis * hpBasis))),
                (float) rand.NextDouble() * 0.6f * energy,
                game
            );
            scene.AddToScene(particle);
        }

        private void TakeDamage()
        {
            if (immunityCooldown > 0) return;

            scene.AddExplosion(X, Y, 5 * SpaceScene.PARTICLE_COUNT);
            angle += 180;
            speed = MIN_SPEED;
            immunityCooldown = 200;
            Health--;

            if (Health == 0) Explode();
            else ActivateBarrier();
        }

        public void Explode()
        {
            scene.AddExplosion(X, Y, 12 * SpaceScene.PARTICLE_COUNT);

            DeleteFromGame();

            scene.Finish();
        }

        public void ActivateBarrier()
        {
            var barrier = new Barrier(this, 200, game);

            scene.AddToScene(barrier);
        }

        public override void OnKeyPress(Keyboard.Key key, bool isAlreadyPressed)
        {
            switch (key)
            {
                case Keyboard.Key.Left:
                    angle -= 2;
                    break;
                case Keyboard.Key.Right:
                    angle += 2;
                    break;
                case Keyboard.Key.Up:
                    if (speed < MAX_SPEED) speed += 0.1f;
                    break;
                case Keyboard.Key.Down:
                    if (speed > MIN_SPEED) speed -= 0.1f;
                    break;
                case Keyboard.Key.Space:
                    Fire();
                    break;
            }
        }

        public override void OnEachFrame()
        {
            if (fireCooldown > 0) fireCooldown--;
            if (immunityCooldown > 0) immunityCooldown--;

            emitParticle(MAX_HEALTH - Health);
            base.OnEachFrame();
        }

        public override void OnCollide(GameObject obj)
        {
            base.OnCollide(obj);

            if (obj is Asteroid) TakeDamage();
        }
    }
}
using System;
using Ungine;
using SFML.System;

namespace Asteroid
{
    public class Asteroid : BounceObject
    {
        private readonly Random rand = new Random();
        private readonly int rank = 1;

        private readonly float rotationSpeed;

        private readonly SpaceScene scene;

        public Asteroid(float x, float y, float initAngle, int rank_, SpaceScene scene_) : base(x, y, 0)
        {
            rank = rank_;

            DrawPriority = 2;
            SetSprite("Art/asteroid.png");
            Scale = new Vector2f(rank * 0.25f, rank * 0.25f);

            rotationSpeed = (float) rand.NextDouble() * 6 - 3;

            scene = scene_;

            speed = 0.5f + 2 * (float) rand.NextDouble();
            angle = initAngle;
        }

        public override void OnEachFrame()
        {
            angleSprite += rotationSpeed;

            base.OnEachFrame();
        }

        public override void OnCollide(GameObject obj)
        {
            base.OnCollide(obj);

            if (obj is Bullet)
            {
                for (var i = 0; i < 2; i++) scene.AddAsteroid(X, Y, rank - 1);
                for (var i = 0; i < 3; i++) scene.AddAsteroid(X, Y, rank - 2);
                for (var i = 0; i < 2; i++) scene.AddAsteroid(X, Y, rank - 3);

                scene.AddExplosion(X, Y, 3 * SpaceScene.PARTICLE_COUNT + rank * SpaceScene.PARTICLE_COUNT);
                scene.RemoveAsteroid();

                DeleteFromGame();
            }
        }
    }
}
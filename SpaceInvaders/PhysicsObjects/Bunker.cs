using System.Collections.Generic;
using csharp_sfml_game_framework;

namespace SpaceInvaders
{
    public class Bunker : PhysicsObject
    {
        private List<string> pathsToSprites; 

        public Bunker(string pathToSprite, float x, float y) : base(x, y, pathToSprite)
        {
            Health = 3;

            pathsToSprites = new List<string>
            {
                "Art/shelter_000.png",
                "Art/shelter_001.png",
                "Art/shelter_002.png"
            };
        }

        public override void OnCollide(GameObject collideObject)
        {
            if (collideObject is Bullet)
            {
                Health--;

                if (Health <= 0)
                {
                    DeleteFromGame();
                }
                else
                {
                    SetSprite(pathsToSprites[pathsToSprites.Count - Health]);
                }
            }

            if (collideObject is Invader)
            {
                DeleteFromGame();
            }
        }
    }
}
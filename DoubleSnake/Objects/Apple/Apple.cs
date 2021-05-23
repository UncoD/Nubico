using Ungine;
using MyFirstGame.Scenes;

namespace MyFirstGame.Objects
{
    class Apple : PhysicsObject
    {
        Play parent;
        public Apple(float x, float y, Play parentScene) : base(x, y, "Art/Food/Apple.png")
        {
            parent = parentScene;
        }

        public override void OnCollide(GameObject collideObject)
        {
            parent.RespawnApple();
        }
    }
}

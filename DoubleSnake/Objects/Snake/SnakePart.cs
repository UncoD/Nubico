using Ungine;

namespace MyFirstGame.Objects
{
    class SnakePart : PhysicsObject
    {
        public bool IsHead;
        private Snake parent;
        public SnakePart(float x, float y, string pathToTexture, Snake parent, bool isHead = false) : base(x, y, pathToTexture)
        {
            IsHead = isHead;
            this.parent = parent;
        }

        public override void OnCollide(GameObject collideObject)
        {
            if (!IsHead) return;

            if (collideObject is Apple)
            {
                parent.AddPart();
            } else if (collideObject is SnakePart)
            {
                var otherPart = collideObject as SnakePart;
                if (otherPart.parent.Equals(parent) || !otherPart.IsHead)
                {
                    parent.GameOver();
                } else
                {
                    parent.Draw();
                }
            }
        }
    }
}

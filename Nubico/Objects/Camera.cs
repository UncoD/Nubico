using SFML.Graphics;
using SFML.System;

namespace Nubico.Objects
{
    public class Camera : GameObject
    {
        private readonly View view;
        private GameObject parent;

        public Camera(GameObject parent, float zoom = 1) : base(0, 0)
        {
            this.parent = parent;
            view = new View();
            view.Size = new Vector2f(Game.Width, Game.Height);
            view.Center = parent.Position;
            view.Zoom(zoom);
            Game.Window.SetView(view);
        }

        public override void OnEachFrame()
        {
            view.Center = parent.Position;
            Game.Window.SetView(view);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
        }
    }
}

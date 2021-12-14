using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using SFML.Graphics;
using SFML.System;

namespace Nubico.Objects
{
    public class PhysicsObject : GameObject
    {
        private Body body;
        private RectangleShape cs;

        public PhysicsObject(float x, float y) : base(x, y)
        {
            BodyDef bodyDef = new BodyDef();
            bodyDef.Position.Set(x, y);
            body = Game.PhysicsWorld.CreateBody(bodyDef);
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox(1.0f, 1.0f);
            shapeDef.Density = 1.0f;
            shapeDef.Friction = 0.3f;
            shapeDef.Restitution = 1;
            body.CreateFixture(shapeDef);
            body.SetMassFromShapes();
            body.SetLinearVelocity(new Vec2(-1, -6));

            cs = new RectangleShape(new Vector2f(200, 200));
            cs.FillColor = SFML.Graphics.Color.Blue;
            cs.Origin = new Vector2f(100, 100);
            Vec2 position = body.GetPosition();
            cs.Position = new Vector2f(position.X * 100, position.Y * 100);
        }

        internal override void UpdateObject()
        {
            Vec2 position = body.GetPosition();
            cs.Position = new Vector2f(position.X * 100, position.Y * 100);
            cs.Rotation = body.GetAngle() * 180 / MathF.PI;

            if (cs.Position.X < -cs.Size.X || cs.Position.X > Game.Width + cs.Size.X
                || cs.Position.Y < -cs.Size.Y || cs.Position.Y > Game.Height + cs.Size.Y)
            {
                Game.PhysicsWorld.DestroyBody(body);
                DeleteFromGame();
            }
            else
            {
                OnEachFrame();
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(cs, states);

            if (Game.DrawObjectBorders)
            {
                var shape = (PolygonShape)body.GetFixtureList().Shape;
                var polygon = new ConvexShape(4)
                {
                    OutlineColor = SFML.Graphics.Color.Green,
                    OutlineThickness = 2,
                    FillColor = SFML.Graphics.Color.Transparent
                };
                for (uint i = 0; i < 4; i++)
                {
                    var vertex = body.GetWorldPoint(shape.Vertices[i]);
                    polygon.SetPoint(i, new Vector2f(vertex.X * 100, vertex.Y * 100));
                }
                target.Draw(polygon, states);
            }
        }
    }
}

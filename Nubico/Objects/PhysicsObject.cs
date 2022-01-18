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
        private SFML.Graphics.Shape shape;
        private const float coef = 200;
        private bool isPolygon = false;
        private bool isState = false;

        public PhysicsObject(float x, float y, bool pol = true, bool state = true) : base(x, y)
        {
            isState = state;
            isPolygon = pol;
            BodyDef bodyDef = new BodyDef();
            var bodyPos = new Vector2f(x, y) * 2 / coef;
            bodyDef.Position.Set(bodyPos.X, bodyPos.Y);
            Console.WriteLine(bodyDef.Position.X + " " + bodyDef.Position.Y);
            body = Game.PhysicsWorld.CreateBody(bodyDef);

            var boxSize = new Vec2(3f, 0.1f);
            var size = new Vector2f(boxSize.X, boxSize.Y) * coef;
            if (isPolygon)
            {
                body.CreateFixture(GetRectangle(boxSize));
                shape = new RectangleShape(size);
            } else
            {
                body.CreateFixture(GetCircle(0.5f));
                size = new Vector2f(0.5f, 0.5f) * coef;
                shape = new SFML.Graphics.CircleShape(size.X / 2);
            }
            if (!isState)
            {
                body.SetLinearVelocity(new Vec2(0, -6));
                body.SetMassFromShapes();
            }

            shape.Origin = size / 2;
            shape.FillColor = SFML.Graphics.Color.Blue;
            var position = body.GetPosition();
            shape.Position = new Vector2f(position.X, position.Y) * coef / 2;

            // Position (coef / 2, если origin в центре (* Origin ?))
            // Size (coef)
            // Body pos (* (2 / coef), / (0.5 * coef)?)
        }

        private PolygonDef GetRectangle(Vec2 size)
        {
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox(size.X, size.Y);
            shapeDef.Density = 1.0f;
            shapeDef.Friction = 0.3f;
            shapeDef.Restitution = 1;

            return shapeDef;
        }

        private CircleDef GetCircle(float radius)
        {
            CircleDef circleDef = new CircleDef();
            circleDef.Radius = radius;
            circleDef.Density = 1.0f;
            circleDef.Friction = 0.3f;
            circleDef.Restitution = 1;

            return circleDef;
        }

        internal override void UpdateObject()
        {
            Vec2 position = body.GetPosition();
            shape.Position = new Vector2f(position.X, position.Y) * coef / 2;
            shape.Rotation = body.GetAngle() * 180 / MathF.PI;

            var pos = new Vector2f();
            var size = new Vector2f();

            if (shape is RectangleShape rect)
            {
                pos = rect.Position;
                size = rect.Size;
            } else if (shape is SFML.Graphics.CircleShape circle)
            {
                pos = circle.Position;
                size = new Vector2f(circle.Radius * 2f, circle.Radius * 2f);
            }

            if (pos.X < -size.X || pos.X > Game.Width + size.X || pos.Y < -size.Y || pos.Y > Game.Height + size.Y)
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
            target.Draw(shape, states);

            if (Game.DrawObjectBorders)
            {
                if (isPolygon)
                {
                    DrawPolygonBorder(target, states);
                } else
                {
                    DrawCircleBorder(target, states);
                }
            }
        }

        private void DrawPolygonBorder(RenderTarget target, RenderStates states)
        {
            var shape = (PolygonShape)body.GetFixtureList().Shape;
            var vertexCount = (uint)shape.VertexCount;
            var polygon = new ConvexShape(vertexCount)
            {
                OutlineColor = SFML.Graphics.Color.Green,
                OutlineThickness = 2,
                FillColor = SFML.Graphics.Color.Transparent
            };
            for (uint i = 0; i < vertexCount; i++)
            {
                var vertex = body.GetWorldPoint(shape.Vertices[i]);
                polygon.SetPoint(i, new Vector2f(vertex.X, vertex.Y) * coef / 2);
            }
            target.Draw(polygon, states);
        }

        private void DrawCircleBorder(RenderTarget target, RenderStates states)
        {
            var shape = (Box2DX.Collision.CircleShape)body.GetFixtureList().Shape;
            var pos = body.GetWorldPoint(shape.GetVertex(0));
            var polygon = new SFML.Graphics.CircleShape()
            {
                Radius = 0.5f * coef / 2,
                Position = new Vector2f(pos.X, pos.Y) * coef / 2,
                OutlineColor = SFML.Graphics.Color.Green,
                OutlineThickness = 2,
                FillColor = SFML.Graphics.Color.Transparent,
                Origin = new Vector2f(0.25f, 0.25f) * coef
            };
            target.Draw(polygon, states);
        }
    }
}

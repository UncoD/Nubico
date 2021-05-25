using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using Ungine.Properties;

namespace Ungine
{
    public class TextObject : GameObject
    {
        public int Size
        {
            get => (int) Text.CharacterSize;
            set {
                Text.CharacterSize = (uint)value;
                Text.Origin = new Vector2f(Width / 2, Height / 2);
                Origin = Text.Origin;
            }
        }

        protected readonly Text Text;
        public new float Width => Text.GetGlobalBounds().Width;
        public new float Height => Text.GetGlobalBounds().Height;
        
        public TextObject(string text, float x, float y, int height = 15, string pathToFont = "") : base(x, y)
        {
            Text = new Text
            {
                Font = string.IsNullOrEmpty(pathToFont) ? new Font(Resources.DefaultFont) : new Font(pathToFont),
                DisplayedString = text,
                Position = new Vector2f(x, y),
                CharacterSize = (uint) height
            };

            Text.Origin = new Vector2f(Width / 2, Height / 2);
            Origin = Text.Origin;
            Text.LineSpacing = 2;
        }
        
        public FloatRect GetBounds()
        {
            return Text.GetGlobalBounds();
        }

        public void SetText(object text)
        {
            Text.DisplayedString = Convert.ToString(text);
        }

        public void SetColor(Color color)
        {
            Text.FillColor = color;
        }

        public override string ToString()
        {
            return Text.ToString();
        }
        
        public override void OnEachFrame() { }

        internal override void UpdateObject()
        {
            Text.Position = Position;
            Text.Origin = Origin;

            OnEachFrame();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(Text);
        }

        public new bool HoverOnThis(Vector2i position)
        {
            return GetBounds().Contains(position.X, position.Y);
        }

        public new bool HoverOnThis()
        {
            return HoverOnThis(Mouse.GetPosition(Game.Window));
        }
    }
}
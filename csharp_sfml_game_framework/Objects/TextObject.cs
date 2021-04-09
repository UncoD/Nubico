using SFML.Graphics;
using SFML.System;

namespace csharp_sfml_game_framework
{
    public class TextObject : GameObject
    {
        public int Size
        {
            get => (int) Text.CharacterSize;
            set => Text.CharacterSize = (uint) value;
        }

        protected readonly Text Text;
        public new float Width => Text.GetGlobalBounds().Width;
        public new float Height => Text.GetGlobalBounds().Height;
        
        public TextObject(string text, float x, float y, int height = 15, string pathToFont = "Font/font.ttf") : base(x, y)
        {
            Text = new Text
            {
                Font = new Font(pathToFont),
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

        public void SetText(string text)
        {
            Text.DisplayedString = text;
        }

        public void SetText(int text)
        {
            Text.DisplayedString = text.ToString();
        }

        public void SetColor(Color color)
        {
            Text.FillColor = color;
        }

        public void SetSize(int height)
        {
            Text.CharacterSize = (uint) height;
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
    }
}
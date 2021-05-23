using SFML.System;

namespace Ungine
{
    public class BlinkingTextObject : TextObject
    {
        private int time;
        private int speed = 80;
        private float x;
        private float y;

        public BlinkingTextObject(string text, float x, float y, int height = 15, string pathToFont = "Font/font.ttf")
            : base(text, x, y, height, pathToFont)
        {
            this.x = x;
            this.y = y;
        }

        public override void OnEachFrame()
        {
            if (time == speed / 3)
            {
                Position = new Vector2f(x, y);
            }

            time = (time + 1) % speed;

            if (time == 0)
            {
                Position = new Vector2f(-1000, -1000);
            }

            base.OnEachFrame();
        }
    }
}
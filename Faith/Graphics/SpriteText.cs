using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Faith.Graphics
{
    public class SpriteText : Drawable  // probably not great since it's literally called a "sprite"text but oh well
    {
        public string Text { get; set; }

        public SpriteFont Font;
        private string fontpath;

        public override Vector2 Scale => Font.MeasureString(Text ?? "");

        public SpriteText(string text, string path = "defaultFont")
        {
            Text = text;
            fontpath = path;
        }

        public SpriteText(SpriteFont font)
            : this ("", font)
        { }

        public SpriteText(string text, SpriteFont font)
            : this (text)
        {
            this.Font = font;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            // load the font if it hasn't been loaded already
            if (Font == null)
            {
                Font = content.Load<SpriteFont>(fontpath);
            }
        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);

            s.DrawString(Font, Text, Position, Colour);
            //s.DrawString(font, Text, Position, Colour, Rotation, OriginPos, Scale, Effects, 0);
        }
    }
}

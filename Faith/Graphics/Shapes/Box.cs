using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.Graphics.Shapes
{
    public class Box : Drawable
    {
        private Texture2D pixel;

        public int OutlineWidth;
        public Color OutlineCol;

        public Box(int x, int y, int w, int h)
            : this(new Vector2(x, y), new Vector2(w, h))
        { }

        public Box(Vector2 pos, Vector2 scale, int outlineWidth=0, Color? outlineCol=null)
        {
            Position = pos;
            Scale = scale;
            Fill = true;

            this.OutlineCol = outlineCol ?? Color.Black;
            this.OutlineWidth = outlineWidth;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            pixel = content.Load<Texture2D>("pixel");
        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);

            s.Draw(pixel, BoundingBox, Colour);

            if (OutlineWidth > 0)
            {
                // top
                s.Draw(pixel, new Rectangle((int)X, (int)Y, (int)Width, OutlineWidth), OutlineCol);
                // left
                s.Draw(pixel, new Rectangle((int)X, (int)Y, OutlineWidth, (int)Height), OutlineCol);
                // right
                s.Draw(pixel, new Rectangle(BoundingBox.Right - OutlineWidth, (int)Y, OutlineWidth, (int)Height), OutlineCol);
                // bottom
                s.Draw(pixel, new Rectangle((int)X, BoundingBox.Bottom - OutlineWidth, (int)Width, OutlineWidth), OutlineCol);
            }
        }
    }
}

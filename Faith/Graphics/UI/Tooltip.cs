using Faith.Graphics.Groups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.Graphics.UI
{
    public class Tooltip : Group
    {
        public Rectangle Region;
        public Vector2 Offset;

        private string text;
        public string Text
        {
            get => text;
            set
            {
                text = value;
                if (Loaded)
                    label.Text = text;
            }
        }

        private SpriteText label;

        public Tooltip(Rectangle region, string text)
        {
            Text = text;
            Region = region;
            Fill = true;
            Colour = Color.Black;
            Offset = new Vector2(15);
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            Add(label = new SpriteText(Text));
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            Vector2 mousePos = Mouse.GetState().Position.ToVector2();
            if (Region.Contains(mousePos))
            {
                Visible = true;
                Position = mousePos + Offset;
                label.Position = Position;
                Scale = new Vector2(label.Width, label.Height);
            }
            else
            {
                Visible = false;
            }
        }
    }
}

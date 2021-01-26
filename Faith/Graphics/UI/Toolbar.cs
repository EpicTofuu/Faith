using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Faith.Graphics.Groups;
using Faith.Graphics.Shapes;

namespace Faith.Graphics.UI
{
    public delegate void EventHandler(string args);

    public class Toolbar : Group
    {
        //https://docs.google.com/document/d/e/2PACX-1vSmXmPtLld5m1FoTVD_H3slrZo6z9UZK8Wu5yqWMR1_EtfV5g5biGzOT8ISxGkeeMCSvCln3vhEYDzU/pub

        //public List<string> Contents;
        public Dictionary<string, EventHandler> Contents;

        public event EventHandler OnClick;

        public Toolbar(FGame game)
        {
            Fill = true;
            Add(new SpriteText("File")
            {
                Position = new Vector2(10, 8),
                Colour = Color.Black
            });
            Add(new SpriteText("Edit")
            {
                Position = new Vector2(60, 8),
                Colour = Color.Black
            });
            Add(new SpriteText("View")
            {
                Position = new Vector2(110, 8),
                Colour = Color.Black
            });
            Add(new SpriteText("Objects")
            {
                Position = new Vector2(160, 8),
                Colour = Color.Black
            });
            Add(new SpriteText("Help")
            {
                Position = new Vector2(225, 8),
                Colour = Color.Black
            });
        }

        /*
        public Toolbar()
            : this(new List<string>())
        { }

        public Toolbar(IEnumerable<string> contents)
            : this(new List<string>(contents))
        { }
        */

        public override void Load(ContentManager content)
        {
            base.Load(content);
        }

        public void UpdateContent()
        {
            foreach (KeyValuePair<string, EventHandler> content in Contents)
            {

            }

            //foreach (string content in Contents)
            //{
            //    Button b = new Button(content);


            //Add(b);
            //}
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }
    }
}

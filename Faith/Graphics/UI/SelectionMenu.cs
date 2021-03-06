﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Faith.Graphics.Groups;
using Faith.Graphics.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Faith.Graphics.UI
{
    public delegate void SelectionHandler(string selection);

    public class SelectionMenu : ClickableGroup
    {
        public event SelectionHandler OnSelect; 

        private List<string> items;
        private List<Button> texts;

        public float HeightSpacing = 25;

        Box outlinebox;

        public override Rectangle BoundingBox => outlinebox?.BoundingBox ?? new Rectangle (0,0,0,0);

        public SelectionMenu(string[] items)
            : this (items.ToList())
        { }

        public SelectionMenu(List<string> items)
        {
            this.items = items;
            Visible = false;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            texts = new List<Button>();
            int i = 0;
            foreach (string item in items)
            {
                texts.Add(new Button(item)
                {
                    Align = Align.Left
                });
                i++;
            }

            foreach (Button b in texts)
                Add(b);

            Add(outlinebox = new Box(Position, new Vector2(Width, HeightSpacing * items.Count), 1)
            {
                Colour = new Color(0, 0, 0, 0)
            });


            UpdateElements();
        }

        public void UpdateElements()
        {
            outlinebox.Position = Position;
            outlinebox.Height = HeightSpacing * items.Count;

            int i = 0;
            foreach (string selection in items)
            {
                var text = texts[i];
                text.Position = new Vector2(X, Y + HeightSpacing * i);
                text.Scale = new Vector2(Width, HeightSpacing);
                text.UpdateButton();
                i++;
            }
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (Visible)
            {
                foreach (Button b in texts)
                {
                    if (b.Clicked)
                    {
                        OnSelect?.Invoke(b.Label);
                        Visible = false;
                        b.Clicked = false;
                    }
                }
            }
        }
    }
}

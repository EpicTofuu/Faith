using System;
using System.Collections.Generic;
using System.Text;
using Faith.Graphics.Groups;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Faith.Graphics.UI
{
    public enum Align
    { 
        Left,
        Centre,
        Right
    }

    public class Button : ClickableGroup
    {
        public Sprite ButtonSprite;
        public SpriteText LabelSprite;

        public string Label => LabelSprite.Text;

        string iconPath;
        Sprite icon;

        string tooltip;

        public Color DefaultColour = Color.White;
        public Color HoverColour = Color.LightGray;
        public Color DisabledColour = Color.DarkGray;

        public Tooltip Tooltip;

        public Align Align = Align.Centre;

        // what the fuck??

        public Button (string label, string tooltip = null)
        {
            ButtonSprite = new Sprite("pixel");
            LabelSprite = new SpriteText(label);

            Add(ButtonSprite);
            Add(LabelSprite);

            OnEnter += Button_OnEnter;
            OnExit += Button_OnExit;

            this.tooltip = tooltip;
        }

        public Button(string icon, string label, string tooltip = null)
        {
            ButtonSprite = new Sprite("pixel");
            LabelSprite = new SpriteText(label);

            Add(ButtonSprite);
            Add(LabelSprite);

            OnEnter += Button_OnEnter;
            OnExit += Button_OnExit;

            this.iconPath = icon;
            this.tooltip = tooltip;
        }

        public Button(Texture2D icon, string tooltip = null)
        {
            ButtonSprite = new Sprite("pixel");
            LabelSprite = new SpriteText("");

            Add(ButtonSprite);
            Add(LabelSprite);

            OnEnter += Button_OnEnter;
            OnExit += Button_OnExit;

            Add (this.icon = new Sprite(icon));
            this.tooltip = tooltip;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            if (icon != null)
            {
                icon.Position = Position;
                icon.Scale = Scale;
            }

            if (iconPath != null)
            {
                Add(icon = new Sprite(iconPath)
                {
                    Position = Position,
                    Scale = Scale
                });
            }

            if (tooltip != null)
            {
                Add(Tooltip = new Tooltip(BoundingBox, tooltip));
            }

            Colour = DefaultColour;
            UpdateButton();
        }

        protected virtual void Button_OnExit(Vector2 position)
        {
            Colour = DefaultColour;
            UpdateButton();
        }

        protected virtual void Button_OnEnter(Vector2 position)
        {
            Colour = HoverColour;
            UpdateButton();
        }

        public virtual void UpdateButton()
        {
            ButtonSprite.Position = Position;
            ButtonSprite.Scale = Scale;
            ButtonSprite.Colour = Colour;

            if (icon != null)
            {
                icon.Position = Position;
                icon.Scale = Scale;
                icon.Colour = Colour;
            }

            switch (Align)
            {
                case Align.Centre:
                    LabelSprite.Position = BoundingBox.Center.ToVector2() - LabelSprite.Scale / 2;
                    break;

                case Align.Left:
                    LabelSprite.Position = new Vector2(BoundingBox.Left + 5, BoundingBox.Center.Y - LabelSprite.Scale.Y / 2);
                    break;

                case Align.Right:
                    LabelSprite.Position = new Vector2(BoundingBox.Right - 5 - LabelSprite.Scale.X, BoundingBox.Center.Y - LabelSprite.Scale.Y / 2);
                    break;
            }

            // TODO:
            LabelSprite.Colour = Color.Black;

            if (Tooltip != null)
                Tooltip.Region = BoundingBox;
        }

        public virtual void Toggle(bool status)
        {
            Usable = status;

            if (Usable)
            {
                ButtonSprite.Colour = DefaultColour;
            }
            else
            {
                ButtonSprite.Colour = DisabledColour;
            }
        }
    }
}

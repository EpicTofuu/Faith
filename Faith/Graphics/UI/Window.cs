using System;
using System.Collections.Generic;
using System.Text;
using Faith.Graphics.Groups;
using Faith.Graphics.Screens;
using Faith.Graphics.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Faith.Graphics.UI
{
    public class Window : Screen
    {
        string title;

        // TODO: implement dragging
        ClickableGroup headbar;

        private Button closeButton;

        private ScreenStack stack;

        public Window(FGame game, string title, ScreenStack stack) 
            : base (game, new Color (255,0,0,0))
        {
            Fill = false;
            this.title = title;
            this.stack = stack;
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);

            Add(new Box(Position, Scale, 1, Color.BlueViolet));

            Add(headbar = new ClickableGroup
            { 
                Position = Position,
                Scale = new Vector2(Scale.X - 30, 30),
                Fill = true,
                Colour = Color.BlueViolet
            });

            Add(closeButton = new Button("ui/close", "")
            { 
                Position = new Vector2 (BoundingBox.Right - 30, Y),
                Scale = new Vector2 (30)
            });
            closeButton.OnPress += CloseButton_OnPress;

            Add(new SpriteText(title)
            { 
                Position = Position + new Vector2 (5),
                Colour = Color.Black
            });
        }

        private void CloseButton_OnPress(Vector2 position)
        {
            stack.Exit();
        }

        public virtual void Suspend()
        {
            stack.Exit();
        }
    }
}

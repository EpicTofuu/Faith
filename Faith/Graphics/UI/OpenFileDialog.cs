using Faith.Graphics.Groups;
using Faith.Graphics.Screens;
using Faith.Graphics.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.Graphics.UI
{
    public delegate void OpenHandler (string path);

    public class OpenFileDialog : Window
    {
        public event OpenHandler OnOpen;

        private Button open;
        private TextBox input;

        private FGame game;

        public string Path;

        
        public OpenFileDialog(FGame game, ScreenStack stack, string title="open file")
            : base (game, title, stack)
        {
            this.game = game;

            Width = 800;
            Height = 400;
        }
        

        public override void Load(ContentManager content)
        {
            Position = new Vector2(game.Width / 2 - Width / 2, game.Height / 2 - Height / 2);
            Scale = new Vector2(Width, Height);
            Colour = Color.White;

            base.Load(content);

            Add(input = new TextBox
            {
                Position = new Vector2(X + 10, BoundingBox.Bottom - 40),
                Scale = new Vector2(Width - 100, 30)
            });

            input.OnTextChange += Input_OnTextChange;

            Add(open = new Button("Open")
            {
                Position = new Vector2(X + Width - 80, BoundingBox.Bottom - 40),
                Scale = new Vector2(70, 30),
                DefaultColour = Color.CadetBlue
            });

            open.Toggle(false);
            open.OnPress += Open_OnPress;
        }

        private void Open_OnPress(Vector2 position)
        {
            OnOpen?.Invoke(input.Text);
            Suspend();
        }

        private void Input_OnTextChange()
        {
            if (input.Text.Length > 0)
            {
                open.Toggle(true);
            }
            else
            {
                open.Toggle(false);
            }
        }
    }
}

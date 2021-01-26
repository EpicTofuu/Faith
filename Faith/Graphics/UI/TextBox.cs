using Faith.Graphics.Groups;
using System;
using System.Linq;
using System.Windows;
using Microsoft.Xna.Framework;
using Faith.Graphics.Shapes;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Faith.Graphics.UI
{
    public delegate void Handler();

    public class TextBox : ClickableGroup
    {
        public bool Focus;

        Box cursor;
        Box backBox;

        SpriteText stext;
        public string Text { get; private set; } = "";

        private Keys[] lastPressedKeys;

        KeyboardState oldState;

        public event Handler OnTextChange;

        public TextBox()
        {
            OnClick += TextBox_OnClick;
            OnOutsideClick += TextBox_OnOutsideClick;

            lastPressedKeys = new Keys[0];

            oldState = Keyboard.GetState();
        }
        public override void Load(ContentManager content)
        {
            base.Load(content);

            Add(backBox = new Box(Position, Scale, 1, Color.Gray));
            Add(cursor = new Box(Position + new Vector2(5, 3), new Vector2(1, Height - 8))
            { 
                Colour = Color.Black
            });

            Add (stext = new SpriteText("")
            { 
                Position = Position + new Vector2 (5),
                Colour = Color.Black
            });
        }

        int t = 0;

        private void TextBox_OnOutsideClick(Vector2 position)
        {
            Focus = false;
        }

        private void TextBox_OnClick(Vector2 position)
        {
            t = 0;
            Focus = true;
            cursor.Enabled = true;
        }
        public override void Update(GameTime time)
        {
            base.Update(time);

            if (Focus)
            {
                backBox.OutlineCol = Color.CornflowerBlue;

                if (t >= 30)
                {
                    t = 0;
                    cursor.Enabled = !cursor.Enabled;
                }
                else
                {
                    t++;
                }

                KeyboardState newState = Keyboard.GetState();

                Keys[] pressedKeys = newState.GetPressedKeys();

                //check if any of the previous update's keys are no longer pressed
                foreach (Keys key in lastPressedKeys)
                {
                    if (!pressedKeys.Contains(key))
                        OnKeyUp(key);
                }

                //check if the currently pressed keys were already pressed
                foreach (Keys key in pressedKeys)
                {
                    if (!lastPressedKeys.Contains(key))
                        OnKeyDown(key);
                }

                //save the currently pressed keys so we can compare on the next update
                lastPressedKeys = pressedKeys;

                oldState = newState;
            }
            else
            {
                backBox.OutlineCol = Color.Gray;
                cursor.Enabled = false;
            }
        }

        private void OnKeyDown(Keys key)
        {
            string addition = "";
            if (key >= Keys.A && key <= Keys.Z)
            {
                if (Console.CapsLock)
                {
                    addition = key.ToString();
                }
                else
                {
                    addition = key.ToString().ToLower();
                }
            }
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                addition = key.ToString().Remove(0, 1);
            }

            if (key == Keys.Back && Text.Length > 0)
            {
                Text = Text.Remove(Text.Length - 1);
            }

            if (key == Keys.Space)
            {
                addition = " ";
            }
            Text += addition;
            updateText();

            OnTextChange?.Invoke();
        }

        private void updateText()
        {
            stext.Text = Text;
            cursor.X = X + stext.Width + 4;
        }

        private void OnKeyUp(Keys key)
        {
            //do stuff
        }
        //https://stackoverflow.com/questions/10154046/making-text-input-in-xna-for-entering-names-chatting
    }
}

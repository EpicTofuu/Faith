using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.Graphics.Screens
{
    public class ScreenStack : Drawable
    {
        public Stack<Screen> stack = new Stack<Screen>();

        public Screen CurrentScreen => stack.Count == 0 ? null : stack.Peek();

        ContentManager content;

        public ScreenStack(Screen initScreen) 
            : this ()
        {
            Push(initScreen);
        }

        public ScreenStack()
        {
        }

        public void Push(Screen s)
        {
            if (Loaded && !s.Loaded)
            {
                if (content == null)
                    throw new Exception("Could not access ContentManager, call Load() before pushing, otherwise parse a screen through the constructor");
                else
                    s.Load(content);
            }
            stack.Push(s);
        }

        public void Exit()
        {
            stack.Pop();
        }

        public void ChangeScreen(Screen s)
        {
            if (stack.Count > 0)
                Exit();

            Push(s);
        }

        public override void Load(ContentManager content)
        {
            this.content = content;

            CurrentScreen?.Load(content);

            base.Load(content);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            // only update the upmost screen
            CurrentScreen.Update(time);

            /*
            Screen[] st = stack.ToArray();
            
            foreach (Screen s in st)
            {
                s.Update(time);
            }
            */
        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);

            Screen[] st = stack.ToArray();
            Array.Reverse(st);
            foreach (Screen scr in st)
            {
                scr.Draw(s);
            }
        }
    }
}

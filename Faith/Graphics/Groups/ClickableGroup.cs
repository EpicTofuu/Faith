using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faith.Graphics.Groups
{
    // idk about delegate naming
    public delegate void MouseHandler(Vector2 position);

    public class ClickableGroup : Group
    {
        public virtual bool Usable { get; set; } = true;

        public event MouseHandler OnHover;
        public event MouseHandler OnEnter;
        public event MouseHandler OnExit;
        public event MouseHandler OnClick;
        public event MouseHandler OnHold;
        public event MouseHandler OnPress;
        public event MouseHandler OnOutsideClick;

        private MouseState oldState;
        private MouseState newState;

        public bool Holding;
        public bool Clicked;

        public ClickableGroup()
        {
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (Usable)
            {
                if (Holding)
                    OnHold?.Invoke(newState.Position.ToVector2());

                newState = Mouse.GetState();
                if (BoundingBox.Contains(newState.Position))
                {
                    if (BoundingBox.Contains(oldState.Position))
                    {
                        // idk
                        if (newState.LeftButton == ButtonState.Pressed)
                        {
                            OnPress?.Invoke(newState.Position.ToVector2());
                            if (oldState.LeftButton != ButtonState.Pressed)
                            {
                                OnClick?.Invoke(newState.Position.ToVector2());
                                Holding = true;
                                Clicked = true;
                            }
                            else
                            {
                                Clicked = false;
                            }
                        }
                    }
                    else
                    {
                        OnEnter?.Invoke(newState.Position.ToVector2());
                    }
                }
                else
                {
                    if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed)
                    {
                        OnOutsideClick?.Invoke(newState.Position.ToVector2());
                    }

                    if (BoundingBox.Contains(oldState.Position))
                    {
                        OnExit?.Invoke(newState.Position.ToVector2());
                    }
                }

                if (newState.LeftButton == ButtonState.Pressed)
                {
                    OnHover?.Invoke(newState.Position.ToVector2());
                }
                else
                {
                    Holding = false;
                }
                oldState = newState;
            }
        }
    }
}

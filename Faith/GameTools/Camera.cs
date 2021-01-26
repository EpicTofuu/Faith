using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using Faith.Graphics.Groups;

namespace Faith.GameTools
{
    public class Camera : IUpdate, IResource, IGeometric
    {
        public Vector2 Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #region shortcuts
        public virtual Rectangle BoundingBox => new Rectangle(Position.ToPoint(), Scale.ToPoint());

        // TODO make its own object maybe?
        public virtual float X
        {
            get => Position.X;
            set
            {
                Position = new Vector2(value, Position.Y);
            }
        }

        public virtual float Y
        {
            get => Position.Y;
            set
            {
                Position = new Vector2(Position.X, value);
            }
        }

        public virtual float Width
        {
            get => Scale.X;
            set
            {
                Scale = new Vector2(value, Scale.Y);
            }
        }

        public virtual float Height
        {
            get => Scale.Y;
            set
            {
                Scale = new Vector2(Scale.X, value);
            }
        }

        #endregion

        public bool Locked => false;
        public IGeometric Focus => throw new NotImplementedException();

        private Vector2 position;
        public Vector2 Position 
        {
            get => position;
            set
            {
                if (Locked)
                {
                    throw new Exception("the camera was locked before moving" +
                        "please make sure you unlocked the camera before modifying its position");
                }

                position = value;

                updateChildrenTransforms();   
            }
        }

        private float zoom = 1;
        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                updateChildrenTransforms();
            }
        }

        public bool Loaded { get; set; }

        protected Matrix TransformationMatrix =>
            Matrix.Identity *
            Matrix.CreateTranslation(Position.X, Position.Y, 0) *
            Matrix.CreateScale(Zoom, Zoom, Zoom);

        private Dictionary<IGeometric, Vector2> originalPositions = new Dictionary<IGeometric, Vector2>();
        private Group group;

        public Camera(Group group)
        {
            this.group = group;
        }

        // TODO: idk about exposing this
        public void updateChildrenTransforms()
        {
            foreach (IGeometric g in group)
            {
                g.Position = Vector2.Transform(originalPositions[g], Matrix.Invert(TransformationMatrix));
            }
        }

        public void Load(ContentManager content)
        {
            UpdateChildren();
            Loaded = true;
        }

        /// <summary>
        /// Call to add the original transforms of any new children to the originalPositions dictionary
        /// </summary>
        public void UpdateChildren()
        {
            foreach (IGeometric g in group)
            {
                if (!originalPositions.ContainsKey (g))
                    originalPositions[g] = g.Position;
            }
        }

        int oldsw;

        public void Update(GameTime time)
        {
            KeyboardState ks = Keyboard.GetState();
            int sw = Mouse.GetState().ScrollWheelValue;

            if (ks.IsKeyDown(Keys.LeftControl))
            {
                if (sw > oldsw)
                {
                    Zoom /= 1.1f;
                }
                else if (sw < oldsw)
                {
                    Zoom *= 1.1f;
                }

                if (ks.IsKeyDown(Keys.D0))
                    Zoom = 1f;
            }

            oldsw = sw;
        }

        public Vector2 ScreenToWorld(Vector2 scr) => Vector2.Transform(scr, TransformationMatrix);
        public Vector2 WorldToScreen(Vector2 scr) => Vector2.Transform(scr, Matrix.Invert(TransformationMatrix));
    }
}
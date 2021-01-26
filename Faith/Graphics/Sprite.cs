using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.Graphics
{
    public enum Anchor
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public class Sprite : Drawable
    {
        public virtual float Rotation { get; set; }
        public SpriteEffects Effects = SpriteEffects.None;
        public Anchor Origin;
        protected Vector2 OriginPos
        {
            get
            {
                Vector2 retPos = new Vector2();

                if (Loaded)
                {
                    switch (Origin)
                    {
                        case Anchor.TopLeft:
                            retPos = new Vector2(CurrentTexture.Bounds.Left, CurrentTexture.Bounds.Top);
                            break;
                        case Anchor.TopCenter:
                            retPos = new Vector2(CurrentTexture.Bounds.Center.X, CurrentTexture.Bounds.Top);
                            break;
                        case Anchor.TopRight:
                            retPos = new Vector2(CurrentTexture.Bounds.Right, CurrentTexture.Bounds.Top);
                            break;

                        case Anchor.CenterLeft:
                            retPos = new Vector2(CurrentTexture.Bounds.Left, CurrentTexture.Bounds.Center.Y);
                            break;
                        case Anchor.Center:
                            retPos = new Vector2(CurrentTexture.Bounds.Center.X, CurrentTexture.Bounds.Center.Y);
                            break;
                        case Anchor.CenterRight:
                            retPos = new Vector2(CurrentTexture.Bounds.Right, CurrentTexture.Bounds.Center.Y);
                            break;

                        case Anchor.BottomLeft:
                            retPos = new Vector2(CurrentTexture.Bounds.Left, CurrentTexture.Bounds.Bottom);
                            break;
                        case Anchor.BottomCenter:
                            retPos = new Vector2(CurrentTexture.Bounds.Center.X, CurrentTexture.Bounds.Bottom);
                            break;
                        case Anchor.BottomRight:
                            retPos = new Vector2(CurrentTexture.Bounds.Right, CurrentTexture.Bounds.Bottom);
                            break;
                    }
                }
                return retPos;
            }
        }

        public Texture2D CurrentTexture { get; private set; }
        public string CurrentTextureKey;
        private string defaultpath;    // used exclusively to load a single image

        public Dictionary<string, Texture2D> Costumes;

        public Sprite()
        {
            Costumes = new Dictionary<string, Texture2D>();
        }

        public Sprite(string path)
            : this ()
        {
            defaultpath = path;
        }

        public Sprite(Texture2D tex)
            : this()
        {
            Costumes["default"] = tex;
            ChangeCostume("default");
        }

        public Sprite(Dictionary<string, Texture2D> dict)
            : this()
        {
            Costumes = dict;
        }

        public void ChangeCostume(string key)
        {
            CurrentTexture = Costumes[key];
            CurrentTextureKey = key;

            autoUpdateScale();
        }

        public override void Load(ContentManager content)
        {
            // Load using the path if a texture hasn't already been supplied
            if (!Loaded && defaultpath != null)
            {
                Costumes["default"] = content.Load<Texture2D>(defaultpath);
                ChangeCostume("default");
            }

            autoUpdateScale();

            base.Load(content);
        }

        void autoUpdateScale()
        {
            if (Scale == Vector2.Zero && CurrentTexture != null)
                Scale = new Vector2(CurrentTexture.Width, CurrentTexture.Height);
        }

        public override void Draw(SpriteBatch s)
        {
            base.Draw(s);

            if (CurrentTexture != null)
                s.Draw(CurrentTexture, BoundingBox, CurrentTexture.Bounds, Colour, Rotation, OriginPos, Effects, 0);
        }
    }
}

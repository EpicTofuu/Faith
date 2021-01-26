using Faith.Graphics.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Faith.Graphics
{
    /// <summary>
    /// Smallest drawable unit. Drawing takes place manually with 
    /// the <see cref="SpriteBatch"/>
    /// </summary>
    public class Drawable : IResource, IUpdate, IDrawable
    {
        public virtual bool Loaded { get; set; }

        public virtual bool Enabled { get; set; } = true;
        public virtual bool Visible { get; set; } = true;

        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Scale { get; set; }

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

        public virtual Color Colour { get; set; } = Color.White;

        private Texture2D fillBox;
        public bool Fill;

        protected float deltaTime;

        public Drawable()
        {
        }

        /// <summary>
        /// The base call declares this <see cref="Drawable"/> loaded
        /// </summary>
        /// <param name="content"></param>
        public virtual void Load(ContentManager content)
        {
            fillBox = content.Load<Texture2D>("pixel");
            Loaded = true;
        }

        /// <summary>
        /// Called once every frame
        /// </summary>
        /// <param name="time"></param>
        public virtual void Update(GameTime time)
        {
            deltaTime = (float)time.ElapsedGameTime.TotalSeconds * FGame.PhysicsSpeed * FGame.IMPORTANT_AND_INTERNAL_PHYSICS_SPEED_MODIFIER_YOU_DEFINETLY_SHOULDNT_BE_TOUCHING;
        }

        public virtual void Draw(SpriteBatch s)
        {
            if (Fill)
                s.Draw(fillBox, BoundingBox, Colour);
        }
    }
}

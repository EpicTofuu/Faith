using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Faith.Graphics;
using System.ComponentModel;

namespace Faith.Physics
{
    /// <summary>
    /// implements a more traditional axis aligned collision system for 2d games. 
    /// A slightly less accurate representation of physics from a kinematics standpoint
    /// </summary>
    public class AxisAlignedBody : Sprite, ICollidableBody
    {
        // We actually do not need this..
        public int Mass
        {
            get => throw new FieldAccessException("Axis aligned bodies do not have mass");
            set => throw new WarningException("Axis aligned bodies are not supposed to have a mass. Please do not give this object a mass.");
        }

        public List<ICollidableBody> Others { get; set; }

        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        protected bool HorizontalCol;
        protected bool VerticalCol;

        #region shortcuts

        // velocity
        public float V_x
        {
            get => Velocity.X;
            set
            {
                Velocity = new Vector2(value, Velocity.Y);
            }
        }
        public float V_y
        {
            get => Velocity.Y;
            set
            {
                Velocity = new Vector2(Velocity.X, value);
            }
        }

        // acceleration
        public float A_x
        {
            get => Acceleration.X;
            set
            {
                Acceleration = new Vector2(value, Acceleration.Y);
            }
        }
        public float A_y
        {
            get => Acceleration.Y;
            set
            {
                Acceleration = new Vector2(Acceleration.X, value);
            }
        }

        #endregion

        /// <summary>
        /// whether the object physically reacts to collision
        /// </summary>
        public bool Fixed;

        public AxisAlignedBody(string path) : base(path)
        { }

        /// <summary>
        /// Called whenever any collision takes place between two <see cref="IPhysicalBody"/>
        /// </summary>
        /// <param name="other"></param>
        int depth_y = 0; 

        public virtual void OnCollision(IPhysicalBody other)
        {
            // TODO: look to convert if statements to mathematical expressions 

            if (!Fixed)
            {
                // horizontal
                if (Y < other.BoundingBox.Bottom && Y > other.BoundingBox.Top)
                {
                    var dist_x = other.BoundingBox.Center.X - this.BoundingBox.Center.X;
                    var depth_x = dist_x > 0 ? other.BoundingBox.Left - this.BoundingBox.Right : other.BoundingBox.Right - this.BoundingBox.Left;

                    V_x = depth_x * Math.Sign(dist_x);
                }

                // vertical
                if (X < other.BoundingBox.Right && X > other.BoundingBox.Left)
                {
                    var dist_y = this.BoundingBox.Center.Y - other.BoundingBox.Center.Y;
                    depth_y = dist_y > 0 ? this.BoundingBox.Top - other.BoundingBox.Bottom : this.BoundingBox.Bottom - other.BoundingBox.Top;

                    VerticalCol = depth_y == 0;
                    V_y = depth_y * Math.Sign(dist_y);
                }
            }
        }

        public override void Update(GameTime time)
        {
            // change velocity and position in time
            Velocity = new Vector2(Velocity.X + Acceleration.X * deltaTime, Velocity.Y + Acceleration.Y * deltaTime);
            Position = new Vector2(X + Velocity.X * deltaTime, Y + Velocity.Y * deltaTime);
            
            if (Others != null)
            {
                foreach (ICollidableBody other in Others)
                {
                    if (other != this && other.BoundingBox.Intersects (BoundingBox))
                    {
                        OnCollision(other);
                    }
                }
            }
            
            base.Update(time);
        }
    }
}

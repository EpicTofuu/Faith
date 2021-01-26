using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Faith.Graphics;

namespace Faith.Physics.Kinematics
{
    /// <summary>
    /// A sprite which has kinematic properties, 
    /// relatively accurate representation of physics from a kinematics standpoint.
    /// </summary>
    
    // TODO if possible, make kinematic bodies collidable as well
    public class KinematicBody : Sprite, IPhysicalBody
    {
        public int Mass { get; set; } = 10;
        public List<Vector2> Forces { get; set; } = new List<Vector2>();
        public Vector2 NetForce
        {
            get
            {
                Vector2 netforce = Vector2.Zero;
                foreach (Vector2 force in Forces)
                    netforce = Vector2.Add(force, netforce);

                return netforce;
            }
        }

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

        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; protected set; }

        public List<IPhysicalBody> Others { get; set; } = new List<IPhysicalBody>();

        public KinematicBody(string path, Vector2 init_vel) : this(path)
        {
            Velocity = init_vel;
        }

        public KinematicBody(string path) : base(path)
        { }

        public override void Update(GameTime time)
        {
            Acceleration = NetForce / Mass; 

            // change velocity and position in time
            Velocity = new Vector2(Velocity.X + Acceleration.X * deltaTime, Velocity.Y + Acceleration.Y * deltaTime);
            Position = new Vector2(X + Velocity.X * deltaTime, Y + Velocity.Y * deltaTime);

            base.Update(time);
        }
    }
}

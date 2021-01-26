using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.Physics
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }
    public interface ICollidableBody : IPhysicalBody
    {
        /// <summary>
        /// All other physical bodies in the screen
        /// </summary>
        List<ICollidableBody> Others { get; set; }

        /// <summary>
        /// Occurs when the current body takes on any collisions from other bodies
        /// </summary>
        void OnCollision(IPhysicalBody other);
    }
}

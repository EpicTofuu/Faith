using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Faith.Physics
{
    public interface IPhysicalBody : IGeometric
    {
        int Mass { get; set; }
        Vector2 Velocity { get; }
        Vector2 Acceleration { get; }
    }
}

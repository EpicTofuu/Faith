using Microsoft.Xna.Framework;

namespace Faith
{
    // any object pertaining to the grid system
    public interface IGeometric
    {
        Rectangle BoundingBox { get; }
        Vector2 Position { get; set; }
        Vector2 Scale { get; set;  }
    }
}

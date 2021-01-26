using Faith.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.Graphics
{
    interface IDrawable : IGeometric
    {
        Color Colour { get; set; }

        void Draw(SpriteBatch s);
    }
}

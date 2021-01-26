using Faith.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Faith.GameTools.Tiles
{
    /// <summary>
    /// Information regarding the physical properties of an instance of a single tile
    /// </summary>
    public class Tile : IGeometric
    {
        public Rectangle BoundingBox => new Rectangle(Position.ToPoint(), Scale.ToPoint());

        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public string Id;
    }

    public class DrawableTile : Sprite
    {
        Tile tile;

        public DrawableTile(Tile t, Texture2D tex)
        {
            tile = t;

            Costumes["default"] = tex;
            ChangeCostume("default");

            Position = tile.Position;
            Scale = tile.Scale;
        }
    }
}

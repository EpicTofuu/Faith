using Faith.Graphics.Groups;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;

namespace Faith.GameTools.Tiles
{
    /// <summary>
    /// Draws shit
    /// </summary>
    public class DrawableTileMap : Group
    {
        public TileMap TileMap;
        public TileHead TileHead;

        private List<Tile> added;
        private FGame game;

        public DrawableTileMap(TileMap TileMap, TileHead head, FGame game)
        {
            this.TileHead = head;
            this.game = game;
            this.TileMap = TileMap;

            added = new List<Tile>();
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            //TileHead.LoadAllTextures(game.GraphicsDevice);
            TileHead.Load(content);
            Reload(true);
        }

        public virtual void Reload(bool reloadAll=false)
        {
            if (TileMap.Data.Tiles == null)
                return;

            if (reloadAll)
            {
                Clear();
                added.Clear();
            }

            foreach (Tile tile in TileMap.Data.Tiles)
            {
                if (!added.Contains(tile))
                {
                    var d = new DrawableTile(tile, TileHead.GetTextureFromId(tile.Id));
                    Add(d);

                    added.Add(tile);
                }
            }
        }
    }
}

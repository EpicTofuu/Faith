using System;
using System.Collections.Generic;
using System.Text;
using Faith.Graphics.Groups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Faith.Graphics.Screens
{
    public class Screen : Group
    {
        protected FGame Game;

        public Screen(FGame game, Color backgroundCol)
        {
            Game = game;
            Colour = backgroundCol;
            Fill = true;
            Scale = new Vector2(game.Width, game.Height);
        }

        public Screen(FGame game)
            : this (game, Color.Black)
        { }
    }
}

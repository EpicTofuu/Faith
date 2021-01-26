using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Faith;
using Faith.Graphics.Shapes;

namespace FaithTests
{
    public class Game1 : FGame
    {
        public Game1()
        {
            Children.Add(new Box(40, 40, 40, 40));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Faith.Physics.Game
{
    // broken as hell

    /// <summary>
    /// A generic controller for a platformer character
    /// </summary>
    public class BasicPlatformerPlayerController : AxisAlignedBody
    {
        /// <summary>
        /// Maximum velocity attained from moving
        /// </summary>
        public float MaxSpeed { get; protected set; } = 30;
        
        /// <summary>
        /// Rate of change in velocity at which the player travels
        /// </summary>
        public float SetAccel { get; protected set; } = 5;

        /// <summary>
        /// I genuinely have no clue how this works.
        /// </summary>
        public float SetRetard { get; protected set; } = 5;

        /// <summary>
        /// Downward acceleration
        /// </summary>
        public float Gravity { get; protected set; } = 10f;

        /// <summary>
        /// Upward acceleration (when jumping)
        /// </summary>
        public float JumpAccel { get; protected set; } = 20f;

        /// <summary>
        /// How long the player can jump for
        /// </summary>
        public int JumpLimit { get; protected set; } = 10;

        // List of available keybinds
        // TODO: look to replace the current keybind system with something more robust
        public Keys[] LeftKey = { Keys.Left, Keys.A };
        public Keys[] RightKey = { Keys.Right, Keys.D };
        public Keys[] JumpKey = { Keys.Space, Keys.Up, Keys.W };

        // boolean conditions
        private bool isLeftKey  =>   Keyboard.GetState().GetPressedKeys().Intersect(LeftKey) .Any();
        private bool isRightKey =>   Keyboard.GetState().GetPressedKeys().Intersect(RightKey).Any();
        private bool isJumpKey  =>   Keyboard.GetState().GetPressedKeys().Intersect(JumpKey) .Any();
        
        // get the horizontal direction
        //private int h_dir => i(isRightKey) - i(isLeftKey);

        public BasicPlatformerPlayerController(string path)
            : base(path)
        { }

        int jumptime = 0;
        public override void Update(GameTime time)
        {
            #region movement
            // TODO: fix retardation
            // probably something to do with the fact that the retardation isn't a factor of the max speed but i actually have no idea..
            if (isLeftKey)
            {
                if (V_x > -MaxSpeed)
                    A_x = -SetAccel;
                else
                    A_x = 0;
            }
            else
            {
                if (V_x < 0)
                    A_x = SetRetard;
                else
                {
                    // IMPORTANT
                    // set the acceleration to 0 ONLY IN THIS FIRST CLAUSE
                    A_x = 0;
                }
            }
            
            if (isRightKey)
            {
                if (V_x < MaxSpeed)
                    A_x = SetAccel;
                else
                    A_x = 0;
            }
            else
            {
                if (V_x > 0)
                    A_x = -SetRetard;
            }
            #endregion

            #region gravity

            //A_y = 2f;

            // NOTE: jumptime isn't frame independent
            // TODO: move towards using V_y
            // TODO: make the jumping actually work. also fix gravity.

            if (isJumpKey)
            {
                
                if (jumptime < JumpLimit)
                {
                    jumptime++;
                    A_y = -JumpAccel;
                }
                else
                {
                    A_y = Gravity;
                }
            }
            else
            {
                jumptime = 0;
                A_y = Gravity;
            }

            #endregion

            base.Update(time);
        }
    }
}

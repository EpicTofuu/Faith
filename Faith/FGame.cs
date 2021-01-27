using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Faith.Graphics.Groups;


namespace Faith
{
    /// <summary>
    /// Main game type to inherit from
    /// </summary>
    public class FGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Group Children;

        /// <summary>
        /// The general speed at which the physics engine will run.
        /// Use fractional values for slow motion.
        /// </summary>
        public static float PhysicsSpeed = 1f;

        // I actually have no idea how this works..
        internal static float IMPORTANT_AND_INTERNAL_PHYSICS_SPEED_MODIFIER_YOU_DEFINETLY_SHOULDNT_BE_TOUCHING => 20f;

        internal static Texture2D MissingTex;
        internal static Texture2D ErrorTex;

        #region window properties

        //TODO: make this information static
        private int width;
        public int Width
        {
            get => width;
            set
            {
                width = value;
                updateWindowState();
            }
        }
        private int height;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                updateWindowState();
            }
        }
        private bool fullscreen;
        public bool Fullscreen
        {
            get => fullscreen;
            set
            {
                fullscreen = value;
                updateWindowState();
            }
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                updateWindowState();
            }
        }
        #endregion

        public FGame()
            : this (1024, 576)
        {
        }

        public FGame (int w, int h)
            : this ($"faith framework running: {Assembly.GetEntryAssembly().GetName().FullName}", w, h)
        { }

        public FGame(string name, int w, int h, bool fs = false)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Title = name;
            Width = w;
            Height = h;
            Fullscreen = fs;

            Children = new Group()
            {
                Position = Vector2.Zero,
                Scale = new Vector2(Width, Height)
            };
        }

        protected override void Initialize()
        {
            // this is probably a monogame bug
            // set the window title to something random in Initialize() 
            // to allow changing of window title
            Window.Title = "wow";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            ErrorTex = Content.Load<Texture2D>("status/error");
            MissingTex = Content.Load<Texture2D>("status/missing");

            updateWindowState();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Children.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            Children.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            Children.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void updateWindowState()
        {
            Window.Title = Title;

            _graphics.PreferredBackBufferWidth = Width;
            _graphics.PreferredBackBufferHeight = Height;
            _graphics.IsFullScreen = Fullscreen;
            _graphics.ApplyChanges();

        }
    }
}

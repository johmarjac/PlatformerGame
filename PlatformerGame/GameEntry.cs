using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using PlatformerGame.Entities.Levels;
using PlatformerGame.Scenes;

namespace PlatformerGame
{
    public class GameEntry : Game
    {
        public GameEntry()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
        }
        
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(spriteBatch);

            Services.AddService(new LevelManager(this));
            Services.GetService<LevelManager>().LoadLevels();

            Services.AddService(new BoxingViewportAdapter(Window, GraphicsDevice, 1024, 768));
            Services.AddService(new Camera2D(Services.GetService<BoxingViewportAdapter>()));
            

            Components.Add(new ScreenGameComponent(this, new[] { new Ingame(this) }));
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }
        
        protected override void UnloadContent()
        {
        }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
    }
}
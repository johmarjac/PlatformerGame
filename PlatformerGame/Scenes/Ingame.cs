using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Additions.Entities;
using MonoGame.Additions.Entities.Components;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using PlatformerGame.Entities.Components;
using PlatformerGame.Entities.Levels;

namespace PlatformerGame.Scenes
{
    public sealed class Ingame : Screen
    {
        public Ingame(Game game)
        {
            Game = game;
            Ecs = new EntityComponentSystem(Game);
            EntityFactory = new EntityFactory(Game, Ecs);
            LevelManager = new LevelManager(Game);
        }

        public override void Initialize()
        {
            Game.Components.Add(Ecs);
            Game.Components.Add(EntityFactory);

            base.Initialize();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            LevelManager.LoadLevels();
            LevelManager.SetLevel("Levels\\testlevel");

            Level = EntityFactory.CreateLevel(LevelManager.CurrentLevel.Map);
            MyPlayer = EntityFactory.CreatePlayer(Vector2.Zero);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Game.Services.GetService<Camera2D>().LookAt(MyPlayer.GetComponent<TransformComponent>().Position);
        }

        private Game Game;

        private EntityComponentSystem Ecs;
        private EntityFactory EntityFactory;
        private LevelManager LevelManager;

        private Entity Level;
        private Entity MyPlayer;
    }
}
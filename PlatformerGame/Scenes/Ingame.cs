using Microsoft.Xna.Framework;
using MonoGame.Additions.Entities;
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
            MyPlayer = EntityFactory.CreatePlayer(new Vector2(5, -20));
        }
        
        private float SolveLinearEquation(Point2 p1, Point2 p2, float x)
        {
            var m = (p1.Y - p2.Y) / (p1.X - p2.X);
            var b = p1.Y - (m * p1.X);
            return m * x + b;
        }

        private Game Game;

        private EntityComponentSystem Ecs;
        private EntityFactory EntityFactory;
        private LevelManager LevelManager;

        private Entity Level;
        private Entity MyPlayer;
    }
}
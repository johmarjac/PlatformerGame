using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PlatformerGame.Entities.Levels
{
    public sealed class LevelManager
    {
        public LevelManager(Game game)
        {
            Game = game;
            _levels = new List<Level>();
        }

        public void LoadLevels()
        {
            foreach(var levelFile in Directory.GetFiles(Path.Combine(Game.Content.RootDirectory, "Levels")))
            {
                var relativeFileWithoutExt = Path.Combine(Path.GetDirectoryName(levelFile).Replace("Content" + Path.DirectorySeparatorChar, ""), Path.GetFileNameWithoutExtension(levelFile));
                var level = new Level(Game.Content.Load<TiledMap>(relativeFileWithoutExt));
                _levels.Add(level);
            }
        }

        public void SetLevel(string name)
        {
            CurrentLevel = _levels.FirstOrDefault(l => l.Map.Name == name);
        }

        public readonly Game Game;

        private readonly List<Level> _levels;
        public IEnumerable<Level> Levels => _levels;

        public Level CurrentLevel { get; private set; }
    }
}
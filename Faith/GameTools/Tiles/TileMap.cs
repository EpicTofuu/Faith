using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Faith.GameTools.Tiles
{
    /// <summary>
    /// Literal data
    /// </summary>
    [Serializable]
    public class TileMapData
    {
        public string Name;
        public List<string> Properties;
        public List<Tile> Tiles;
    }

    /// <summary>
    /// Handles data
    /// </summary>
    public class TileMap
    {
        private string workingDir;
        public TileMapData Data { get; protected set; }

        /// <summary>
        /// new level
        /// </summary>
        /// <param name="name"></param>
        /// <param name="wd"></param>
        public TileMap(string wd, string name = "new_level")
        {
            workingDir = wd;

            Data = new TileMapData();

            Data.Tiles = new List<Tile>();
            Data.Properties = new List<string>();

            Data.Name = name;

            //Data.Properties.Insert(0, $"tile map version {Game1.MapFileVersion}");
            //Data.Properties.Insert(1, "level by anon");
        }
        

        public void Write()
        {
            // TODO: make own file format

            string leveldir = Path.Combine(workingDir, "_levels");
            if (!Directory.Exists(leveldir))
                Directory.CreateDirectory(leveldir);


            using (TextWriter writer = new StreamWriter(Path.Combine(leveldir, Data.Name + ".xml")))
            {
                XmlSerializer ser = new XmlSerializer(typeof(TileMapData));
                ser.Serialize(writer, Data);
            }
        }
    }
}

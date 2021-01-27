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

            Data = new TileMapData()
            { 
                Tiles = new List<Tile>(),
                Properties = new List<string>(),
                Name = name
            };

            // TODO:
            //Data.Properties.Insert(0, $"tile map version {Game1.MapFileVersion}");
            //Data.Properties.Insert(1, "level by anon");
        }

        /// <summary>
        /// Load level
        /// </summary>
        /// <param name="path"></param>
        public TileMap(string path)
        {
            // infer the working directory
            workingDir = Path.Combine(Path.GetDirectoryName(path), @"..\");

            XmlSerializer ser = new XmlSerializer(typeof(TileMapData));
            using (Stream reader = new FileStream(path, FileMode.Open))
            {
                Data = (TileMapData)ser.Deserialize(reader);
            }
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

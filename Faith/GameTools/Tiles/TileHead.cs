using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Graphics;

namespace Faith.GameTools.Tiles
{
    public class TileHead
    {
        public const string FILENAME = "_head.json";
        string workingDir;

        public TileData[] TileData;

        private Dictionary<string, Texture2D> tileTextures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// New tile head
        /// </summary>
        public TileHead()
        { }

        /// <summary>
        /// Load existing tile head
        /// </summary>
        /// <param name="wd">working directory</param>
        public TileHead(string wd)
        {
            workingDir = wd;
            try
            {
                var jsonString = File.ReadAllText(Path.Combine (wd, FILENAME));
                TileData = JsonSerializer.Deserialize<TileData[]>(jsonString);
            }
            catch (JsonException e)
            {
                Console.WriteLine(e);
            }

            if (Array.Find(TileData, x => x.Id == "none") != null)
            {
                throw new Exception($"cannot have block id with name 'none'");  
            }
        }

        // TODO: doesnt write to the working path
        public void Write()
        {
            var jsonString = JsonSerializer.Serialize(TileData, new JsonSerializerOptions
            { 
                WriteIndented = true
            });

            File.WriteAllText(FILENAME, jsonString);
        }

        public void LoadAllTextures(GraphicsDevice gd)
        {
            foreach (TileData t in TileData)
            {
                if (!tileTextures.ContainsKey(t.Id))
                {
                    var tex = Texture2D.FromFile(gd, Path.Combine (workingDir, t.Tex));
                    tileTextures[t.Id] = tex;
                }
            }
        }

        public Texture2D GetTextureFromId(string id)
        {
            if (tileTextures.ContainsKey(id))
            {
                return tileTextures[id];
            }
            else
            {
                return FGame.MissingTex;
            }
        }

        public Dictionary<string, Texture2D> GetAllTextures () => tileTextures;
    }

    /// <summary>
    /// Information regarding overall information of tile type
    /// </summary>
    public class TileData
    { 
        public string Id { get; set; }
        public string Tex { get; set; }
    }
}

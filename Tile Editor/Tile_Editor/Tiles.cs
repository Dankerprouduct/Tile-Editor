using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tile_Editor
{
        
    class Tiles
    {
        public List<Texture2D> tiles;
        public Tiles()
        {
            
        }


        public void LoadContent(ContentManager content)
        {
            tiles = new List<Texture2D>();
            tiles.Add(content.Load<Texture2D>("BlankTile")); 
            tiles.Add(content.Load<Texture2D>("Tile"));
            tiles.Add(content.Load<Texture2D>("Tile2"));

            
        }
        
    }
}

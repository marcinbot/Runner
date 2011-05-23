using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using runner.Object;

namespace runner.Platform
{
    /// <summary>
    /// Represents a static platform, that won't fall, explode or do anything unexpected. It's just there.
    /// </summary>
    class NormalPlatform : PlatformTemplate
    {

        int[,] tiles;

        public NormalPlatform(int x, int screenHeight, int width, int height, Texture2D texture)
            : base(x, screenHeight, width, height, texture)
        {
            tiles = new int[width, height/50+1];
            tiles[0, 0] = 0;
            for (int i = 1; i < width-1; i++)
            {
                tiles[i,0] = GameState.random.Next(1, 3);
            }
            tiles[width - 1, 0] = 3;
            for (int j = 1; j < height / 50+1; j++)
            {
                tiles[0, j] = GameState.random.Next(4, 6);
                for (int i = 1; i < width-1; i++)
                {
                    tiles[i, j] = GameState.random.Next(8, 11);
                }
                tiles[width - 1, j] = GameState.random.Next(6, 8);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
                for (int y = 0; y < tiles.GetLength(1); y++)
                    spriteBatch.Draw(Textures.building, 
                                     new Rectangle(boundingBox.X + x * 50, boundingBox.Y + y * 50, 50, 50), 
                                     new Rectangle(50 * tiles[x, y], 0, 50, 50), 
                                     Color.White);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace runner.Platform
{
    /// <summary>
    /// Represents a platform that also has a ceiling
    /// </summary>
    class TunnelPlatform : PlatformTemplate
    {
        public Rectangle topBoundingBox;
        int[,] topTiles;
        int[,] tiles;

        public TunnelPlatform(int x, int screenHeight, int width, int height, Texture2D texture)
            : base(x, screenHeight, width, height, texture)
        {
            topBoundingBox = new Rectangle(x, -25, width*50, screenHeight - height - 100);

            //ugly hack to put the tiles together
            tiles = new int[width, height / 50 + 2];
            tiles[0, 0] = 0;
            tiles[tiles.GetLength(0) - 1, 0] = 3;
            //sides:
            for (int vertI = 1; vertI < tiles.GetLength(1); vertI++)
            {
                tiles[0, vertI] = GameState.random.Next(3, 5);
                tiles[tiles.GetLength(0)-1, vertI] = GameState.random.Next(3, 5);
            }
            //fill the middle
            int horizI = 1;
            while (horizI < tiles.GetLength(0)-2)
            {
                int r = GameState.random.Next(0, 2);
                if (r == 0)
                {
                    tiles[horizI, 0] = 1;
                    tiles[horizI+1, 0] = 2; //tiles still hold information about the x position of the image
                    for (int vertI = 1; vertI < tiles.GetLength(1); vertI++)
                    {
                        //but now they will hold the information about the y position
                        int vR = GameState.random.Next(3, 5);
                        tiles[horizI, vertI] = vR;
                        tiles[horizI+1, vertI] = vR;
                    }
                }
                if (r == 1)
                {
                    //as above, horizontal:
                    tiles[horizI, 0] = 4;
                    tiles[horizI + 1, 0] = 5;
                    //vertical:
                    tiles[horizI, 1] = 3;
                    tiles[horizI + 1, 1] = 3;
                    for (int vertI = 2; vertI < tiles.GetLength(1); vertI++)
                    {
                        tiles[horizI, vertI] = 4;
                        tiles[horizI + 1, vertI] = 4;
                    }
                }
                horizI += 2;
            }

            //top box tiles:
            //bottom row:
            topTiles = new int[width, (screenHeight - height) / 50 + 2];
            topTiles[0, 0] = 6;
            topTiles[topTiles.GetLength(0) - 1, 0] = 9;
            for (int hI = 1; hI < topTiles.GetLength(0) / 2 - 1; hI++)
                topTiles[hI, 0] = 10;
            //clock:
            topTiles[topTiles.GetLength(0) / 2-1, 0] = 7;
            topTiles[topTiles.GetLength(0) / 2, 0] = 8;
            topTiles[topTiles.GetLength(0) / 2 - 1, 1] = 3;//horizontal
            topTiles[topTiles.GetLength(0) / 2, 1] = 3;//horizontal
            //complete bottom row:
            for (int hI = topTiles.GetLength(0) / 2 + 1; hI < topTiles.GetLength(0) - 1; hI++)
                topTiles[hI, 0] = 10;
            //sides:
            for (int vI = 1; vI < topTiles.GetLength(1); vI++)
            {
                topTiles[0, vI] = 3;
                for (int hI = 1; hI < topTiles.GetLength(0) - 1; hI++)
                    if (topTiles[hI, vI] == 0)
                        topTiles[hI, vI] = 2;
                topTiles[topTiles.GetLength(0) - 1, vI] = 3;
            }
            GameState.debug = "a";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //bottom box
            for (int x = 0; x < tiles.GetLength(0); x++)
            {
                spriteBatch.Draw(Textures.building,
                                     new Rectangle(boundingBox.X + x * 50, boundingBox.Y, 50, 50),
                                     new Rectangle(50 * tiles[x, 0], 100, 50, 50),
                                     Color.White);
                for (int y = 1; y < tiles.GetLength(1); y++)
                        spriteBatch.Draw(Textures.building,
                                         new Rectangle(boundingBox.X + x * 50, boundingBox.Y + y * 50, 50, 50),
                                         new Rectangle(50*tiles[x, 0], 50 * tiles[x, y], 50, 50),
                                         Color.White);
            }
            //top box
            //spriteBatch.Draw(texture, topBoundingBox, Color.Yellow);
            for (int x = 0; x < topTiles.GetLength(0); x++)
            {
                spriteBatch.Draw(Textures.building,
                                     new Rectangle(topBoundingBox.X + x * 50, topBoundingBox.Bottom - 50, 50, 50),
                                     new Rectangle(50 * topTiles[x, 0], 200, 50, 50),
                                     Color.White);
                for (int y = 1; y < topTiles.GetLength(1); y++)
                    spriteBatch.Draw(Textures.building,
                                         new Rectangle(topBoundingBox.X + x * 50, topBoundingBox.Bottom - (y+1) * 50, 50, 50),
                                         new Rectangle(50 * topTiles[x, 0], 50 * topTiles[x, y], 50, 50),
                                         Color.White);
            }
        }

        public override void Update()
        {
            topBoundingBox.X = boundingBox.X;
        }


    }
}

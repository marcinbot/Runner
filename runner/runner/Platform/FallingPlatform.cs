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
    /// Represents a platform that will start crumbling down once the player lands on it
    /// </summary>
    class FallingPlatform : PlatformTemplate
    {
        bool falling;
        int[,] tiles;

        public FallingPlatform(int x, int screenHeight, int width, int height, Texture2D texture)
            : base(x, screenHeight, width, height, texture)
        {
            falling = false;

            tiles = new int[width, height / 50 + 1];
            tiles[0, 0] = 11;
            for (int i = 1; i < width - 1; i++)
            {
                tiles[i, 0] = GameState.random.Next(12, 14);
            }
            tiles[width - 1, 0] = 14;
            for (int j = 1; j < height / 50 + 1; j++)
            {
                tiles[0, j] = GameState.random.Next(15, 17);
                for (int i = 1; i < width - 1; i++)
                {
                    tiles[i, j] = GameState.random.Next(19, 22);
                }
                tiles[width - 1, j] = GameState.random.Next(17, 19);
            }
        }

        public override void HandleCollision()
        {
            falling = true;
            VibrationController.Start(3);
        }

        public override void Update()
        {
            if (falling)
            {
                if (GameState.player.standing)
                    GameState.player.boundingBox.Y += 2;
                boundingBox.Y += 2;
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

        public override void HandleRelease()
        {
            VibrationController.Stop();
        }
    }
}

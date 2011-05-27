using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace runner.Object
{
    class ObstacleTemplate : ObjectTemplate
    {
        int skinID;

        public ObstacleTemplate(int x, int y, int width, int height, Texture2D texture)
            : base(x, y, width, height, texture)
        {
            skinID = GameState.random.Next(0, 3);
        }

        public void checkForCollision(Player player)
        {
            if (isActive && boundingBox.Intersects(player.boundingBox))
            {
                GameState.scrollingSpeed -= 4;
                if (GameState.scrollingSpeed < 7)
                {
                    GameState.scrollingSpeed = 7;
                }
                isActive = false;
            }
        }

        public void Update(Player player)
        {
            if (isActive)
                checkForCollision(player);
            else
            {
                rotation += 0.3f;
                boundingBox.Y += 3;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, boundingBox, Color.Orange);
            spriteBatch.Draw(Textures.obstacles,
                //new Vector2(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2),
                new Rectangle(boundingBox.X, boundingBox.Y + boundingBox.Height / 2 + 2, boundingBox.Width, boundingBox.Height),
                new Rectangle(skinID * boundingBox.Width, 0, boundingBox.Width, boundingBox.Height),
                Color.White,
                rotation,
                origin,
                SpriteEffects.None,
                0);
        }
    }
}

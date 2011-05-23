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
        public ObstacleTemplate(int x, int y, int width, int height, Texture2D texture)
            : base(x, y, width, height, texture)
        {
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
    }
}

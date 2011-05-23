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
    /// Abstract for all platform types
    /// </summary>
    abstract class PlatformTemplate
    {
        public Rectangle boundingBox;
        public Texture2D texture;

        public PlatformTemplate(int x, int screenHeight, int width, int height, Texture2D texture)
        {
            boundingBox = new Rectangle(x, screenHeight-height, width*50, height);
            this.texture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, Color.Red);
        }

        public ObstacleTemplate PlaceObstacleOnPlatform(int width, int height)
        {
            int x = GameState.random.Next(boundingBox.Left + 50, boundingBox.Right - width - 60);
            return new ObstacleTemplate(x, boundingBox.Y-height, width, height, Textures.dummy);
        }

        public virtual void HandleCollision()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void HandleRelease()
        {
        }
    }
}

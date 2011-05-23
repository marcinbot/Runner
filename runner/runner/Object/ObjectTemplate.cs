using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace runner.Object
{
    abstract class ObjectTemplate
    {
        public Rectangle boundingBox;
        Texture2D texture;
        public bool isActive;
        public float rotation;
        Vector2 origin;

        public ObjectTemplate()
        {
        }

        public ObjectTemplate(int x, int y, int width, int height, Texture2D texture)
        {
            isActive = true;
            boundingBox = new Rectangle(x, y, width, height);
            this.texture = texture;
            rotation = .0f;
            origin = new Vector2(0.5f, 0.5f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, boundingBox, Color.Orange);
            spriteBatch.Draw(texture, 
                new Vector2(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2), 
                null, 
                Color.Green, 
                rotation, 
                origin, 
                new Vector2(boundingBox.Width,boundingBox.Height), 
                SpriteEffects.None, 
                0);
        }
    }
}

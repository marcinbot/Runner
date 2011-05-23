using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using runner.Object;

namespace runner.Platform
{
    /// <summary>
    /// Static class producing platforms
    /// </summary>
    static class PlatformFactory
    {
        static int generateX(int lastX, int min, int max)
        {
            if (min == 0)
                min = 12 * (int)GameState.scrollingSpeed;
            if (max == 0)
                max = 18 * (int)GameState.scrollingSpeed;
            return lastX + GameState.random.Next(min, max);
        }

        static int generateHeight(int lastH)
        {
            return lastH + GameState.random.Next(-30, 50);
        }

        /// <summary>
        /// Adds a normal platform to the list
        /// </summary>
        /// <param name="platforms"></param>
        /// <param name="obstacles"></param>
        /// <param name="GameState.random"></param>
        /// <param name="screenHeight"></param>
        /// <param name="h"></param>
        static void addNormalPlatform(List<PlatformTemplate> platforms, List<ObstacleTemplate> obstacles, int screenHeight, int h)
        {
            int x = generateX(platforms.Last().boundingBox.Right, 0, 0);
            int height;
            if (h == 0)
            {
                height = generateHeight(platforms.Last().boundingBox.Height);
                if (height > 300) height = 250;
                if (height <= 0) height = 60;
            }
            else
                height = h;
            int width = GameState.random.Next(4, 10);
            NormalPlatform p = new NormalPlatform(x, screenHeight, width, height, Textures.dummy);

            //place objects on the platform
            if (width > 6)
            {
                int objectsToPlace = GameState.random.Next(0, (int)width / 3);
                for (int i = 0; i < objectsToPlace; i++)
                    obstacles.Add(p.PlaceObstacleOnPlatform(25, 25));
            }
            platforms.Add(p);
        }

        /// <summary>
        /// Adds GameState.random new platforms to the platforms list
        /// </summary>
        /// <param name="platforms"></param>
        /// <param name="obstacles"></param>
        /// <param name="GameState.random"></param>
        /// <param name="screenHeight"></param>
        public static void UpdatePlatformsList(List<PlatformTemplate> platforms, List<ObstacleTemplate> obstacles, int screenHeight)
        {
            int platformType = GameState.random.Next(0, 5);

            if (platformType < 3)
            {
                //normal latform
                addNormalPlatform(platforms, obstacles, screenHeight, 0);
            }
            else if (platformType == 3)
            {
                //falling platform
                int x = generateX(platforms.Last().boundingBox.Right, 0, 0);
                int height = generateHeight(platforms.Last().boundingBox.Height);
                if (height > 300) height = 250;
                if (height <= 0) height = 60;
                int width = GameState.random.Next(5, 10);

                FallingPlatform fP = new FallingPlatform(x, screenHeight, width, height, Textures.dummy);
                platforms.Add(fP);

                addNormalPlatform(platforms, obstacles, screenHeight, height + GameState.random.Next(0, 20));
            }
            else if (platformType == 4)
            {
                //tunnel platform
                int x = generateX(platforms.Last().boundingBox.Right, 8 * (int)GameState.scrollingSpeed, 10 * (int)GameState.scrollingSpeed);
                int height = generateHeight(platforms.Last().boundingBox.Height);
                if (height > 300) height = 250;
                if (height <= 0) height = 60;
                int width = GameState.random.Next(4, 10);
                if (width % 2 == 1)
                    width += 1;

                TunnelPlatform tP = new TunnelPlatform(x, screenHeight, width, height, Textures.dummy);
                platforms.Add(tP);

                addNormalPlatform(platforms, obstacles, screenHeight, height + GameState.random.Next(0, 20));
            }
        }
    }
}

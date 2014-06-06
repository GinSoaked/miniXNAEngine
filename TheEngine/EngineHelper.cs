using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheEngine
{
    public static class EngineHelper
    {
        public static Random random = new Random();


        /// <summary>
        /// Rotate vector about point
        /// </summary>
        /// <param name="vecToRotate">Vector that is to be rotated</param>
        /// <param name="angle">Angle to rotate by in degrees</param>
        /// <param name="centre">Centre to rotate about</param>
        /// <returns></returns>
        /// 

        public static Vector2 RotateAboutPoint(Vector2 vecToRotate, float angle, Vector2 centre)
        {

            float X = centre.X + (vecToRotate.X - centre.X) * (float)Math.Cos(MathHelper.ToRadians(angle)) - (vecToRotate.Y - centre.Y) * (float)Math.Sin(MathHelper.ToRadians(angle));
            float Y = centre.Y + (vecToRotate.X - centre.X) * (float)Math.Sin(MathHelper.ToRadians(angle)) + (vecToRotate.Y - centre.Y) * (float)Math.Cos(MathHelper.ToRadians(angle));

            return new Vector2(X, Y);
        }


        /// <summary>
        /// returns random float between min and max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float RandomFloat(float min, float max)
        {
            return (float)((max - min) * random.NextDouble() + min);
        }


        /// <summary>
        /// Returns random float between 0 and 1
        /// </summary>
        /// <returns></returns>
        public static float RandomFloat()
        {
            return (float)((1.0f - 0) * random.NextDouble() + 0);
        }

        public static float RandomFloat(float max)
        {
            return (float)((max - 0) * random.NextDouble() + 0);
        }

        public static float RandomMax2Pi()
        {
            return RandomFloat(0, 2 * MathHelper.TwoPi);
        }

        public static float AngleOfArcDegrees(float arcLength, float radius)
        {
            return (float)MathHelper.ToDegrees(arcLength / radius);
        }

        public static float AngleOfArcRadians(float arcLength, float radius)
        {
            return arcLength / radius;
        }

        public static bool PointInCircle(Vector2 point, Vector2 centre, float circleRad)
        {
            if (Vector2.Distance(point, centre) < circleRad)
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Returns -1,-1 if there is no collision.
        /// </summary>
        /// <param name="tex1">Texture Array 1</param>
        /// <param name="mat1">Matrix of object 1</param>
        /// <param name="tex2">Texture Array 2</param>
        /// <param name="mat2">Matrix of object 2</param>
        /// <returns></returns>
        public static Vector2 TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 0)
                            {
                                if (tex2[x2, y2].A > 0)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    return screenPos;
                                }
                            }
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }

        public static Color[,] TextureTo2DArray(Texture2D texture)
        {
            Color[] colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }
    }
}

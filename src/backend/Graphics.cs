using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public static class Graphics
    {
        private static Texture2D primitivePixel { get; set; }
        private static Texture2D primitiveCircle { get; set; }
        private static Texture2D primitiveEqualateral { get; set; }

        public static void Initiate(GraphicsDevice graphics)
        {
            primitivePixel = new Texture2D(graphics, 1, 1,false,SurfaceFormat.Color);
            primitivePixel.SetData(new[] { Color.White });


            System.Diagnostics.Debug.WriteLine("Defining circle prim");
            int radius = 5;
            primitiveCircle = new Texture2D(graphics, radius*2, radius*2, false, SurfaceFormat.Color);
            
            Color[] pixels = new Color[radius * radius * 4];
            Vector2 center = new Vector2(primitiveCircle.Width,primitiveCircle.Height)/2;
            for(int i=0;i<radius*2;i++)
                for (int j = 0; j < radius*2;j++)
                {
                    Vector2 point = new Vector2(i, j) + (Vector2.One/2);
                    if (Vector2.Distance(center, point) < radius)
                        pixels[(i * (radius * 2)) + j] = Color.White;
                }

            primitiveCircle.SetData(pixels);
        }

       

        public static SpriteEffect DrawLinePrimitive(Color color, Vector2 position, float angle, Vector2 scale)
        {
            SpriteEffect d = new SpriteEffect();

            d.SetSprite(primitivePixel);
            d.Rotation = angle;
            d.Scale = scale;
            d.Color = color;
            d.ScreenPosition = position;
            return d;
        }

        public static SpriteEffect DrawCirclePrimitive(Color color, Vector2 position, float radius)
        {
            SpriteEffect d = new SpriteEffect();

            d.SetSprite(primitiveCircle);
            d.Scale = new Vector2(radius,radius)/ 10;
            d.Color = color;
            d.ScreenPosition = position;
            return d;
        }

        
    }
}

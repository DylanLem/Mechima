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

        public static void Initiate(GraphicsDevice graphics)
        {
            primitivePixel = new Texture2D(graphics, 1, 1,false,SurfaceFormat.Color);
            primitivePixel.SetData(new[] { Color.White  });
        }

       

        public static SpriteEffect MakeLinePrimitive(Color color, Vector2 position, float angle, Vector2 scale)
        {
            SpriteEffect d = new SpriteEffect();

            d.SetSprite(primitivePixel);
            d.Rotation = angle;
            d.Scale = scale;
            d.Color = color;
            d.ScreenPosition = position;
            return d;
        }
    }
}

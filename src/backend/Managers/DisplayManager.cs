using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Mechima
{

    /// <summary>
    /// Dirty manager class that processes graphics related requests 
    /// Keeps track of all drawn entities
    /// 
    /// todo: 
    ///     Occlusion culling (once camera is developed)
    ///     shader & effects management
    /// </summary>
    public static class DisplayManager
    {
        public static Vector2 Resolution = new Vector2(1280, 720);

        public static List<Drawable> Drawables = new List<Drawable>();
        public static List<Drawable> PoppedDrawables = new List<Drawable>();
        public static List<Drawable> QueuedDrawables = new List<Drawable>();



        public static GraphicsDeviceManager graphicsDevice;

        private static List<BlitRequest> spriteBlitQueue = new List<BlitRequest>();
        private static List<BlitRequest> stringBlitQueue = new List<BlitRequest>();

        public static SpriteFont defaultFont;


        public static Dictionary<string, Texture2D> spriteMap = new Dictionary<string, Texture2D>();
        private static Dictionary<string, AnimData> AnimationMap = new Dictionary<string, AnimData>();


        public static readonly List<string> spritePaths = new List<string>()
        {
            "player_new", "headsheet","crosshair","sword","knight-sheet", "testdummy", "bowey", "laser", "bow-sheet","arrow"
        };



        public static void Update(GameTime gameTime)
        {
            foreach (Drawable drawable in PoppedDrawables)
                Drawables.Remove(drawable);
            PoppedDrawables.Clear();


            foreach (Drawable drawable in Drawables)
            {
                drawable.Update();
            }

            foreach (Drawable drawable in QueuedDrawables)
                Drawables.Add(drawable);
            QueuedDrawables.Clear();

            
        }


        public static void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,null, null, null, null);
            
            foreach(Drawable drawable in Drawables)
            {
                drawable.Draw(spriteBatch);
            }
                

            foreach(BlitRequest request in stringBlitQueue)
                spriteBatch.DrawString(defaultFont, request.Message, request.ScreenPosition, request.Color, request.Rotation, request.Origin, request.Scale, SpriteEffects.None, 1f);
            foreach (BlitRequest request in spriteBlitQueue)
                spriteBatch.Draw(request.Sprite, request.ScreenPosition, null, request.Color, request.Rotation, request.Origin, request.Scale, SpriteEffects.None, 1f);

            spriteBatch.End();

            spriteBlitQueue.Clear();
            stringBlitQueue.Clear();
        }


        public static void LoadTextures(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            foreach (string spritename in spritePaths)
                spriteMap[spritename] = content.Load<Texture2D>(spritename);
        }

        public static void AddSprite(Drawable drawable)
        {
            QueuedDrawables.Add(drawable);
        }

        //Blits are destroyed on the frame they are created
        public static void RequestBlit(BlitRequest request)
        {
            if (request.RequestType == null)
                throw new Exception("Blit request type not recognized :( heres what you tried to blit:" + request.ToString());

            if (request.RequestType == "sprite")
                spriteBlitQueue.Add(request);

            if (request.RequestType == "string")
                stringBlitQueue.Add(request);
        }



        public static AnimData GetAnim(string spriteName)
        {
            if (!AnimationMap.ContainsKey(spriteName))
                AnimationMap[spriteName] = ContentManager.LoadAnimation(spriteName);

           

            return ((AnimData)AnimationMap[spriteName]?.Clone());
        }



        public static void SetScreenResolution(int x, int y)
        {
            graphicsDevice.PreferredBackBufferWidth = x;
            graphicsDevice.PreferredBackBufferHeight = y;
            graphicsDevice.ApplyChanges();
        }



        //converts anchorpoint enum value to texture offset vector between 0 and 1
        //I hate switch statements like this but i dont have a better idea for mapping these values.
        public static Vector2 GetAnchorVector(Vector2 TextureDimension, AnchorPoint anchor)
        {
            switch (anchor)
            {
                case AnchorPoint.TopLeft:
                    return Vector2.Zero;
                case AnchorPoint.TopCenter:
                    return new Vector2(0.5f * TextureDimension.X, 0);
                case AnchorPoint.TopRight:
                    return new Vector2(1f * TextureDimension.X, 0);
                case AnchorPoint.CenterLeft:
                    return new Vector2(0, 0.5f * TextureDimension.Y);
                case AnchorPoint.Center:
                    return new Vector2(0.5f * TextureDimension.X, 0.5f * TextureDimension.Y);
                case AnchorPoint.CenterRight:
                    return new Vector2(1.0f * TextureDimension.X, 0.5f * TextureDimension.Y);
                case AnchorPoint.BottomLeft:
                    return new Vector2(0f, 1.0f * TextureDimension.Y);
                case AnchorPoint.BottomCenter:
                    return new Vector2(0.5f * TextureDimension.X, 1.0f * TextureDimension.Y);
                case AnchorPoint.BottomRight:
                    return new Vector2(1.0f * TextureDimension.X, 1.0f * TextureDimension.Y);

                default:
                    throw new Exception("wtf anchorpoint is this: " + anchor);
            }
        }

    }
}

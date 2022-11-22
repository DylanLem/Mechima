using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mechima
{
    public struct BlitRequest
    {
        public string RequestType { get { return type; } set { type = value != "sprite" && value != "string" ? null : value; } }
        private string type { get; set; }

        public string Message;
        public Texture2D Sprite;
        public Color Color;
        public Vector2 ScreenPosition;

        public BlitRequest( string _message, Color _color, Vector2 _screenPosition)
        {
            type = "string";
            Message = _message;
            Color = _color;
            Sprite = null;
            ScreenPosition = _screenPosition;
        }

        public BlitRequest(string _type, Texture2D _sprite, Color _color, Vector2 _screenPosition)
        {
            type = "sprite";
            Message = null;
            Color = _color;
            Sprite = _sprite;
            ScreenPosition = _screenPosition;
        }


        public override string ToString()
        {
            string s = String.Format("blitrequest type:{0}", RequestType);
            return base.ToString();
        }
    }


    public static class DisplayManager
    {
        public static Vector2 Resolution = new Vector2(1280, 720);

        public static List<Drawable> Drawables = new List<Drawable>();
        public static GraphicsDeviceManager graphicsDevice;

        private static List<BlitRequest> spriteBlitQueue = new List<BlitRequest>();
        private static List<BlitRequest> stringBlitQueue = new List<BlitRequest>();

        public static SpriteFont defaultFont;


        public static Dictionary<string, Texture2D> spriteMap = new Dictionary<string, Texture2D>();
        public static Dictionary<string, AnimData> animationMap = new Dictionary<string, AnimData>();


        public static readonly List<string> spritePaths = new List<string>()
        {
            "player_new", "headsheet","crosshair","sword"
        };





        public static void Update(GameTime gameTime)
        {
            foreach(Drawable drawable in Drawables)
            {
                drawable.Animate(gameTime);
                drawable.DetermineScreenPosition();
            }
        }


        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp,null, null, null, null);

            foreach(Drawable drawable in Drawables)
                drawable.Draw(spriteBatch);

            foreach(BlitRequest request in stringBlitQueue)
                spriteBatch.DrawString(defaultFont, request.Message, request.ScreenPosition, request.Color);
            foreach (BlitRequest request in spriteBlitQueue)
                spriteBatch.Draw(request.Sprite, request.ScreenPosition, request.Color);

            spriteBatch.End();

            spriteBlitQueue.Clear();
            stringBlitQueue.Clear();
        }


        public static void RequestBlit(BlitRequest request)
        {
            if (request.RequestType == null)
                throw new Exception("Blit request type not recognized :( heres what you tried to blit:" + request.ToString());

            if (request.RequestType == "sprite")
                spriteBlitQueue.Add(request);

            if (request.RequestType == "string")
                stringBlitQueue.Add(request);
        }

        public static void SetScreenResolution(int x, int y)
        {
            graphicsDevice.PreferredBackBufferWidth = x;
            graphicsDevice.PreferredBackBufferHeight = y;
            graphicsDevice.ApplyChanges();
        }

    }
}

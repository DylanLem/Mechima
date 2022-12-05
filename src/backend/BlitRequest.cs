using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    //originally just a hacky way to draw debug data to the screen while testing
    //probably going to be removed in favour of SpriteEffect
    public class BlitRequest
    {

        public string RequestType { get { return type; } set { type = value != "sprite" && value != "string" ? null : value; } }
        private string type { get; set; }

        public string Message;
        public Texture2D Sprite;
        public Color Color;
        public Vector2 ScreenPosition;

        public float Rotation;
        public float Scale;
        public Vector2 Origin;


        public BlitRequest(string _message, Color _color, Vector2 _screenPosition, AnchorPoint anchor, float _rotation = 0, float _scale = 1)
        {
            type = "string";
            Message = _message;
            Color = _color;
            Sprite = null;
            ScreenPosition = _screenPosition ;
            Origin = DisplayManager.GetAnchorVector(DisplayManager.defaultFont.MeasureString(Message), anchor);
            
            Rotation = _rotation;
            Scale = _scale;
        }

        public BlitRequest(Texture2D _sprite, Color _color, Vector2 _screenPosition, AnchorPoint anchor, float _rotation = 0, float _scale = 1)
        {
            type = "sprite";
            Message = null;
            Color = _color;
            Sprite = _sprite;
            ScreenPosition = _screenPosition - (_scale * DisplayManager.GetAnchorVector(new Vector2(Sprite.Width, Sprite.Height), anchor));
            Origin = DisplayManager.GetAnchorVector(DisplayManager.defaultFont.MeasureString(Message), anchor);
            Rotation = _rotation;
            Scale = _scale;
        }


        public override string ToString()
        {
            string s = String.Format("blitrequest type:{0}", RequestType);
            return base.ToString();
        }


    }
}
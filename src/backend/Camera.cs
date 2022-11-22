using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    public class Camera
    {

        //The imaginary aperature of the camera in the gameworld. Has world position and size
        public Rectangle Viewport;

        public Vector2 WorldPosition { get => new Vector2(Viewport.X, Viewport.Y); }
        public Vector2 WorldSize { get => new Vector2(Viewport.Width, Viewport.Height); }

        //How much of the screen the camera takes up and where it is located.
        public Rectangle ScreenDimensions;
        public Vector2 ScreenPosition { get => new Vector2(ScreenDimensions.X, ScreenDimensions.Y); }
        public Vector2 ScreenSize { get => new Vector2(ScreenDimensions.Width, ScreenDimensions.Height); }
    }
}

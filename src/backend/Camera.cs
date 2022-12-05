using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    //This is a stub!
    //The camera will serve as a stepping stone when transforming world coordinates to screen coordinates
    //Process: Camera is positioned in world -> Game objects in the world are checked vs. the camera's position -> Objects in aperture are rendered to camera -> camera viewport is rendered to screen
    //This allows for things like zooming in and out in game, as well as managing dynamic screen resolution
    //e.g. UI items are anchored to specific points on camera's render plane
    // camera's render plane can undergo transformations to fit desired resolution without having to touch game sprites
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

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mechima
{
    class MouseMoveEventArgs : EventArgs
    {
        public Vector2 Position { get; set; }

        public MouseMoveEventArgs(Vector2 position)
        {
            Position = position;
        }
    }
}

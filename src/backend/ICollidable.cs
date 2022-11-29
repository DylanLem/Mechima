using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace Mechima
{
    public interface ICollidable
    {
        virtual void OnCollisionEnter() { }

        virtual void OnCollisionExit() { }

        Circle Collider { get; }

        bool CheckCollision(ICollidable collidable);
    }
}

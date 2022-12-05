using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace Mechima
{
    /// <summary>
    /// Implements collision on a given object. Inheriting from classes that implement ICollidable is not recommended unless child class uses the same collision logic.
    /// </summary>
    public interface ICollidable
    {
        virtual void OnCollisionEnter() { }

        virtual void OnCollisionExit() { }

        Circle Collider { get; }

        bool CheckCollision(ICollidable collidable);
    }
}

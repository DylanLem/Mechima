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

        List<ICollidable> CollidedObjects { get; set; }

        Circle Collider { get; set; }

        public void AddCollide(ICollidable collidable)
        {
            if (CollidedObjects.Contains(collidable)) return;

            OnCollisionEnter();

        }

        public void RemoveCollide(ICollidable collidable)
        {

        }

    }
}

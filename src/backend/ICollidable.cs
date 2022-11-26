using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public interface ICollidable
    {
        virtual void OnCollisionEnter() { }

        virtual void OnCollisionExit() { }

        List<ICollidable> CollidedObjects { get; set; }

    }
}

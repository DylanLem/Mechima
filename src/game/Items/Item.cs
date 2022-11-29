using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    public abstract class Item: Entity
    {

        public ControllableEntity Parent;
        public bool IsAnchored;
        


        public override void Update(GameTime gameTime)
        {
            if (IsAnchored && Parent != null)

                base.Update(gameTime);
        }

        

        public  virtual void SetParent(ControllableEntity e, bool anchored = false)
        {
            this.Parent = e;
            this.IsAnchored = anchored;
        }
    }
}

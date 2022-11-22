using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Mechima
{
    public abstract class Equipment : Entity
    {
        public string Name { get; private set; }
        public int PowerCost { get; private set; }
        public int Rarity { get; private set; }



        public Entity Parent;
        public bool IsAnchored { get; set; }

        public Equipment(Texture2D sprite=null):base(sprite)
        {

        }

        public override void Update(GameTime gameTime)
        { 
            if(IsAnchored && Parent != null)

            base.Update(gameTime);
        }

        public virtual void Activate()
        {
            
        }

        public void SetParent(Entity e, bool anchored = false)
        {
            this.Parent = e;
            this.IsAnchored = anchored;
        }
    }
}

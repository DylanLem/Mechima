using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    
    public abstract class Item: Entity
    {

        public Creature ParentCreature;
        public bool IsAnchored;
        

        public float orbitDistance { get => distanceOffset + (this.Scale.Y * (float)this.SpriteCell.Height); }
        public Vector2 orbitVector { get => GameManager.MakeVector(Rotation , orbitDistance); }
        public float distanceOffset = 15f;
        public float angleOffset = 0f;
        public float coolTimer;

        public abstract List<ItemTag> Tags { get; }

        public float lookSpeed = 0.15f;
        public override void Update(GameTime gameTime)
        {
            if (IsAnchored && ParentCreature != null)

                base.Update(gameTime);
        }

        

        public  virtual void SetParent(Creature e, bool anchored = true)
        {
            this.ParentCreature = e;
            this.IsAnchored = anchored;
        }
    }
}

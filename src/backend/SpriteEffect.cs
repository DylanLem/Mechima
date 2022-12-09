using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    /// <summary>
    /// WIP class for drawing sprites to the screen temporarily
    /// </summary>
    public class SpriteEffect: Drawable
    {
        public float LifeTime { get; set; } = 0;
        private float timer = 0;

        public SpriteEffect() : base()
        {
            IsDrawn = true;
        }
        public override void Update()
        {
            
            
            if (timer > LifeTime)
                this.DeleteDrawable();

            timer += GameManager.lastTick;

            base.Update();
        }

    }
}

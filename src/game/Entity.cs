using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Mechima
{
    /*
     * ##This might need retooling##
     * 
     * Entity: Base class for all game objects that behave with some sort of logic
     * 
     * Encapsulates items, mobs, etc
     * 
     * Things that ARENT entities: 
     *      -Hitboxes
     *      -Effects
     *      -UI
     *      -Walls
     *      -etc.
     * 
     * Issues:
     *      
     *      
     *      
     */
    public abstract class Entity : Drawable
    {
        public Vector2 WorldPosition { get; set; }
        public Vector2 Velocity { get; set; }


        public virtual void Update(GameTime gameTime)
        { 
            if (Texture == null)
                Enabled = false;
            
            if (this.Velocity.Length() > 0) Move(this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                
        }


        
        public void Move(Vector2 displacement)
        {
            GameManager.TryMove(this, this.WorldPosition + displacement);
        }

        public override void DetermineScreenPosition()
        {

            this.ScreenPosition =  this.WorldPosition - GameManager.MainCam.WorldPosition;
            
        }


        



    }


}

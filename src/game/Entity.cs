using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Mechima
{
    /// <summary>
    /// The main prototype for any class that intends to "exist" within the game and operate as a world object.
    /// </summary>
    public abstract class Entity : Drawable
    {
        public Vector2 WorldPosition { get; set; } = Vector2.Zero;
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 ForceVector { get; set; } = Vector2.Zero; //This might be reworked but something similar will be used.



        //Entities can have various attributes and can be indexed entity[x] where x=string. This is going to be redone in favor of a proper attribute system.
        protected Dictionary<string, float> Attributes { get; set; } = new Dictionary<string, float>();
        public Dictionary<string, float> Modifiers { get; set; } = new Dictionary<string, float>();


        //Entities may or may not have a referencable parent.
        public Entity ParentEntity { get; set; }


        //Entities may be indexed for their stats and attributes
        public float this[string key]
        {
            get { return CalculateStat(key); }
            set { Attributes[key] = value; }
        }

        private float CalculateStat(string name)
        {
            Modifiers.TryGetValue(name, out float modval);
            Attributes.TryGetValue(name, out float statval);
            modval += statval;
            return modval;
        }

        public void AddModifier(string name, float value)
        {
            if (Modifiers.ContainsKey(name))
                Modifiers[name] += value;
            else
                Modifiers[name] = value;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Texture == null)
                IsDrawn = false;

            this.Velocity += ForceVector;

            if (this.Velocity.Length() > 0) 
            {
                Move((this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds));
                this.Velocity = (this.Velocity * (1-(this["drag"]*GameManager.lastTick)));
            } 

            ForceVector = Vector2.Zero;

            this.Modifiers.Clear();
        }


        //I play fast and loose at the moment for allowing entities to control their own movement logic. we'll see 
        public void Move(Vector2 displacement)
        {
            GameManager.TryMove(this, this.WorldPosition + displacement);
        }


        //Technically a stub right now
        public override void DetermineScreenPosition()
        {

            this.ScreenPosition =  this.WorldPosition - GameManager.MainCam.WorldPosition;
            
        }
        




    }


}

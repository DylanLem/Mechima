﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Mechima
{

    public abstract class Entity : Drawable
    {
        public Vector2 WorldPosition { get; set; } = Vector2.Zero;
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 ForceVector { get; set; } = Vector2.Zero;

        protected Dictionary<string, float> Attributes { get; set; } = new Dictionary<string, float>();
        public Dictionary<string, float> Modifiers { get; set; } = new Dictionary<string, float>();

        public Entity ParentEntity;


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

            if (this.Velocity.Length() > 0) Move(this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            ForceVector = Vector2.Zero;

            this.Modifiers.Clear();
        }


        
        public void Move(Vector2 displacement)
        {
            GameManager.TryMove(this, this.WorldPosition + displacement);
        }

        public override void DetermineScreenPosition()
        {

            this.ScreenPosition =  this.WorldPosition - GameManager.MainCam.WorldPosition;
            
        }
        
        public Vector2 GetPosition()
        {
            return this.WorldPosition;
        }



    }


}

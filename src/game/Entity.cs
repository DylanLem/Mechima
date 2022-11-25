using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace Mechima
{
    public abstract class Entity : Drawable, IControllable
    {
        public Vector2 WorldPosition { get; set; }
        public Vector2 Velocity { get; set; }
        public Dictionary<ActionType, Action> ActionMap { get; set; }

        public List<ActionType> QueuedActions { get; set; } = new List<ActionType>();

        private bool hasCollision;
        

        public bool isControlled { get; set; }


        protected bool HasStats;
        public Dictionary<string, float> Modifiers { get; set; } = new Dictionary<string, float>();
        public Dictionary<string, float> Stats { get; set; } = new Dictionary<string, float>();

            


        public virtual void Update(GameTime gameTime) 
        {
            IControllable thisCollidable = (IControllable)this;

            thisCollidable.InvokeQueuedActions();

            if (Texture == null)
                Enabled = false;
            
            if (this.Velocity.Length() > 0) Move(this.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);



            this.Modifiers.Clear();

                
        }


        //Please make sure u dont erase the base function call when moving your derived objects
        public virtual void Move(Vector2 displacement)
        {
            GameManager.TryMove(this, this.WorldPosition + displacement);
        }

        public override void DetermineScreenPosition()
        {

            this.ScreenPosition =  this.WorldPosition - GameManager.MainCam.WorldPosition;
            
        }


        public void AddModifier(string stat, float value)
        {
            if (!this.Modifiers.ContainsKey(stat))
                this.Modifiers[stat] = value;
            else
                this.Modifiers[stat] += value;
        }

        public float GetStat(string stat)
        {
            float valmod = 0;
            this.Modifiers.TryGetValue(stat, out valmod);
            float val = 0;
            this.Stats.TryGetValue(stat, out val);
            
            return val + valmod;
        }



    }


}

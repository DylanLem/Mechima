using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Mech : Entity
    {

        

        public Dictionary<string, Equipment> Equipment { get;private set; } = new Dictionary<string, Equipment>() 
        {
            {"Primary", null},
            {"Secondary", null },
        };
        
        
        public Mech(Texture2D sprite=null) : base(sprite) 
        {
            HasStats = true;

            Stats  = new Dictionary<string, float>()
            {
                {"health",0 },
                { "maxThrust", 100},
                { "acceleration", 100 },
                { "stamina", 5},
                { "drag", 1.005f },
                { "stoppingPower", 1.15f }
            };


            this.ActionMap = new Dictionary<ActionType, Action>()
            {
                {ActionType.MoveDown, () =>  this.ApplyThrust(new Vector2(0,1))},
                {ActionType.MoveLeft, () =>  this.ApplyThrust(new Vector2(-1,0))},
                {ActionType.MoveRight, () =>  this.ApplyThrust(new Vector2(1,0))},
                {ActionType.MoveUp, () =>  this.ApplyThrust(new Vector2(0,-1))},
                {ActionType.Primary, () => this.Equipment["Primary"]?.Activate()},
                {ActionType.Secondary, () => this.Airbrake() }      
            };

        }


        public override void Update(GameTime gameTime)
        {


            

            base.Update(gameTime);


            this.Color = Color.White;
        }

        public override void Move(Vector2 displacement)
        {
            

            if (this.GetStat("acceleration") > 150)
                System.Diagnostics.Debug.WriteLine("fuck"); 

            if (this.Velocity.Length() > 0)
                this.Velocity /= GetStat("drag");

            if (Velocity.Length() < 0.05)
                this.Velocity = Vector2.Zero;

            base.Move(displacement);
        }

        public void EquipItem(Equipment equipment, string slot)
        {
            if (! this.Equipment.ContainsKey(slot))
                return;

            this.Equipment[slot] = equipment;
            equipment.Parent = this;
        }

        public void ApplyThrust(Vector2 direction)
        {
            DisplayManager.RequestBlit(new BlitRequest(this.GetStat("acceleration").ToString(), Color.White, this.ScreenPosition));

            Vector2 force = GetStat("acceleration") * direction * GameManager.lastTick;

            this.Velocity = this.Velocity + force;
            if(this.Velocity.Length() > GetStat("maxThrust")) 
                NormalizeToMagnitude(Velocity, GetStat("maxThrust"));
        }


        public Vector2 NormalizeToMagnitude(Vector2 vector, float magnitude)
        {
            return Vector2.Normalize(vector) * magnitude;
        }


        public void Shoot()
        {

        }

        
        public void Airbrake()
        {
            this.Color = new Color(0.95f, 0, 0);

            this.Velocity -= this.Velocity * (3 * GameManager.lastTick);
            AddModifier("acceleration", 250f);
            
        }



    }
}

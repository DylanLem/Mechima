using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Mech : ControllableEntity
    {

        private Vector2 thrustVector;


       
        public Dictionary<string, Equipment> Equipment { get;private set; } = new Dictionary<string, Equipment>() 
        {
            {"Primary", null},
            {"Secondary", null },
        };

        
        public Mech() : base() 
        {

            this.ActionMap = new Dictionary<ActionType, Action>()
            {
                {ActionType.MoveDown, () =>  this.ApplyThrust(new Vector2(0,1))},
                {ActionType.MoveLeft, () =>  this.ApplyThrust(new Vector2(-1,0))},
                {ActionType.MoveRight, () =>  this.ApplyThrust(new Vector2(1,0))},
                {ActionType.MoveUp, () =>  this.ApplyThrust(new Vector2(0,-1))},
                {ActionType.Primary, () => this.Equipment["Primary"]?.Activate()},
                {ActionType.Secondary, () => this.Airbrake() }      
            };

            Stats = new Dictionary<string, float>()
            {
                {"health",0 },
                { "maxThrust", 100},
                { "acceleration", 100 },
                { "stamina", 5},
                { "drag", 1.005f },
                { "stoppingPower", 1.15f }
            };

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

           

            


            this.Color = Color.White;


            if (thrustVector.Length() == 0)
                this.AnimData.SetAnim(AnimationState.Default);
            else if (thrustVector.X < 0)
                this.AnimData.SetAnim(AnimationState.MoveLeft);
            else if (thrustVector.X > 0)
                this.AnimData.SetAnim(AnimationState.MoveRight);

            thrustVector = Vector2.Zero;
            DisplayManager.RequestBlit(new BlitRequest("accel: " + ((IHasStats)this).GetStat("acceleration").ToString(), Color.White, this.ScreenPosition, AnchorPoint.Center, 0, 1));
            
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
            

            Vector2 force = ((IHasStats)this).GetStat("acceleration") * direction * GameManager.lastTick;
            thrustVector += force;

            

            this.Velocity = this.Velocity + force;
            if(this.Velocity.Length() > ((IHasStats)this).GetStat("maxThrust")) 
                NormalizeToMagnitude(Velocity, ((IHasStats)this).GetStat("maxThrust"));

            
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
            ((IHasStats)this).AddModifier("acceleration", 250f);
            
        }



    }
}

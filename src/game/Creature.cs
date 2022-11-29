using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Creature : ControllableEntity
    {

        private Vector2 thrustVector;


       
        public Dictionary<string, Equipment> Equipment { get;private set; } = new Dictionary<string, Equipment>() 
        {
            {"Primary", null},
            {"Secondary", null },
            {"Propulsion", null }
        };


        Dictionary<string,float> Stats = new Dictionary<string, float>()
            {
                {"health",0 },
                { "maxThrust", 100},
                { "acceleration", 100 },
                { "stamina", 5},
                { "drag", 1.005f },
                { "stoppingPower", 1.15f }
            };


    public Creature() 
        {
            Scale = new Vector2(2f, 2f);

            this["health"] = 10;
            this["maxThrust"] = 100;
            this["acceleration"] = 100;
            this["stamina"] = 5;
            this["drag"] = 1.0005f;
            this["stoppingPower"] = 1.15f;

            this.ActionMap = new Dictionary<ActionType, Action>()
            {
                {ActionType.MoveDown, null},
                {ActionType.MoveLeft, null},
                {ActionType.MoveRight, null},
                {ActionType.MoveUp, null},
                {ActionType.Primary, null},
                {ActionType.Secondary, null}      
            };
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);



            if (this.Velocity.Length() > 0)
                this.Velocity -= (this.Velocity / this["drag"]) * GameManager.lastTick;


            this.Color = Color.White;


            if (thrustVector.Length() == 0)
                this.AnimData.SetAnim(AnimationState.Default);
            else if (thrustVector.X < 0)
                this.AnimData.SetAnim(AnimationState.MoveLeft);
            else if (thrustVector.X > 0)
                this.AnimData.SetAnim(AnimationState.MoveRight);

            thrustVector = Vector2.Zero;
            DisplayManager.RequestBlit(new BlitRequest("accel: " + this["acceleration"].ToString(), Color.White, this.ScreenPosition, AnchorPoint.Center, 0, 1));
            
        }



        public void EquipItem(Equipment equipment, string slot)
        {
            if (! this.Equipment.ContainsKey(slot))
                return;

            this.Equipment[slot] = equipment;
            equipment.Parent = this;
            equipment.Enable();
        }

        


        

        
        public void Airbrake()
        {
            this.Color = new Color(0.95f, 0, 0);

            this.Velocity -= this.Velocity * (3 * GameManager.lastTick);
            this.AddModifier("acceleration", 250f);
            
        }



    }
}

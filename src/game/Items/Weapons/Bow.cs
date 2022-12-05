using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Bow: Ability
    {
        public override Dictionary<ActionType, Action> Abilities
        {
            get =>
            new Dictionary<ActionType, Action>() {
                    {ActionType.Primary, DrawArrow},
                    {ActionType.Action04, Release}
            };
        }

        public override List<ItemTag> Tags 
        {
            get =>
                new List<ItemTag>() { ItemTag.Damage }; 

        }

        Projectile projectile;


        public Bow()
        {
            
            
            

            Scale = Vector2.One * 3;
            this["coolDown"] = 1.0f;
            this["damage"] = 10;
            this["maxCharge"] = 3;
            this["currentCharge"] = 0;
            this["chargeSpeed"] = 1;
            this["firePower"] = 400;
        }

        public override void Update(GameTime gameTime)
        {
            this.RotateFromParent();

            this.WorldPosition = this.ParentCreature.WorldPosition + this.orbitVector;

            if (CurrentState == State.CoolDown)
            {
                coolTimer -= GameManager.lastTick;
                this.Color = new Color(coolTimer/this["coolDown"], 0, 0);
                
                if(coolTimer <= 0)
                {
                    coolTimer = 0;
                    CurrentState = State.Ready;
                    this.Color = Color.White;
                }
            }

            if (CurrentState == State.Active)
            {
                Vector2 laserLine = GameManager.MakeVector(this.Rotation, 1350);
               
                Graphics.MakeLinePrimitive(Color.Red*0.1f, this.WorldPosition + (laserLine / 2), Rotation, new Vector2(1,laserLine.Length()));

                projectile.Rotation = this.Rotation;
                projectile.WorldPosition = this.WorldPosition;
                if (this["currentCharge"] <= this["maxCharge"])
                {
                    this["currentCharge"] += GameManager.lastTick * this["chargeSpeed"];
                    this.Color = new Color(0, 0, this["currentCharge"] / this["maxCharge"] - 0.01f);
                    projectile.WorldPosition -= this.orbitVector.NormalizeToMagnitude(3) * (projectile.Texture.Height/2) * this["currentCharge"]/this["maxCharge"] ;
                }
                else
                {
                    this["currentCharge"] = this["maxCharge"];
                    this.Color = new Color(0, 0.99f, 0);
                }

                
            }

            base.Update(gameTime);
        }

        public void DrawArrow()
        {
            

            if (CurrentState != State.Ready)
                return;
            Projectile proj = (Projectile)GameManager.AddEntity(new Projectile());
            proj.SetSprite("laser");
            proj.Scale = Vector2.One * 2;
            proj.WorldPosition = this.WorldPosition;
            proj.ParentEntity = this;
            proj.Owner = this.ParentCreature;

            this.projectile = proj;
            CurrentState = State.Active;
            
        }

        public void Release()
        {
            

            if (CurrentState == State.Active)
            {
                if (this["currentCharge"] >= 1)
                    Fire();
                else
                {
                    projectile.Delete();
                }

                CurrentState = State.CoolDown;
                coolTimer = this["coolDown"];
                this["currentCharge"] = 0;
                
            }

            
                
        }

        public void Fire()
        {
            projectile.ParentEntity = null;
            projectile.Velocity = GameManager.MakeVector(projectile.Rotation,this["firePower"] * this["currentCharge"]);
            projectile.isActive = true;
            this.projectile = null;
            
        }
    }
}

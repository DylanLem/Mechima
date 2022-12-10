using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Sword : Ability, ICollidable
    {
        public override Dictionary<ActionType, Action> Abilities { get =>
                new Dictionary<ActionType, Action>() {
                    {ActionType.Primary, Activate}
                };
        }


        public Circle Collider { get => new Circle() {Position=this.WorldPosition,
                                               Radius = this.Texture.Width/2};
            set => this.Collider = value;
        }

        public override List<ItemTag> Tags { get => new List<ItemTag>() { ItemTag.Damage }; }

        

        



        private float swingOffset = 0f; //the current angular offset due to swing animation
        

       

        
        
        

        
        
        public Sword()
        {
            this["swingRange"] = 1 * MathF.PI;;
            this["swingTime"] = 3.0f;
            this["coolDown"] = 1.0f;
            this["damage"] = 10;


            Scale = new Vector2(2, 2);

            angleOffset = -MathF.PI/2;
            
            
        }

       
        public override void Update()
        {

            if (ParentCreature != null)
            {
                this.Rotation -= (angleOffset + swingOffset);
                this.RotateFromParent();
                this.Rotation += angleOffset + swingOffset;

                this.WorldPosition = this.ParentCreature.WorldPosition + this.orbitVector;

            }


            this.Color = Color.White;



            if (CurrentState == State.Active)
            {
                this.lookSpeed = 0.95f;
                this.Color = new Color(0, 0, 0.8f);

                if (swingOffset < this["swingRange"])
                    swingOffset += (MathF.PI * 2 * this["swingTime"]) * GameManager.lastTick;
                else
                {
                    CurrentState = State.CoolDown;
                    this.coolTimer = this["coolDown"];
                }

                foreach (ICollidable collidable in GameManager.GetCollidables())
                {
                    if (CheckCollision(collidable))
                    {
                        
                        DisplayManager.RequestBlit(new BlitRequest("HIT!", Color.White, this.ScreenPosition + new Vector2(0, 10), AnchorPoint.TopCenter));
                        
                        if (collidable is Entity entity)
                        {
                            Vector2 d = (entity.WorldPosition - ParentCreature.WorldPosition);
                            d.Normalize();
                            entity.ForceVector += (d * 15);
                        }

                    }
                }
            }


            if (CurrentState == State.CoolDown)
            {
                this.lookSpeed = 0.1f;
                this.Color = new Color(0.8f, 0, 0);

                swingOffset %= (MathF.PI*2);
                this.coolTimer -= GameManager.lastTick;


                if (swingOffset > 0.01)
                    swingOffset -= this["swingRange"] / (this["coolDown"] / (GameManager.lastTick * 2));


                else if(this.coolTimer <= 0)
                {
                    swingOffset = 0;
                    CurrentState = State.Ready;
                    this.Color = new Color(0, 0.8f, 0);
                }
            }

            if (CurrentState == State.Ready)
            {
                this.lookSpeed = 0.15f;
            }

            base.Update();
        }

        public bool CheckCollision(ICollidable collidable)
        {           
            float range = orbitDistance + ParentCreature.SpriteCell.Width/2;


            if (collidable == this || collidable == ParentCreature)
                return false;


            float distance = Vector2.Distance(ParentCreature.WorldPosition, collidable.Collider.Position) - collidable.Collider.Radius;



            if (distance > range || distance < distanceOffset)
                return false;

                

            Vector2 displacement =  collidable.Collider.Position - ParentCreature.WorldPosition;

                

            float angle = MathF.Atan2(displacement.Y, displacement.X) + (MathF.PI/2);

                
            if(MathF.Abs(angle - this.Rotation) < (MathF.PI * 2 * this["swingTime"]) * GameManager.lastTick)              
                return true;


            return false;
        }

        public void Activate()
        {
            
            if (CurrentState != State.Ready) return;
            CurrentState = State.Active;
            
        }

        private float CalculateDamage()
        {
            float mouseFactor = MathF.Abs((2 * MathF.PI) / ParentCreature.ScreenPosition.GetAngleFromMouse());
            float swingFactor = 1.2f * this.swingOffset / this["swingRange"];
            float speedFactor = MathF.Max(ParentCreature.Velocity.Length(),500);

            return this["damage"] * swingFactor * MathF.Max((speedFactor / 100), 1);

        }

    }
}

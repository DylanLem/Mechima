using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Sword : Equipment, ICollidable
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

        private enum State
        {
            Swinging, UnSwinging, Ready
        }

        private State currentState = State.Ready;



        private float swingOffset = 0f; //the current angular offset due to swing animation
        private float angleOffset;

        private float distanceOffset = 15f;
        private float orbitDistance { get => distanceOffset + (this.Scale.Y * (float)this.Texture?.Height); }
        private float lookSpeed;

        private float coolTimer;
        
        

        private Vector2 orbitVector { get => GameManager.MakeVector(Rotation, orbitDistance); }
        
        public Sword()
        {
            this["swingRange"] = 1 * MathF.PI;;
            this["swingTime"] = 3.0f;
            this["coolDown"] = 1.0f;
            this["damage"] = 10;


            Scale = new Vector2(2, 2);

            angleOffset = 0;
            
            this.lookSpeed = 0.15f;
        }

        public override void Update(GameTime gameTime)
        {

            if (Parent != null)
            {
                float newRot = GameManager.lerpRotation(this.Rotation - this.angleOffset - swingOffset, Parent.ScreenPosition.GetAngleFromMouse(), this.lookSpeed);
                this.Rotation = newRot + angleOffset + swingOffset;
                this.WorldPosition = Parent.WorldPosition + orbitVector;
            }


            this.Color = Color.White;



            if (currentState == State.Swinging)
            {
                this.lookSpeed = 0.95f;
                this.Color = new Color(0, 0, 0.8f);

                if (swingOffset < this["swingRange"])
                    swingOffset += (MathF.PI * 2 * this["swingTime"]) * GameManager.lastTick;
                else
                {
                    currentState = State.UnSwinging;
                    this.coolTimer = this["coolDown"];
                }

                foreach (ICollidable collidable in GameManager.GetCollidables())
                {
                    if (CheckCollision(collidable))
                    {
                        System.Diagnostics.Debug.WriteLine(CalculateDamage());
                        DisplayManager.RequestBlit(new BlitRequest("HIT!", Color.White, this.ScreenPosition + new Vector2(0, 10), AnchorPoint.TopCenter));
                        System.Diagnostics.Debug.WriteLine("collided with: " + collidable.ToString());
                        if (collidable is Entity entity)
                        {
                            Vector2 d = (entity.WorldPosition - Parent.WorldPosition);
                            d.Normalize();
                            entity.Move(d * 15);
                        }

                    }
                }
            }


            if (currentState == State.UnSwinging)
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
                    currentState = State.Ready;
                    this.Color = new Color(0, 0.8f, 0);
                }
            }

            if (currentState == State.Ready)
            {
                this.lookSpeed = 0.15f;
            }

            base.Update(gameTime);
        }

        public bool CheckCollision(ICollidable collidable)
        {           
            float range = orbitDistance + Parent.SpriteCell.Width/2;


            if (collidable == this || collidable == Parent)
                return false;


            float distance = Vector2.Distance(Parent.WorldPosition, collidable.Collider.Position) - collidable.Collider.Radius;



            if (distance > range || distance < distanceOffset)
                return false;

                

            Vector2 displacement =  collidable.Collider.Position - Parent.WorldPosition;

                

            float angle = MathF.Atan2(displacement.Y, displacement.X) + (MathF.PI/2);

                
            if(MathF.Abs(angle - this.Rotation) < (MathF.PI * 2 * this["swingTime"]) * GameManager.lastTick)              
                return true;


            return false;
        }

        public void Activate()
        {
            
            if (currentState != State.Ready) return;
            currentState = State.Swinging;
            System.Diagnostics.Debug.WriteLine("---------------------");
        }

        private float CalculateDamage()
        {
            float mouseFactor = MathF.Abs((2 * MathF.PI) / Parent.ScreenPosition.GetAngleFromMouse());
            float swingFactor = 1.2f * this.swingOffset / this["swingRange"];
            float speedFactor = MathF.Max(Parent.Velocity.Length(),500);

            return this["damage"] * swingFactor * MathF.Max((speedFactor / 100), 1);

        }

    }
}

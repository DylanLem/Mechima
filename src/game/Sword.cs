using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public class Sword: Equipment
    {
        private enum State
        {
            Swinging, UnSwinging, Ready
        }

        private State currentState = State.Ready;

        private float swingRange; //angular distance in radians
        private float swingOffset = 0f; //the current angular offset due to swing animation
        private float angleOffset;
        private float orbitDistance;
        private float lookSpeed;

        private float swingTime;
        private float coolDown;
        private float coolTimer;
       


        private Vector2 orbitVector { get => GameManager.MakeVector(Rotation, orbitDistance); }
        
        public Sword(Texture2D sprite) : base(sprite)
        {
            this.swingTime = 3.0f;
            this.coolDown = 1.0f;
            swingRange =  1 * MathF.PI ;
            angleOffset = -MathF.PI / 2;
            this.orbitDistance = 45f;
            this.lookSpeed = 0.15f;
        }

        public override void Update(GameTime gameTime)
        {
            

            if(Parent != null)
            {
                

                float newRot = GameManager.lerpRotation(this.Rotation - this.angleOffset - swingOffset, GameManager.GetAngleFromMouse(Parent.ScreenPosition), this.lookSpeed);
                this.Rotation = newRot + angleOffset + swingOffset;
                this.WorldPosition = Parent.WorldPosition + orbitVector;

            }
            this.Color = Color.White;

            if (currentState == State.Swinging)
            {
                this.lookSpeed = 0.95f;
                this.Color = new Color(0, 0, 0.8f);

                if (swingOffset < swingRange)
                    swingOffset += (MathF.PI * 2 * swingTime) * GameManager.lastTick;
                else
                {
                    currentState = State.UnSwinging;
                    this.coolTimer = this.coolDown;
                }
                    

                                 
            }

            if (currentState == State.UnSwinging)
            {
                this.lookSpeed = 0.1f;
                this.Color = new Color(0.8f, 0, 0);

                swingOffset %= (MathF.PI*2);
                this.coolTimer -= GameManager.lastTick;


                if (swingOffset > 0.01)
                    swingOffset -= swingRange / (this.coolDown / (GameManager.lastTick * 2));


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

        public override void Activate()
        {
 
            if (currentState != State.Ready) return;
            currentState = State.Swinging;
        }

    }
}

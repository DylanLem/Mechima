using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    class Projectile: Entity, ICollidable
    {
        public bool isActive = false;
        public Creature Owner;




        public Projectile()
        {
            this["drag"] = 0.4f;
        }
        public Circle Collider
        {
            get => new Circle()
            {
                Position = this.WorldPosition + new Vector2(0,Texture.Height/2),
                Radius = (Scale.X * (float)this.SpriteCell.Width / 2)
            };
        }

        //This is garbage-ass collision rn
        public bool CheckCollision(ICollidable collidable)
        {
            if (Vector2.Distance(this.Collider.Position, collidable.Collider.Position) < this.Collider.Radius + collidable.Collider.Radius)
            {
                return true;
            }
            return false;
        }




        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                
                this.Velocity -= this["drag"] * GameManager.lastTick * this.Velocity;


                foreach (ICollidable collidable in GameManager.GetCollidables())
                {
                    if (collidable != this && collidable != this.Owner && CheckCollision(collidable))
                    {

                        DisplayManager.RequestBlit(new BlitRequest("HIT!", Color.White, this.ScreenPosition + new Vector2(0, 10), AnchorPoint.TopCenter));

                        if (collidable is Entity entity)
                        {
                            Vector2 d = GameManager.MakeVector(this.Rotation, 5);

                            entity.ForceVector += (d * (1 + MathF.Log2(Velocity.Length())));
                            this.Delete();
                        }

                    }
                }

                if (Vector2.Distance(Owner.WorldPosition, this.WorldPosition) > 2000)
                    this.Delete();
            }
            

            base.Update(gameTime);
        }

    }
}

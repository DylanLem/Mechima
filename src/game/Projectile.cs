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
        private List<ICollidable> collidedEntities = new List<ICollidable>();



        public Projectile()
        {
            this["drag"] = 0.4f;
        }
        public Circle Collider
        {
            get => new Circle()
            {
                Position = this.WorldPosition + (new Vector2(0,Texture.Height/2).Rotate(Rotation)),
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
            Graphics.DrawCirclePrimitive(Color.Red, this.Collider.Position, this.Collider.Radius);
            if (isActive)
            {
                
                this.Velocity -= this["drag"] * GameManager.lastTick * this.Velocity;


                foreach (ICollidable collidable in GameManager.GetCollidables())
                {
                    if (collidable != this && collidable != this.Owner && !collidedEntities.Contains(collidable) && CheckCollision(collidable))
                    {

                        DisplayManager.RequestBlit(new BlitRequest("HIT!", Color.White, this.ScreenPosition + new Vector2(0, 10), AnchorPoint.TopCenter));

                        if (collidable is Entity entity)
                        {
                            Vector2 d = GameManager.MakeVector(this.Rotation, 5);

                            entity.ForceVector += (d * (1 + MathF.Log2(Velocity.Length())));
                            collidedEntities.Add(collidable);
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

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace Mechima
{
    public static class Pathfinding
    {

        //Returns a normalized directional vector, checking for collideable obstacles in the way
        public static Vector2 SimpleFollow(this NPCBrain npc, Vector2 startPosition, Vector2 goal)
        {
            Vector2 direction = goal - startPosition;
            float distanceTolerance = 45f;

            foreach(ICollidable collidable in GameManager.GetCollidables())
            {
                Vector2 colPos = collidable.Collider.Position;
                float distance = Vector2.Distance(colPos, startPosition);

                if(distance < distanceTolerance)
                {
                    float goalAngle = (goal - startPosition).ToAngle();
                    float colliderAngle = (colPos - startPosition).ToAngle();

                    float arclength = 2 * (new Vector2(distance, collidable.Collider.Radius).ToAngle());

                    if(MathF.Abs(goalAngle - colliderAngle) < arclength)
                    {                
                        return direction;
                    }
                }
            }


            return direction;
            
        }





    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    public partial class NPCBrain
    {

        Func<int> integer = () => 1;

        //GIANT OL SWITCH STATEMENT TO BUILD A GOAL BASED ON STRING INPUT. HOPEFULLY U SPELL IT RIGHT!
        //I can probably turn goaltype into an enum at some point.
        public void GenerateGoal(string goalType, Entity newTarget=null)
        {
            if (newTarget != null)
                this.Target = newTarget;

           
            switch (goalType)
            {

                case "kill":
                    if (!(this.Target is Creature creature)) throw new Exception("uhh i think you loaded a kill goal with an invalid target. heres your target: " + this.Target.ToString());

                    Condition targetDead = new Condition(() => creature.GetAttribute("health"), () => 0f, 1);
                    Condition inAttackRange = new Condition(() => Vector2.Distance(creature.WorldPosition, this.Vessel.WorldPosition), () => 65, -1);
                    Condition outsideAttackRange = new Condition(() => Vector2.Distance(creature.WorldPosition, this.Vessel.WorldPosition), () => 55, 1);


                    Dictionary<Condition, Action> goalSatisfyers = new Dictionary<Condition, Action>()
                    {
                        {targetDead, () => this.CompleteGoal() },
                        {outsideAttackRange, () => this.DetermineBestAction("approach") },
                        {inAttackRange, () => this.DetermineBestAction("attack") }
                    };


                    this.Goal = new Goal(goalSatisfyers);
                    return;



                case "flee":
                    Vector2 fleeFrom = this.Target.WorldPosition;

                    Condition safeDistanceReached = new Condition(() => Vector2.Distance(this.Vessel.WorldPosition, fleeFrom), () => 100f, 1);

                    return;

                default:
                    throw new Exception("whatever you tried loading into this NPC, it wasnt a goal. Heres what you asked for: " + goalType);


            }
        }



        public void DetermineBestAction( string actionType)
        {
            switch (actionType)
            {
                case "approach":
                    
                    Vector2 direct = this.SimpleFollow(this.Vessel.WorldPosition, Target.WorldPosition);
                    
                    if (direct.X > 0)
                    {
                        
                        this.Vessel.QueueAction(ActionType.MoveRight);
                    }
                        
                    else if (direct.X < 0)
                        this.Vessel.QueueAction(ActionType.MoveLeft);
                    if (direct.Y > 0)
                        this.Vessel.QueueAction(ActionType.MoveDown);
                    else if (direct.Y < 0)
                        this.Vessel.QueueAction(ActionType.MoveUp);

                    return;

                case "attack":
                    Vessel.ActionMap[ActionType.Primary].Invoke();
                    return;

            }

        }
    }
}

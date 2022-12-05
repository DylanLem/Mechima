using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public class Goal
    {

        /*The list of conditions that will be tracked and mapped to actions. This is scary
         * pseudocode example:
         * Condition attackInRange = new Condition(() => return target.GetDistance(), () => return self.DetermineAttackRange(), <)
         * this creates a condition that evaluates true if the creature is within attack range
         * 
         * Satisfyers[attackInRange] = DetermineAction("attack");
         * 
         * We would need another condition for telling the creature to approach if it's too far away
        */
        public Dictionary<Condition, Action> Satisfyers;

        public Goal(Dictionary<Condition,Action> _satisfyers)
        {
            Satisfyers = _satisfyers;
        }

        public List<Action> CheckSatisfyers()
        {
            List<Action> SatisfiedConditions = new List<Action>();
            foreach(KeyValuePair<Condition,Action> pair in Satisfyers)
            {
                if (pair.Key.CheckCondition())
                    SatisfiedConditions.Add(pair.Value);
            }

            return SatisfiedConditions;
        }



    }
}

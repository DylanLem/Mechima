using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Mechima
{
    

    public partial class NPCBrain
    {
        public Creature Vessel;

        public Entity Target;

        public Goal Goal;

        public InherentBehavior behavior;

        public Dictionary<ItemTag, Item> ToolMap = new Dictionary<ItemTag, Item>();


        public void Update()
        {
            if(Goal != null)
                foreach(Action action in Goal.CheckSatisfyers())
                    action.Invoke();
                

        }
        public void SortTools()
        {

        }




        

        public void CompleteGoal() { }
        
    }
}

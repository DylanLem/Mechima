using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    

   
    public class Condition
    {

        public Func<float> Tracker { get; private set; }
        public Func<float> Threshold { get; private set; }

        private int Comparator;  


        public Condition(Func<float> tracker, Func<float> threshold, int comparator)
        {
            //sanitizing comparator values
            if (comparator < 0)
                Comparator = -1;
            else if (comparator == 0)
                Comparator = 0;
            else
                Comparator = 1;

            Tracker = tracker;
            Threshold = threshold;
        }

        
        public bool CheckCondition()
        {
            
            float trackVal = Tracker.Invoke();
            float threshval = Threshold.Invoke();

            return trackVal.CompareTo(threshval) == Comparator;
        }
        
    }
}

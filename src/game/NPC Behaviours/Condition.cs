using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    

    // Summary:
    //     Defines a generalized condition for two values. Tracks one against the other with a predefined comparator variable
    //
    // Type parameters:
    //   T:
    //     The type of object that will be tracked.
    //   V:
    //      The type of object that the tracked value will compare against
    // Parameters:
    //   tracker:
    //      tracked value of type T
    //   threshold:
    //      value to be compared against
    //   comparator:
    //      integer corresponding to whether the tracked value should be greater, less than, or equal to the threshold value
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

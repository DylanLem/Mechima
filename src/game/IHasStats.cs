using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public interface IHasStats
    {
        Dictionary<string, float> Modifiers { get; set; }
        Dictionary<string, float> Stats { get; set; }

        public void AddModifier(string stat, float value)
        {
            if (!this.Modifiers.ContainsKey(stat))
                this.Modifiers[stat] = value;
            else
                this.Modifiers[stat] += value;
        }

        public float GetStat(string stat)
        {
            float valmod = 0;
            this.Modifiers.TryGetValue(stat, out valmod);
            float val = 0;
            this.Stats.TryGetValue(stat, out val);

            return val + valmod;
        }
    }
}

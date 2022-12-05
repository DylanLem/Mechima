using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    //Any ability/effect that deals damage will conform to IDamaging
    interface IDamaging
    {
        DamageType DamageType { get; set; }

        float CalculateDamage();
    }
}

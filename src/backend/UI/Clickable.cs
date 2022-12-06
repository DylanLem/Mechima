using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima 
{
    class Clickable: Drawable
    {
        EventHandler OnMouseDown { get; set; }
        EventHandler OnMouseUp { get; set; }
        EventHandler OnMouseOver { get; set; }
        EventHandler OnMouseExit { get; set; }
        
    }
}

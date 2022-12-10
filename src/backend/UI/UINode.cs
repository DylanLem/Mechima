using System;
using System.Collections.Generic;
using System.Text;

namespace Mechima
{
    public class UINode: Clickable
    {
        public UINode Parent { get; private set; }
        public List<UINode> Children { get; private set; }
        
        public bool Enabled { get => enabled; set => Toggle(); }
        private bool enabled;




        
        private void Toggle() 
        {
            enabled = !enabled;
            foreach (UINode child in Children)
                child.Toggle();
        }

    }
}

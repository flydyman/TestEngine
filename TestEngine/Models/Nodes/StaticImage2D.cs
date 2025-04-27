using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEngine.Models.Nodes
{
    public class StaticImage2D : Node2D
    {
        
        public override void Draw()
        {
            if (IsVisible && IsActive)
            {
                // Draw here
            }
        }

        public override void OnLoad()
        {
            // Load resource here
        }

        public override void OnUpdate()
        {
            if (IsActive)
            {
                // Action handler here
            }
        }
    }
}
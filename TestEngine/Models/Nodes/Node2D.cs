
using OpenTK.Mathematics;

namespace TestEngine.Models.Nodes
{
    public abstract class Node2D: Node
    {
        public Vector2i GlobalPosition {get;set;}
        public bool IsAbsolute {get;set;} = false;
        public Vector2i Position {get;set;}
        public Vector2 Scale = new Vector2(1,1);
        public float Angle {get; set;} = 0; // 1 = 0 = -1

        protected override void Draw()
        {
            if (IsAbsolute)
            {
                Position = GlobalPosition;
            } else {
                if (Parent!=null)
                {

                }
            }
        }

        public void FlipH()
        {
            Scale = new Vector2(Scale.X, -Scale.Y);
        }

        public void FlipV()
        {
            Scale = new Vector2(-Scale.X, Scale.Y);
        }
    }
}
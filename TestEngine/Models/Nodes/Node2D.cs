
using OpenTK.Mathematics;

namespace TestEngine.Models.Nodes
{
    public abstract class Node2D: Node
    {
        public Vector2i Position {get;set;}
        public Vector2 Scale = new Vector2(1,1);
        public float Angle {get; set;} = 0; // 1 = 0 = -1

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
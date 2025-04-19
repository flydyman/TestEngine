
using OpenTK.Mathematics;

namespace TestEngine.Models.Nodes
{
    public abstract class Node2D: Node
    {
        public Vector2i Position {get;set;}
        public float Angle {get; set;} = 0;
    }
}
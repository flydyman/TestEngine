using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEngine.Models
{
    public abstract class Node
    {
        public Node? Parent {get;set;} = null;
        public List<Node> Children = new List<Node>();
        public string Name{get;set;}

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void Draw();

        public void AddChild(Node child)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEngine.Models
{
    public abstract class Node: IDisposable, IComparable<Node>
    {
        public Node? Parent {get;set;} = null;
        public List<Node> Children = new List<Node>();
        public string Name{get;set;}
        public int Order {get;set;}
        bool _isDisposed = false;

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void Draw();

        public virtual void Dispose(bool disposed)
        {
            if (!_isDisposed){
                foreach (Node child in Children)
                {
                    RemoveChild(child);
                    child.Dispose();
                }
                _isDisposed = true;
            }
        }

        ~Node()
        {
            if (!_isDisposed)
            Console.WriteLine($"Node {Name} is not disposed!");
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public void AddChild(Node child)
        {
            child.Parent = this;
            Children.Add(child);
        }

        public void RemoveChild(Node child)
        {
            child.Parent = null;
            Children.Remove(child);
        }

        public Node? GetChild(string name)
        {
            Node? res = Children.FirstOrDefault(x=>x.Name == name);
            return res;
        }

        public void SortChildren()
        {
            Children.Sort();
        }

        public int CompareTo(Node? other)
        {
            if (other == null) return 1;
            else
                return Order.CompareTo(other.Order);
        }
    }
}
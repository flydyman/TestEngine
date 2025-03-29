using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEngine.Models
{
    public abstract class Scene : IDisposable
    {
        bool _isDisposed = false;
        public string Name {get;}
        public Node MainNode {get;set;}

        public Scene(string name)
        {
            Name = name;
        }

        public virtual void Dispose(bool dispose)
        {
            if(!_isDisposed)
            {
                // TODO: Clean resources
                _isDisposed = true;
            }
        }
        ~Scene()
        {
            if (!_isDisposed)
                Console.WriteLine("RAM possible leak on Scene");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
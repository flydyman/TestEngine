using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEngine.Resources
{
    public class TileEntity2D : IDisposable
    {
        public string Name {get;set;}
        private bool _isDisposed = false;
        // "name", "object" for example:
        // "gr_s", "grass_south.png"
        public Dictionary<string, Texture2D> Textures{get;set;}

        public TileEntity2D(string name, Dictionary<string, Texture2D>? textures)
        {
            Name = name;
            if (textures == null) Textures = new Dictionary<string, Texture2D>(); else 
            {
                Textures = new Dictionary<string, Texture2D>();
                foreach (KeyValuePair<string,Texture2D> item in textures)
                    Textures.Add(item.Key, item.Value);
            }

        }

        ~TileEntity2D()
        {
            if (!_isDisposed) Console.WriteLine($"Possible RAM leaking in {this.GetType().Name}");
        }
        public void Dispose()
        {
            if (!_isDisposed)
            {
                // Free resources
                _isDisposed= true;
                Textures.Clear();
            }
            GC.SuppressFinalize(this);
        }
    }
}
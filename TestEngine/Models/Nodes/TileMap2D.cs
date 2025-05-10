using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestEngine.Resources;

namespace TestEngine.Models.Nodes
{
    public class TileMap2D : Node2D
    {
        // Q: Where should I keep map data: here or somewhere else? 

        public List<TileEntity2D> Tiles { get; set; }
        MapTable2D _mapTable2D;

        public TileMap2D(string name, List<TileEntity2D>? tiles)
        {
            Name = name;
            if (tiles != null)
            {
                Tiles = tiles;
            } else Tiles = new List<TileEntity2D>();
        }

        protected override void Draw()
        {
            // From here I need global coordinates
            throw new NotImplementedException();
        }

        public override void OnLoad()
        {
            
        }

        public override void OnUpdate()
        {
            
        }

        public override void Dispose(bool disposed)
        {
            Tiles.Clear();

            base.Dispose(disposed);
        }
    }
}
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using TestEngine.Resources;

namespace TestEngine.Models.Nodes
{
    public class StaticImage2D : Node2D
    {
        Texture2D? _texture;

        private int _vao;
        private int _vbo;

        private float[] _vertices;
        
        public override void Draw()
        {
            if (IsVisible && IsActive)
            {
                if (_texture != null)
                {
                    _texture.Bind();
                    _texture.SetOffset(Position);
                    _texture.SetScale(Scale);
                    _texture.SetScreenSize(new Vector2(GlobalSettings.ScreenWidth, GlobalSettings.ScreenHeight));

                    GL.BindVertexArray(_vao);
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
                    GL.BindVertexArray(0);
                    _texture.Unbind();
                }
            }
        }

        public void AssignTexture(Texture2D texture)
        {
            if (_texture != null) _texture.Dispose();
            _texture = texture;
        }

        public override void OnLoad()
        {
            // Load resource here
            _vertices = new float[]{
                // Position (x, y, z)      // Texture coordinates (u, v)
            -0.5f, -0.5f, 0.0f,        0.0f, 0.0f, // Bottom left
             0.5f, -0.5f, 0.0f,        1.0f, 0.0f, // Bottom right
             0.5f,  0.5f, 0.0f,        1.0f, 1.0f, // Top right
             
            -0.5f, -0.5f, 0.0f,        0.0f, 0.0f, // Bottom left
             0.5f,  0.5f, 0.0f,        1.0f, 1.0f, // Top right
            -0.5f,  0.5f, 0.0f,        0.0f, 1.0f  // Top left
            };
            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);

            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length*sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0,3,VertexAttribPointerType.Float, false, 5*sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1,2, VertexAttribPointerType.Float, false, 5*sizeof(float),3*sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.BindVertexArray(0);
        }

        public override void OnUpdate()
        {
            if (IsActive)
            {
                // Action handler here
            }
        }

        public override void Dispose(bool disposed)
        {
            if (!disposed && _texture!=null) _texture.Dispose(); 
            base.Dispose(disposed);
        }
    }
}
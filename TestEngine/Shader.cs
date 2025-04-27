
using OpenTK.Graphics.OpenGL;

namespace TestEngine
{
    public class Shader : IDisposable
    {
        public int _handle; // Made public for Texture2D to access
        int _fragmentShader;
        int _vertexShader;
        bool _isDisposed = false;
        public bool IsCompiled = false;

        public Shader(string vertex, string fragment)
        {
            bool done = true;
            _vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShader, vertex);
            _fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShader, fragment);
            
            GL.CompileShader(_vertexShader);
            int success;
            GL.GetShader(_vertexShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string info = GL.GetShaderInfoLog(_vertexShader);
                Console.WriteLine(info);
                done = false;
            }

            GL.CompileShader(_fragmentShader);
            GL.GetShader(_fragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string info = GL.GetShaderInfoLog(_fragmentShader);
                Console.WriteLine(info);
                done = false;
            }

            if (done){
                _handle = GL.CreateProgram();
                GL.AttachShader(_handle, _vertexShader);
                GL.AttachShader(_handle, _fragmentShader);
                GL.LinkProgram(_handle);
                GL.GetProgram(_handle, GetProgramParameterName.LinkStatus, out success);
                if (success == 0)
                {
                    string info = GL.GetProgramInfoLog(_handle);
                    Console.WriteLine(info);
                    IsCompiled = false;
                } else
                    IsCompiled = true;
            } else {
                IsCompiled = false;
            }
        }

        public void Use()
        {
            if (IsCompiled)
                GL.UseProgram(_handle);
            else
                Console.WriteLine("Shader is not compiled");
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_isDisposed){
                GL.DeleteProgram(_handle);
                _isDisposed = true;
            }
        }

        ~Shader()
        {
            if (!_isDisposed)
            {
                Console.WriteLine("GPU resource leak on Shader");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
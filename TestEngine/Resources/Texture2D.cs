using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TestEngine.Resources
{
    public class Texture2D : IDisposable
    {
        bool _isDisposed = false;
        int _texture;
        public Shader? Shader {get;set;}

        public Texture2D(string filename)
        {
            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            Image<Rgba32> img = Image.Load<Rgba32>(filename);

            byte[] data = new byte[img.Width * img.Height * 4];
            img.CopyPixelDataTo(data);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            // ?
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Bind(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, _texture);
        }

        ~Texture2D()
        {
            if (!_isDisposed)
            {
                Console.WriteLine("Possible RAM leaking in Texture2D");
            }
        }
        public void Dispose()
        {
            if (_texture!=0)
            {
                GL.DeleteTexture(_texture);
                _texture = 0;
            }
            if (!_isDisposed)
                {
                    _isDisposed = true;
                }
            GC.SuppressFinalize(this);
        }
    }
}
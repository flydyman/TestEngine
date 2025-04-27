using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace TestEngine.Resources
{
    public class Texture2D : IDisposable
    {
        bool _isDisposed = false;
        int _texture;
        public Shader Shader {get; private set;}
        public int Width {get; private set;}
        public int Height {get; private set;}
        
        // Locations for shader uniforms
        private int _offsetLocation;
        private int _textureLocation;
        private int _scaleLocation;
        private int _screenSizeLocation;
        
        // Default shader strings
        private const string DefaultVertexShaderSource = @"
#version 330 core
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

uniform vec2 uOffset;      // Position in screen coordinates (pixels)
uniform vec2 uScale;       // Scale of the sprite
uniform vec2 uScreenSize;  // Screen dimensions for conversion

void main()
{
    texCoord = aTexCoord;
    
    // Convert from pixel coordinates to normalized device coordinates [-1, 1]
    vec2 normalizedPosition = (uOffset / uScreenSize) * 2.0 - 1.0;
    
    // Apply scale (normalized by screen dimensions to maintain aspect ratio)
    vec2 normalizedScale = uScale / uScreenSize * 2.0;
    
    // Calculate final position with scaling and offset
    vec2 finalPosition = aPosition.xy * normalizedScale + normalizedPosition;
    
    // Y coordinate is flipped in OpenGL (positive is up, but in screen coordinates positive is down)
    finalPosition.y = -finalPosition.y;
    
    gl_Position = vec4(finalPosition, aPosition.z, 1.0);
}
";

        private const string DefaultFragmentShaderSource = @"
#version 330 core
out vec4 FragColor;
  
in vec2 texCoord;

uniform sampler2D uTexture;

void main()
{
    FragColor = texture(uTexture, texCoord);
}
";


        public Texture2D(string filename)
        {
            // Initialize shader
            if (Shader == null) InitializeShader();

            // Load texture
            _texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Add transparent to PNG
            // TODO check how it works with other formats, f.e. BMP or PCX
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            Image<Rgba32> img = Image.Load<Rgba32>(filename);
            Width = img.Width;
            Height = img.Height;

            byte[] data = new byte[img.Width * img.Height * 4];
            img.CopyPixelDataTo(data);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            // Unbind texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public Texture2D(string filename, Shader shader): this(filename)
        {
            Shader = shader;
            //return this(filename);
        }
        
        private void InitializeShader()
        {
            // Create and compile shader
            Shader = new Shader(DefaultVertexShaderSource, DefaultFragmentShaderSource);
            
            if (!Shader.IsCompiled)
            {
                Console.WriteLine("Failed to compile texture shader");
                return;
            }
            
            // Get uniform locations
            Shader.Use();
            _offsetLocation = GL.GetUniformLocation(Shader._handle, "uOffset");
            _textureLocation = GL.GetUniformLocation(Shader._handle, "uTexture");
            _scaleLocation = GL.GetUniformLocation(Shader._handle, "uScale");
            _screenSizeLocation = GL.GetUniformLocation(Shader._handle, "uScreenSize");
        }

        public void Bind(TextureUnit unit = TextureUnit.Texture0)
        {
            // Bind the texture
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, _texture);
            
            // Use the shader and set the texture uniform
            if (Shader != null && Shader.IsCompiled)
            {
                Shader.Use();
                GL.Uniform1(_textureLocation, 0); // Assuming texture unit 0
            }
        }
        
        public void SetOffset(Vector2 offset)
        {
            if (Shader != null && Shader.IsCompiled)
            {
                Shader.Use();
                GL.Uniform2(_offsetLocation, offset.X, offset.Y);
            }
        }
        
        public void SetScale(Vector2 scale)
        {
            if (Shader != null && Shader.IsCompiled)
            {
                Shader.Use();
                GL.Uniform2(_scaleLocation, scale.X, scale.Y);
            }
        }
        
        public void SetScreenSize(Vector2 screenSize)
        {
            if (Shader != null && Shader.IsCompiled)
            {
                Shader.Use();
                GL.Uniform2(_screenSizeLocation, screenSize.X, screenSize.Y);
            }
        }
        
        public void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
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
            
            // Dispose of the shader
            Shader?.Dispose();
            
            if (!_isDisposed)
            {
                _isDisposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
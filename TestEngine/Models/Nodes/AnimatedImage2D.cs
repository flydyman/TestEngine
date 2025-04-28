using System.Timers;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using TestEngine.Resources;
using Timer = System.Timers.Timer;

namespace TestEngine.Models.Nodes;

public class AnimatedImage2D: Node2D
{
    public AnimationBank2D Animations { get; private set; } = new AnimationBank2D("default");
    private int _innerCounter = 0;
    private string _currentAnimation = String.Empty;
    private int _currentFrame = 0;
    private bool _backwards = false;
    
    private int _vao;
    private int _vbo;

    private float[] _vertices;

    private System.Timers.Timer _innerTimer;

    private object _locker = new object();

    public AnimatedImage2D()
    {
        //
    }

    public AnimatedImage2D(AnimationBank2D bank) : this()
    {
        Animations = bank;
    }

    public void Play(string action, bool backward = false)
    {
        if (_currentAnimation == action && _backwards == backward) return;
        // Find animation
        AnimationEntity2D? anim = Animations.Animations.FirstOrDefault(x => x.Name == action);
        if (anim == null)
            return;
        _currentAnimation = action;
        _innerCounter = 0;
        _currentFrame = 0;
        _backwards = backward;
        _innerTimer = new Timer(1000 / anim.AnimationSpeed);
        _innerTimer.Elapsed += InnerTimerOnElapsed;
        _innerTimer.AutoReset = anim.IsLooped;
        _innerTimer.Start();
    }

    private void InnerTimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        lock(_locker)
        {
            /*
            _innerCounter++;
            if (_innerCounter >= Animations.Animations.First(x => x.Name == _currentAnimation).AnimationSpeed)
            {
                _innerCounter = 0;
                _currentFrame++;
            }/**/

            _currentFrame++;

            if (_currentFrame >= Animations.Animations.First(x => x.Name == _currentAnimation).Textures.Count)
                if (Animations.Animations.First(x => x.Name == _currentAnimation).IsLooped) _currentFrame = 0;
                else
                {
                    _currentFrame = Animations.Animations.First(x => x.Name == _currentAnimation).Textures.Count - 1;
                    _innerTimer.Stop();
                }
        }
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
            // input or logic here
        }
    }

    public override void Draw()
    {
        if (IsVisible && IsActive)
        {
            lock(_locker)
            {
                Texture2D _texture = Animations.Animations.First(x => x.Name == _currentAnimation)
                    .Textures[_currentFrame];
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
    }

    public override void Dispose(bool disposed)
    {
        if (!disposed ) Animations.Dispose();
        base.Dispose(disposed);
    }
}
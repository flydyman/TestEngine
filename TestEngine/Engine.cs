using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace TestEngine;

public class Engine: GameWindow
{
    public Engine(int width, int height, string title):
    base(GameWindowSettings.Default, new NativeWindowSettings{
        ClientSize = (width, height), Title= title
    })
    {

    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Call Draw here

        SwapBuffers();
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f,0.3f,0.5f,1.0f);
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }
}

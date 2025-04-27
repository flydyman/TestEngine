using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using TestEngine.Models;

namespace TestEngine;

public class Engine: GameWindow
{
    List<Scene> scenes = new List<Scene>();
    public Engine(int width, int height, string title):
    base(GameWindowSettings.Default, new NativeWindowSettings{
        ClientSize = (width, height), Title= title
    })
    {
        GlobalSettings.ScreenWidth = width;
        GlobalSettings.ScreenHeight = height;
    }

    public void InvokeScene(Scene scene)
    {
        scenes.Add(scene);
    }

    public void RemoveScene(string name)
    {
        scenes.Remove(scenes.First(x=>x.Name == name));
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Call Draw here
        foreach (Scene scene in scenes) scene.Draw();

        SwapBuffers();
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.ClearColor(0.2f,0.3f,0.5f,1.0f);
        foreach (Scene scene in scenes) scene.OnLoad();
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
        
        GlobalSettings.ScreenWidth = e.Width;
        GlobalSettings.ScreenHeight = e.Height;
    }
}

using OpenTK.Mathematics;
using TestEngine;
using TestEngine.Models;
using TestEngine.Models.Nodes;
using TestEngine.Resources;

namespace DataTescting
{
    public class Example01
    {
        public Example01(Engine engine)
        {
            Scene scene = new Scene("main");
            StaticImage2D img = new StaticImage2D();
            Texture2D tex = new Texture2D("res/draw1.png");
            img.AssignTexture(tex);
            img.Position = new Vector2i(100, 100);
            img.Scale = new Vector2i(64,64);
            scene.MainNode = img;
            engine.InvokeScene(scene);
        }
    }
}
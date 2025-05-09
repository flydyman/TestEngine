using OpenTK.Mathematics;
using TestEngine;
using TestEngine.Models;
using TestEngine.Models.Nodes;
using TestEngine.Resources;

namespace DataTescting
{
    public class Example01
    {
        // Example 01 - static sprite
        public Example01(Engine engine)
        {
            Scene scene = new Scene("main");
            StaticImage2D img = new StaticImage2D();
            // Variant for testing region selecting from texture
            //Texture2D tex = new Texture2D("res/draw1.png", new Vector2i(32, 16), new Vector2i(0,1));
            Texture2D tex = new Texture2D("res/draw1.png");
            img.AssignTexture(tex);
            img.Position = new Vector2i(100, 100);
            img.Scale = new Vector2(1.5f,1.5f);
            scene.MainNode = img;
            engine.InvokeScene(scene);
        }
    }
}
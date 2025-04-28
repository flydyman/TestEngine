using OpenTK.Mathematics;
using TestEngine;
using TestEngine.Models;
using TestEngine.Models.Nodes;
using TestEngine.Resources;

namespace DataTescting;

public class Example02
{
    // Example 02 - animated sprite
    public Example02(Engine engine)
    {
        Texture2D frame0 = new Texture2D("res/anim.png", new Vector2i(16, 16), new Vector2i(0, 0));
        Texture2D frame1 = new Texture2D("res/anim.png", new Vector2i(16, 16), new Vector2i(0, 1));

        AnimatedImage2D img = new AnimatedImage2D();
        AnimationEntity2D walk = new AnimationEntity2D("walk");
        walk.Textures.AddRange(new []{frame0, frame1});
        walk.IsLooped = true;
        // Bigger -> faster
        walk.AnimationSpeed = 5;
        img.Animations.Animations.Add(walk);
        img.Position = new Vector2i(100, 100);
        img.Scale = new Vector2(3, 3);
        
        img.Play("walk");

        Scene scene = new Scene("main");
        scene.MainNode = img;
        
        engine.InvokeScene(scene);
    }
}
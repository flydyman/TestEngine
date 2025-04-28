using OpenTK.Mathematics;
using TestEngine;
using TestEngine.Models;
using TestEngine.Models.Nodes;
using TestEngine.Resources;

namespace DataTescting;

public class Example03
{
    // More advanced spritesheet from https://anokolisa.itch.io/action
    public Example03(Engine engine)
    {
        List<Texture2D> texs = new List<Texture2D>();
        for (int x = 0; x<40; x++)
            texs.Add(new Texture2D("res/Full-Sheet.png", new Vector2i(72,72), new Vector2i(x,0)));

        AnimatedImage2D img = new AnimatedImage2D();
        AnimationEntity2D walk = new AnimationEntity2D("walk");
        walk.Textures.AddRange(texs);
        walk.IsLooped = true;
        // Bigger -> faster
        walk.AnimationSpeed = 10;
        img.Animations.Animations.Add(walk);
        img.Position = new Vector2i(200, 200);
        img.Scale = new Vector2(3, 3);
        
        img.Play("walk");

        Scene scene = new Scene("main");
        scene.MainNode = img;
        
        engine.InvokeScene(scene);
    }
}
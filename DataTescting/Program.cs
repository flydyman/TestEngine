// See https://aka.ms/new-console-template for more information
using OpenTK.Mathematics;
using TestEngine;
using TestEngine.Models;
using TestEngine.Models.Nodes;
using TestEngine.Resources;

Console.WriteLine("Hello, World!");

using (Engine game = new Engine(800,600,"Oh, hi Mark"))
{
    Scene scene = new Scene("main");
    StaticImage2D img = new StaticImage2D();
    Texture2D tex = new Texture2D("res/draw1.png");
    img.AssignTexture(tex);
    img.Position = new Vector2i(100, 100);
    img.Scale = new Vector2i(64,64);
    scene.MainNode = img;
    game.InvokeScene(scene);
    game.Run();
}

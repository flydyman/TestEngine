// See https://aka.ms/new-console-template for more information
using DataTescting;
using OpenTK.Mathematics;
using TestEngine;
using TestEngine.Models;
using TestEngine.Models.Nodes;
using TestEngine.Resources;

Console.WriteLine("Hello, World!");

using (Engine game = new Engine(800,600,"Oh, hi Mark"))
{
    new Example03(game);
    game.Run();
}

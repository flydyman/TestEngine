// See https://aka.ms/new-console-template for more information
using TestEngine;

Console.WriteLine("Hello, World!");

using (Engine game = new Engine(800,600,"Oh, hi Mark"))
{
    game.Run();
}

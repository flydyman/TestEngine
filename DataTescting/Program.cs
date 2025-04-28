using DataTescting;
using TestEngine;

Console.WriteLine("Hello, World of Coding!");

using (Engine game = new Engine(800,600,"Oh, hi Mark"))
{
    new Example03(game);
    game.Run();
}

using ItemInventoryManager.GameObjects;
using System;

namespace ItemInventoryManager
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool running = true;
            Game Game = new Game();
            Game.GameLoop(running);
        }

    }
}

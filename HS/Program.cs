using System;

namespace HS
{
    class Program
    {
        static StateManager stateManager;

        static void Main(string[] args)
        {
            Setup();
            while (true)
            {
                Update();
            }
        }

        static void Setup()
        {
            stateManager = new StateManager();
            stateManager.Setup();
        }

        static void Update()
        {
            stateManager.Update();
        }
    }
}

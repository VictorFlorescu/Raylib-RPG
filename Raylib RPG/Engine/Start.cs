using Raylib_CsLo;


namespace Engine
{
    internal static class Engine
    {
        public static bool devMode = true;

        public static int screenWidth = 1280;
        public static int screenHeight = 720;

        static void Main()
        {
            EngineLoop();
        }

        public static void EngineLoop()
        {
            Raylib.InitWindow(screenWidth, screenHeight, "Engine");

            Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor())); // Sets max FPS using the refresh rate of the current display


            // Screen Manager

            // Engine Loop
            while (!Raylib.WindowShouldClose())
            {
                // Update 
                ScreenManager.UpdateScreen();

                // ----------------------------------------

                // Draw

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.WHITE);

                if (devMode)
                {
                    Raylib.DrawFPS(10, 10);
                }
                ScreenManager.DrawScreen();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}